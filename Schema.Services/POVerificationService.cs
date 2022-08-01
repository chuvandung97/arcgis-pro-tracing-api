using System;
using System.Collections.Generic;
using System.Linq;
using Schema.Core.Models;
using System.Text;
using System.Threading.Tasks;
using Schema.Core.Data;
using Schema.Core.Services;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Collections;
using System.Drawing;
using Schema.Core.Utilities;

namespace Schema.Services
{
    public class POVerificationService : IPOVerificationService
    {
        ILoggingService _loggingService;
        IPOVerificationDataService _poVerificationDataService;
        Dictionary<string, object> errorLogInfo;
        CommonUtilities _commonUtilities = new CommonUtilities();
        public POVerificationService(ILoggingService LoggingService, IPOVerificationDataService POVerificationDataService)
        {
            _loggingService = LoggingService;
            _poVerificationDataService = POVerificationDataService;
        }
        public ICustomAuthorizeService CustomAuthorizeService
        {
            get
            {
                return DependencyResolver.Current.GetService<ICustomAuthorizeService>();
            }
        }
        //added by Sandip on 1st Sept 2019 for RFC0021956 -- This api returns the complete list of PO Verification IDs that are assigned to the logged in Project Officer
        public async Task<HashSet<Dictionary<string, object>>> GetPOJobInfoAsync(string ProjectOfficerID)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _poVerificationDataService.GetPOJobInfoAsync(ProjectOfficerID);
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }
        //added by Sandip on  8th Sept 2019 for RFC0021956 -- When PO approves/rejects As-Built data in Schema application, the same will be updated in PostgreSQL database.
        public async Task<Dictionary<string, object>> UpdatePOJobInfoAsync(object JsonObj, string ProjectOfficerID)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            string toAddress = string.Empty;
            string subject = string.Empty;
            string body = string.Empty;
            string response = string.Empty;
            string imageType = string.Empty;
            string imageName = string.Empty;
            string finalImageName = string.Empty;
            string poAttachmentPath = string.Empty;
            string fromAddress = string.Empty;
            string adminURL = string.Empty;
            string ccAddress = string.Empty;
            Image image;

            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "updatePOJobInfo");
                var rowInfo = JsonConvert.DeserializeObject<List<POVerficationListItem>>(jsonVal[0]);
                adminURL = System.Configuration.ConfigurationManager.AppSettings["AdminURL"];

                if (rowInfo[0].postatus == "POREJECTED" && string.IsNullOrEmpty(rowInfo[0].poremarks))
                {
                    result.Add("message", "Remark is mandatory in case of rejection!");
                    return result;
                }
                else
                    result = await _poVerificationDataService.UpdatePOJobInfoAsync(rowInfo);

                /*if (!string.IsNullOrEmpty(rowInfo[0].imageattachment))
                {
                    //To send email to MEA & contractor, on PO approval/rejection of data on Schema side.
                    imageType = rowInfo[0].imageattachment.Split(',')[0].Split('/')[1].Split(';')[0];
                    imageName = "SCHEMA_" + rowInfo[0].sessionid + "_" + DateTime.Now.TimeOfDay.ToString().Replace(":", "").Replace(".", "");
                    finalImageName = imageName + "." + imageType;
                    poAttachmentPath = ConfigurationManager.AppSettings["POVerificationImageAttachment"];

                    byte[] bytes = Convert.FromBase64String(rowInfo[0].imageattachment.Split(',')[1]);

                    image = new Bitmap(new MemoryStream(bytes));
                    using (Image imageToExport = image)
                    {
                        imageToExport.Save(poAttachmentPath + imageName + "." + imageType);
                    }
                }*/

                fromAddress = System.Configuration.ConfigurationManager.AppSettings["EmailFromAddress"];

                if (result.Values.Contains("Success"))
                {
                    if (rowInfo[0].postatus == "POAPPROVED")
                    {
                        toAddress = rowInfo[0].meaeditoremail;
                        ccAddress = "SCHEMA.SUPPORT@cyient.com";
                        subject = "As-Built project approved by PO.";
                        /*body = "Dear Sir/Mam, @@Session Name - '" + rowInfo[0].sessionname.Replace("&","*") + "' has been approved successfully by " + ProjectOfficerID.ToUpper() + ". " +
                               "@@For more details, please click - " + adminURL + " @@Thank You";*/

                        //RFC0032812 - Added by Sandip on 14th April 2021, to add POVID in the hyperlink so that user can directly see the record in Admin portal 
                        if (string.IsNullOrEmpty(rowInfo[0].poremarks))
                        {
                            body = "Dear Sir/Mam, @@Session Name - '" + rowInfo[0].sessionname.Replace("&", "*") + "' has been approved successfully by " + ProjectOfficerID.ToUpper() + ". " +
                                    "@@To check status or change assigned PO click on - " + adminURL + "?id=" + rowInfo[0].povid + " @@Thank You";
                        }
                        else
                        {
                            body = "Dear Sir/Mam, @@Session Name - '" + rowInfo[0].sessionname.Replace("&", "*") + "' has been approved successfully by " + ProjectOfficerID.ToUpper() + ". @@Remarks - " + rowInfo[0].poremarks + ". " +
                                    "@@To check status or change assigned PO click on - " + adminURL + "?id=" + rowInfo[0].povid + " @@Thank You";
                        }
                    }
                    else if (rowInfo[0].postatus == "POREJECTED")
                    {
                        toAddress = rowInfo[0].contractoremail;
                        ccAddress = rowInfo[0].poemail + "," + rowInfo[0].meaeditoremail + "," + "SCHEMA.SUPPORT@cyient.com";
                        subject = "As-Built project rejected by PO.";

                        //RFC0032812 - Added by Sandip on 14th April 2021, to add POVID in the hyperlink so that user can directly see the record in Admin portal
                        body = "Dear Sir/Mam, @@Session Name - '" + rowInfo[0].sessionname.Replace("&", "*") + "' has been rejected by " + ProjectOfficerID.ToUpper() + ". @@Remarks - " + rowInfo[0].poremarks + ".  " +
                                "@@To check status or change assigned PO click on - " + adminURL + "?id=" + rowInfo[0].povid + " @@Thank You";
                    }

                    response = await _commonUtilities.SendEmailMethod(fromAddress, toAddress, subject, body, ccAddress, rowInfo[0].imageattachment);
                }
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }
        //added by Sandip on  8th Nov 2019 for RFC0021956 -- Allow PO to upload multiple files using Schema application.
        public async Task<Dictionary<string, object>> UpdatePOVImageOrPDFFileAsync(object JsonObj)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            string imagePdfType = string.Empty;
            string imagePdfName = string.Empty;
            string finalImageName = string.Empty;
            string poAttachmentPath = string.Empty;
            Image image;

            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "uploadPOVFiles");
                var rowInfo = JsonConvert.DeserializeObject<List<POVerficationListItem>>(jsonVal[0]);

                //To upload multiple files on Web Server.
                imagePdfType = rowInfo[0].imageorpdffile.Split(',')[0].Split('/')[1].Split(';')[0];
                imagePdfName = "SCHEMA_" + rowInfo[0].sessionid + "_" + DateTime.Now.TimeOfDay.ToString().Replace(":", "").Replace(".", "");
                finalImageName = imagePdfName + "." + imagePdfType;
                poAttachmentPath = ConfigurationManager.AppSettings["POVerificationImageAttachment"];

                byte[] bytes = Convert.FromBase64String(rowInfo[0].imageorpdffile.Split(',')[1]);

                if (imagePdfType.ToUpper() == "PDF" || imagePdfType.ToUpper() == "DOC" || imagePdfType.ToUpper() == "DOCX")
                {
                    using (FileStream stream = File.Create(poAttachmentPath + finalImageName))
                    {
                        stream.Write(bytes, 0, bytes.Length);
                    }
                }
                else
                {
                    image = new Bitmap(new MemoryStream(bytes));
                    using (Image imageToExport = image)
                    {
                        imageToExport.Save(poAttachmentPath + imagePdfName + "." + imagePdfType);
                    }
                }

                result.Add("FileName", finalImageName);
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }
        //added by Sandip on  19th Sept 2019 for RFC0021956 -- This api will be called from GEMS, based on jobstatus it will return true or false.
        public async Task<HashSet<Dictionary<string, object>>> GetPOVGemsEnablePOButtonAsync(Int64 SessionID)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _poVerificationDataService.GetPOVGemsEnablePOButtonAsync(SessionID);
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }
        //added by Sandip on  20th Sept 2019 for RFC0021956 -- This api will be called from GEMS, based on jobstatus it will return true or false.
        public async Task<HashSet<Dictionary<string, object>>> GetPOVGemsEnableReconcileButtonAsync(Int64 SessionID)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _poVerificationDataService.GetPOVGemsEnableReconcileButtonAsync(SessionID);
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }
        //added by Sandip on  9th Oct 2019 for RFC0021956 -- This api will be called from GEMS, it will return the current job status of each session.
        public async Task<HashSet<Dictionary<string, object>>> GetPOVGemsJobStatusAsync(Int64 SessionID)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _poVerificationDataService.GetPOVGemsJobStatusAsync(SessionID);
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }
        //added by Sandip on  25th Sept 2019 for RFC0021956 -- This api will be called from GEMS, it will update various stages of landbase status.
        public async Task<Dictionary<string, object>> UpdatePOVGemsLandbaseStatusAsync(object JsonObj)
        {
            HashSet<Dictionary<string, object>> emailIDs = new HashSet<Dictionary<string, object>>();
            Dictionary<string, object> result = new Dictionary<string, object>();
            string toAddress = string.Empty;
            string subject = string.Empty;
            string body = string.Empty;
            string response = string.Empty;
            string fromAddress = string.Empty;
            string poEmail = string.Empty;
            string meaEmail = string.Empty;
            string ccAddress = string.Empty;
            string conEmail = string.Empty;
            string meaName = string.Empty;
            string imageAttachment = string.Empty;
            string adminURL = string.Empty;
            string povid = string.Empty;

            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "updatePOVLandbaseStatus");
                var rowInfo = JsonConvert.DeserializeObject<List<POVerficationListItem>>(jsonVal[0]);
                adminURL = System.Configuration.ConfigurationManager.AppSettings["AdminURL"];

                if (string.IsNullOrEmpty(rowInfo[0].sessionname) || rowInfo[0].sessionid == 0)
                {
                    result.Add("message", "SessionName & SessionID are mandatory to proceed further!");
                    return result;
                }
                if (string.IsNullOrEmpty(rowInfo[0].jobstatus))
                {
                    result.Add("message", "Job status is mandatory to proceed further!");
                    return result;
                }
                result = await _poVerificationDataService.UpdatePOVGemsLandbaseStatusAsync(rowInfo);

                if (result.Values.Contains("Success"))
                {
                    emailIDs = await _poVerificationDataService.GetEmailsIDsOfPOVOfficersAsync(rowInfo[0].sessionid);

                    foreach (Dictionary<string, object> item in (IEnumerable)emailIDs)
                    {
                        if (item["povid"] != null)
                            povid = item["povid"].ToString();

                        if (item["poemails"] != null)
                            poEmail = item["poemails"].ToString();

                        if (item["meaemails"] != null)
                            meaEmail = item["meaemails"].ToString();

                        if (item["meanames"] != null)
                            meaName = item["meanames"].ToString();

                        if (item["conemails"] != null)
                            conEmail = item["conemails"].ToString();
                        break;
                    }

                    fromAddress = System.Configuration.ConfigurationManager.AppSettings["EmailFromAddress"];

                    if (rowInfo[0].jobstatus == "GEMSLANDBASEVERIFICATIONREJECTED")
                    {
                        toAddress = conEmail; //Send email to contractor, on landbase team rejection
                        ccAddress = meaEmail + "," + poEmail + "," + "SCHEMA.SUPPORT@cyient.com"; //RFC0034115 -- 19/07/2021 -- To include PO EmailID in the mail chain.
                        subject = "As-Built project rejected by Landbase Team.";
                        body = "Dear Sir/Mam, @@Session Name - '" + rowInfo[0].sessionname + "' has been rejected by " + meaName + ". @@Remarks - " + rowInfo[0].mearemarks + ". @@Thank You";
                        imageAttachment = rowInfo[0].imageattachment;

                        //WriteErrorLog("STARTED :- " + rowInfo[0].jobstatus);
                    }
                    else if (rowInfo[0].jobstatus == "GEMSLANDBASEVERIFICATIONREVIEWPENDING")
                    {

                        toAddress = ConfigurationManager.AppSettings["GEMSLandbaseVerificationReviewEmailTrigger"]; //Send email to Landbase Team for review
                        ccAddress = "SCHEMA.SUPPORT@cyient.com";
                        subject = "As-Built project is pending for Landbase Verification.";

                        //RFC0032812 - Added by Sandip on 14th April 2021, to add POVID in the hyperlink so that user can directly see the record in Admin portal
                        body = "Dear Sir/Mam, @@Session Name - '" + rowInfo[0].sessionname + "' has been assigned successfully by " + meaName + " for you to review." +
                               "@@To check status or change assigned PO click on - " + adminURL + "?id=" + povid + " @@Thank You";
                        imageAttachment = rowInfo[0].imageattachment;
                    }
                    else if (rowInfo[0].jobstatus == "MEAREJECTED")
                    {
                        toAddress = conEmail; //Send email to Contractor , on MEA rejection
                        ccAddress = poEmail + "," + meaEmail + "," + "SCHEMA.SUPPORT@cyient.com"; //Send email to PO and MEA, on MEA Rejection
                        subject = "As-Built project rejected by MEA.";
                        body = "Dear Sir/Mam, @@Session Name - '" + rowInfo[0].sessionname + "' has been rejected by " + meaName + ". @@Remarks - " + rowInfo[0].mearemarks + ". @@Thank You";
                        imageAttachment = rowInfo[0].imageattachment;
                    }
                    else if (rowInfo[0].jobstatus == "POJSONCREATIONFAILED")
                    {
                        toAddress = "SCHEMA.SUPPORT@cyient.com"; //Send email to Schema when JSON creation fails
                        subject = "As-Built project JSON creaton failed";
                        body = "Hello Team, @@There is an error while generating JSON for Session - '" + rowInfo[0].sessionname + "'. Kindly check the server logs. @@Thank You";
                    }
                    response = await _commonUtilities.SendEmailMethod(fromAddress, toAddress, subject, body, ccAddress, imageAttachment);
                }
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }
        //added by Sandip on  30th Sept 2019 for RFC0021956 -- This api will be called from GEMS, when MEA wants to reassign the Session back to PO for his/her verification.
        public async Task<Dictionary<string, object>> UpdatePOVGemsMEAReAssignAsync(object JsonObj)
        {
            HashSet<Dictionary<string, object>> emailIDs = new HashSet<Dictionary<string, object>>();
            Dictionary<string, object> result = new Dictionary<string, object>();
            string toAddress = string.Empty;
            string subject = string.Empty;
            string body = string.Empty;
            string response = string.Empty;
            string fromAddress = string.Empty;
            string poEmail = string.Empty;
            string adminURL = string.Empty;
            string meaName = string.Empty;
            string meaEmail = string.Empty;
            string ccAddress = string.Empty;
            string povid = string.Empty;

            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "updatePOVMEAReassign");
                var rowInfo = JsonConvert.DeserializeObject<List<POVerficationListItem>>(jsonVal[0]);

                adminURL = System.Configuration.ConfigurationManager.AppSettings["AdminURL"];

                if (string.IsNullOrEmpty(rowInfo[0].sessionname) || rowInfo[0].sessionid == 0)
                {
                    result.Add("message", "SessionName & SessionID are mandatory to proceed further!");
                    return result;
                }

                if (string.IsNullOrEmpty(rowInfo[0].mearemarks))
                {
                    result.Add("message", "Remark is mandatory if session is reassigned to PO!");
                    return result;
                }

                result = await _poVerificationDataService.UpdatePOVGemsMEAReAssignAsync(rowInfo);

                emailIDs = await _poVerificationDataService.GetEmailsIDsOfPOVOfficersAsync(rowInfo[0].sessionid);

                foreach (Dictionary<string, object> item in (IEnumerable)emailIDs)
                {
                    if (item["povid"] != null)
                        povid = item["povid"].ToString();

                    if (item["poemails"] != null)
                        poEmail = item["poemails"].ToString();

                    if (item["meanames"] != null)
                        meaName = item["meanames"].ToString();

                    if (item["meaemails"] != null)
                        meaEmail = item["meaemails"].ToString();
                    break;
                }

                if (result.Values.Contains("Success"))
                {
                    fromAddress = System.Configuration.ConfigurationManager.AppSettings["EmailFromAddress"];

                    toAddress = poEmail;
                    ccAddress = meaEmail + "," + "SCHEMA.SUPPORT@cyient.com"; ;
                    subject = "As-Built project has been reassigned by MEA.";

                    //RFC0032812 - Added by Sandip on 14th April 2021, to add POVID in the hyperlink so that user can directly see the record in Admin portal
                    body = "Dear Sir/Mam, @@Session Name - '" + rowInfo[0].sessionname + "' has been reassigned by " + meaName + " for further verificaton. @@Remarks - " + rowInfo[0].mearemarks + "." +
                           "@@To check status or change assigned PO click on - " + adminURL + "?id=" + povid + " @@Thank You";

                    response = await _commonUtilities.SendEmailMethod(fromAddress, toAddress, subject, body, ccAddress, rowInfo[0].imageattachment);
                }
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }
        //added by Sandip on  24th Sept 2019 for RFC0021956 -- This api will be called from GEMS, it will update the jobstatus when the data is finally posted in GEMS.
        public async Task<Dictionary<string, object>> UpdatePOVGemsPostStatusAsync(object JsonObj)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            string toAddress = string.Empty;
            string subject = string.Empty;
            string body = string.Empty;
            string response = string.Empty;
            string fromAddress = string.Empty;

            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "updatePOVPostStatus");
                var rowInfo = JsonConvert.DeserializeObject<List<POVerficationListItem>>(jsonVal[0]);

                if (string.IsNullOrEmpty(rowInfo[0].sessionname) || rowInfo[0].sessionid == 0)
                {
                    result.Add("message", "SessionName & SessionID are mandatory to proceed further!");
                    return result;
                }

                result = await _poVerificationDataService.UpdatePOVGemsPostStatusAsync(rowInfo);
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }
        //added by Sandip on  27th Sept 2019 for RFC0021956 -- This api will be called from GEMS to update the PDFName against session name.
        public async Task<Dictionary<string, object>> UpdatePOVGemsPDFNameAsync(object JsonObj)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            string toAddress = string.Empty;
            string subject = string.Empty;
            string body = string.Empty;
            string fromAddress = string.Empty;

            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "updatePOVPDFName");
                var rowInfo = JsonConvert.DeserializeObject<List<POVerficationListItem>>(jsonVal[0]);

                if (string.IsNullOrEmpty(rowInfo[0].sessionname) || rowInfo[0].sessionid == 0)
                {
                    result.Add("message", "SessionName & SessionID are mandatory to proceed further!");
                    return result;
                }

                result = await _poVerificationDataService.UpdatePOVGemsPDFNameAsync(rowInfo);
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }
        //added by Sandip on  5th Nov 2019 for RFC0021956 -- This api will be called from GEMS to update the MEA Editor details for each session ids.
        public async Task<Dictionary<string, object>> UpdatePOVGemsMEAIDAsync(object JsonObj)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                string results = string.Empty;
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "updatePOVMEAID");
                var rowInfo = JsonConvert.DeserializeObject<List<POVerficationListItem>>(jsonVal[0]);
                result = await _poVerificationDataService.UpdatePOVGemsMEAIDAsync(rowInfo);

                //RFC0031224 -- commented by Sandip on 17/12/2020, as this email is not required and seperate email is triggered from GEMS Subtask.

                /*string adminURL = System.Configuration.ConfigurationManager.AppSettings["AdminURL"];
                 
                if (result.Values.Contains("Success"))
                {
                    string toAddress = string.Empty;
                    string subject = string.Empty;
                    string body = string.Empty;
                    string response = string.Empty;
                    string image = string.Empty;

                    string fromAddress = System.Configuration.ConfigurationManager.AppSettings["EmailFromAddress"];
                    toAddress = rowInfo[0].meaeditoremail;

                    subject = "As-Built project assigned to MEA.";
                    body = "Dear Sir/Mam, @@Session Name - '" + rowInfo[0].sessionname + "' has been assigned to you for verification." +
                           "@@For more details, please click - " + adminURL + " @@Thank You";

                    response = await _commonUtilities.SendEmailMethod(fromAddress, toAddress, subject, body);
                }*/
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }
        //added by Sandip on  20th July 2021 for RFC0034115 -- This api will be called from GEMS, it will update totalfeatures, totallbfeatures, totallberror and qaqcpct. 
        public async Task<Dictionary<string, object>> UpdatePOVGemsQAQCPctAsync(object JsonObj)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                string results = string.Empty;
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "updatePOVQAQCPCT");
                var rowInfo = JsonConvert.DeserializeObject<List<POVerficationListItem>>(jsonVal[0]);
                result = await _poVerificationDataService.UpdatePOVGemsQAQCPctAsync(rowInfo);
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }
        /*public void WriteErrorLog(string message)
        {
            string _filePath = System.Configuration.ConfigurationManager.AppSettings["LogPath"];
            string content = DateTime.Now + Environment.NewLine + message + Environment.NewLine;
            System.IO.File.AppendAllText(_filePath, content);
        }*/
    }
}
