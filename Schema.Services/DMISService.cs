using Schema.Core.Data;
using Schema.Core.Models;
using Schema.Core.Services;
using Schema.Core.Utilities;
using Schema.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data;
using System.Collections;
using System.Web.Mvc;
using System.Net;
using System.IO;
using Schema.Core.Utilities;

namespace Schema.Services
{
    public class DMISService : IDMISService
    {
        ILoggingService _loggingService;
        IDMISDataService _dmisDataService;
        Dictionary<string, object> errorLogInfo;
        CommonUtilities _commonUtilities = new CommonUtilities();
        public DMISService(ILoggingService LoggingService, IDMISDataService DMISDataService)
        {
            _loggingService = LoggingService;
            _dmisDataService = DMISDataService;
        }
        public ICustomAuthorizeService CustomAuthorizeService
        {
            get
            {
                return DependencyResolver.Current.GetService<ICustomAuthorizeService>();
            }
        }
        public async Task<Dictionary<string, object>> CreateDMISPointAsync(object JsonObj, string Username)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "createDMISPoint");
                var rowInfo = JsonConvert.DeserializeObject<List<DMISGasLeakItem>>(jsonVal[0]);
                string JobID = await _dmisDataService.GetDMISJobID();

                result = await _dmisDataService.CreateDMISPointAsync(rowInfo, JobID, Username);
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
        public async Task<HashSet<Dictionary<string, object>>> GetDMISEditorsAsync()
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _dmisDataService.GetDMISEditorsAsync();
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
                result = await _dmisDataService.GetApprovalOfficersAsync(Username);
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
        public async Task<HashSet<Dictionary<string, object>>> GetDMISPointsAsync(string Username)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _dmisDataService.GetDMISPointsAsync(Username);
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
        public async Task<SchemaResult> GetDMISpointsWithinExtentAsync(string Geometry, string Username)
        {
            SchemaResult result = new SchemaResult();
            HashSet<Dictionary<string, object>> dmisResult = new HashSet<Dictionary<string, object>>();
            try
            {
                dmisResult = await _dmisDataService.GetDMISpointsWithinExtentAsync(Geometry, Username);

                if (dmisResult.Count > 1)
                {
                    result.Result = null;
                    result.Message = "Select only one DMIS gas leak point to proceed.";
                }
                else if (dmisResult.Count == 0)
                {
                    result.Result = null;
                    result.Message = "No DMIS gas leak point found.";
                }
                else
                {
                    string OID = string.Empty;
                    foreach (Dictionary<string, object> item in (IEnumerable)dmisResult)
                    {
                        if (item["oid"] != null)
                            OID = item["oid"].ToString();
                        break;
                    }
                    dmisResult = await _dmisDataService.GetDMISPointDetailsAsync(OID);
                    result.Result = dmisResult;
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
        public async Task<HashSet<Dictionary<string, object>>> GetDMISPointDetailsAsync(string OID)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _dmisDataService.GetDMISPointDetailsAsync(OID);
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
        public async Task<Dictionary<string, object>> SendToApproverAsync(object JsonObj)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "sendToApprover");
                var rowInfo = JsonConvert.DeserializeObject<List<DMISProcessItem>>(jsonVal[0]);
                string objectID = rowInfo[0].ObjectID;
                Int64[] oidArr = Array.ConvertAll(objectID.Split(','), Int64.Parse);
                string approverName = rowInfo[0].approverName;
                string jobIDs = rowInfo[0].jobIDs;

                result = await _dmisDataService.SendToApproverAsync(oidArr, approverName);

                //email code
                string toAddress = string.Empty;
                HashSet<Dictionary<string, object>> emailID = await _dmisDataService.GetEmailIDAsync(approverName, 0);
                foreach (Dictionary<string, object> item in (IEnumerable)emailID)
                {
                    if (item["emails"] != null)
                        toAddress = item["emails"].ToString();
                }
                string fromAddress = System.Configuration.ConfigurationManager.AppSettings["EmailFromAddress"];
                string subject = "DMIS point(s) sent for approval.";
                string body = "Dear Sir/Mam, @@There are some DMIS point(s) with Job ID(s) '" + jobIDs + "' that requires your approval. Kindly verify and proceed for approval. @@Thank You";
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
        public async Task<Dictionary<string, object>> ApprovalProcessAsync(object JsonObj, string Username, string ApproverName)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                int statusCode = 0;
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "approvalProcess");
                var rowInfo = JsonConvert.DeserializeObject<List<DMISProcessItem>>(jsonVal[0]);
                //string approverName = Username;
                string objectID = rowInfo[0].ObjectID;
                string approvalStatus = rowInfo[0].approvalStatus;
                string approverRemarks = rowInfo[0].approverRemarks;
                string assignedEditor = rowInfo[0].assignedEditor;
                string jobID = rowInfo[0].jobIDs;

                if (approvalStatus.ToUpper() == "APPROVE")
                {
                    statusCode = 2;
                    Username = "";
                }
                else if (approvalStatus.ToUpper() == "REJECT")
                {
                    if (!string.IsNullOrEmpty(assignedEditor))
                        Username = assignedEditor;

                    statusCode = 3;
                }
                result = await _dmisDataService.ApprovalProcessAsync(objectID, statusCode, approverRemarks, Username);

                //email code
                string toAddress = string.Empty;
                string subject = string.Empty;
                string body = string.Empty;
                string response = string.Empty;
                HashSet<Dictionary<string, object>> emailID = await _dmisDataService.GetEmailIDAsync(Username, Convert.ToInt64(objectID));
                foreach (Dictionary<string, object> item in (IEnumerable)emailID)
                {
                    if (item["emails"] != null)
                        toAddress = item["emails"].ToString();
                }
                string fromAddress = System.Configuration.ConfigurationManager.AppSettings["EmailFromAddress"];
                              
                if (statusCode == 2)
                {
                    subject = "DMIS point approved.";
                    body = "Dear Sir/Mam, @@DMIS point with Job ID '" + jobID + "' has been approved successfully by " + ApproverName + ".  @@Thank You";
                }
                else if (statusCode == 3)
                {
                    subject = "DMIS point rejected.";
                    body = "Dear Sir/Mam, @@DMIS point with Job ID '" + jobID + "' has been rejected by " + ApproverName + ".  @@Thank You";
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
        public async Task<Dictionary<string, object>> UpdateDMISPointAsync(object JsonObj, string Username)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "updateDMISPoint");
                var rowInfo = JsonConvert.DeserializeObject<List<DMISGasLeakItem>>(jsonVal[0]);
                int objectID = rowInfo[0].objectid;

                result = await _dmisDataService.UpdateDMISPointAsync(rowInfo, objectID, Username);
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
        public async Task<Dictionary<string, object>> DeleteDMISPointAsync(object JsonObj)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "deleteDMISPoint");
                var rowInfo = JsonConvert.DeserializeObject<List<DMISProcessItem>>(jsonVal[0]);
                string objectID = rowInfo[0].ObjectID;


                result = await _dmisDataService.DeleteDMISPointAsync(objectID);
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
        public async Task<HashSet<Dictionary<string, object>>> GetPointSnaptoGaslineAsync(double X, double Y, string Layer)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _dmisDataService.GetPointSnaptoGaslineAsync(X, Y, Layer);
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
    }
}
