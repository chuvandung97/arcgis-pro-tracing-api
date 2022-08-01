using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Schema.Core.Data;
using Schema.Core.Models;
using Schema.Core.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Schema.Core.Utilities;

namespace Schema.Services
{
    public class OWTSIRService : IOWTSIRService
    {
        ILoggingService _loggingService;
        IOWTSIRDataService _owtsirDataService;
        Dictionary<string, object> errorLogInfo;
        CommonUtilities _commonUtilities = new CommonUtilities();

        public OWTSIRService(ILoggingService LoggingService, IOWTSIRDataService OWTSIRDataService)
        {
            _loggingService = LoggingService;
            _owtsirDataService = OWTSIRDataService;
        }
        public ICustomAuthorizeService CustomAuthorizeService
        {
            get
            {
                return DependencyResolver.Current.GetService<ICustomAuthorizeService>();
            }
        }
        public async Task<HashSet<Dictionary<string, object>>> GetCompletedTasksAync(string Officer, string FromDate, string ToDate)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _owtsirDataService.GetCompletedTasksAync(Officer, FromDate, ToDate);
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
        public async Task<HashSet<Dictionary<string, object>>> GetPendingItemsAync(string Username)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _owtsirDataService.GetPendingItemsAync(Username);
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
        public async Task<HashSet<Dictionary<string, object>>> GetReadingDetailsAsync(string ID)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _owtsirDataService.GetReadingDetailsAsync(ID);
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
        public async Task<HashSet<Dictionary<string, object>>> GetPrevReadingsAsync(string SrcStationName, string SrcStationBoardNo, string SrcStationPanelNo, string TgtStationName, string TgtStationBoardNo, string TgtStationPanelNo)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _owtsirDataService.GetPrevReadingsAsync(SrcStationName, SrcStationBoardNo, SrcStationPanelNo, TgtStationName, TgtStationBoardNo, TgtStationPanelNo);
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
        public async Task<HashSet<Dictionary<string, object>>> GetReadingWithinExtentAsync(string Geometry)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _owtsirDataService.GetReadingWithinExtentAsync(Geometry);
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
        public async Task<HashSet<Dictionary<string, object>>> GetOWTSIREditors()
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _owtsirDataService.GetOWTSIREditors();
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
        public async Task<HashSet<Dictionary<string, object>>> GetApprovalOfficersAsync(string Username)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _owtsirDataService.GetApprovalOfficersAsync(Username);
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
        public async Task<Dictionary<string, object>> ApprovalProcessAsync(object JsonObj, string approverName)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            HashSet<Dictionary<string, object>> getFeederInfo = new HashSet<Dictionary<string, object>>();

            try
            {
                int statusCode = 0;
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "approvalProcess");
                var rowInfo = JsonConvert.DeserializeObject<List<OWTSIRProcessItem>>(jsonVal[0]);
                string ID = rowInfo[0].ID;
                string approvalStatus = rowInfo[0].approvalStatus;
                string approverRemarks = rowInfo[0].approverRemarks;
                //string approverName = rowInfo[0].approverName;

                if (approvalStatus.ToUpper() == "APPROVE")
                    statusCode = 1;
                else if (approvalStatus.ToUpper() == "REJECT")
                    statusCode = 2;
                result = await _owtsirDataService.ApprovalProcessAsync(ID, statusCode, approverRemarks);

                // code to get the source station name and target station name based on ID
                getFeederInfo = await _owtsirDataService.GetReadingDetailsAsync(ID);
                string srcStnName = string.Empty;
                string tgtStnName = string.Empty;

                foreach (Dictionary<string, object> item in (IEnumerable)getFeederInfo)
                {
                    if (item["srcstationname"] != null)
                        srcStnName = item["srcstationname"].ToString();
                    if (item["tgtstationname"] != null)
                        tgtStnName = item["tgtstationname"].ToString();
                }
                //email code
                string toAddress = string.Empty;
                string subject = string.Empty;
                string body = string.Empty;
                string response = string.Empty;

                HashSet<Dictionary<string, object>> emailID = await _owtsirDataService.GetEmailIDAsync("", Convert.ToInt64(ID));
                foreach (Dictionary<string, object> item in (IEnumerable)emailID)
                {
                    if (item["emails"] != null)
                        toAddress = item["emails"].ToString();
                }
                string fromAddress = System.Configuration.ConfigurationManager.AppSettings["EmailFromAddress"];

                if (statusCode == 1)
                {
                    subject = "OWTS/IR reading approved: SOURCE: '" + srcStnName + "' <-> TARGET: '" + tgtStnName + "'";
                    body = "Dear Sir/Mam, @@OWTS/IR reading has been approved successfully by " + approverName + ".  @@Thank You";
                }
                else if (statusCode == 2)
                {
                    subject = "OWTS/IR reading rejected: SOURCE: '" + srcStnName + "' <-> TARGET: '" + tgtStnName + "'";
                    body = "Dear Sir/Mam, @@OWTS/IR reading has been rejected by " + approverName + ".  @@Thank You";
                }
                response = await _commonUtilities.SendEmailMethod(fromAddress, toAddress, subject, body);

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
        public async Task<Dictionary<string, object>> AddReadingAsync(object JsonObj, string Username)
        {
            //WriteErrorLog(JsonObj.ToString());
            string approverName = string.Empty;
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "addReading");
                var rowInfo = JsonConvert.DeserializeObject<List<OWTSIRItem>>(jsonVal[0]);
                approverName = rowInfo[0].officer;
                string srcStnName = rowInfo[0].srcstationname;
                string tgtStnName = rowInfo[0].tgtstationname;

                result = await _owtsirDataService.AddReadingAsync(rowInfo, Username);

                //email code
                string toAddress = string.Empty;
                HashSet<Dictionary<string, object>> emailID = await _owtsirDataService.GetEmailIDAsync(approverName, 0);
                foreach (Dictionary<string, object> item in (IEnumerable)emailID)
                {
                    if (item["emails"] != null)
                        toAddress = item["emails"].ToString();
                }

                string fromAddress = System.Configuration.ConfigurationManager.AppSettings["EmailFromAddress"];
                string subject = "OWTS/IR reading pending approval: SOURCE: '" + srcStnName + "' <-> TARGET: '" + tgtStnName + "'";
                string body = "Dear Sir/Mam, @@There is a OWTS/IR reading that requires your approval. Kindly verify under SCHEMA -> OWTS/IR -> Pending Tasks and proceed for approval.  @@Thank You";                
                string response = await _commonUtilities.SendEmailMethod(fromAddress, toAddress, subject, body);
               
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
        public async Task<Dictionary<string, object>> UpdateReadingAsync(object JsonObj, string Username)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "updateReading");
                var rowInfo = JsonConvert.DeserializeObject<List<OWTSIRItem>>(jsonVal[0]);

                result = await _owtsirDataService.UpdateReadingAsync(rowInfo, Username);
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
