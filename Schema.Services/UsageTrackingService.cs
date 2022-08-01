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
using Schema.Core.Utilities;

namespace Schema.Services
{
    public class UsageTrackingService : IUsageTrackingService
    {
        ILoggingService _loggingService;
        IUsageTrackingDataService _usageTrackingDataService;
        Dictionary<string, object> errorLogInfo;
        CommonUtilities _commonUtilities = new CommonUtilities();
        public UsageTrackingService(ILoggingService LoggingService, IUsageTrackingDataService UsageTrackingDataService)
        {
            _loggingService = LoggingService;
            _usageTrackingDataService = UsageTrackingDataService;
        }
        public ICustomAuthorizeService CustomAuthorizeService
        {
            get
            {
                return DependencyResolver.Current.GetService<ICustomAuthorizeService>();
            }
        }
        //added by Sandip on 9th April 2019 for RFC0018439 -- This api returns the list of users reporting under the logged in manager 
        //and their thresold limit to access the Schema application.
        //added by Sandip on 8th Jan 2020 for RFC0023594 -- API modified to allow ReportOfficerID as an input parameter
        public async Task<HashSet<Dictionary<string, object>>> GetUsageTrackingUserThresholdAync(string ReportOfficerID)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _usageTrackingDataService.GetUsageTrackingUserThresholdAync(ReportOfficerID);
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
        //added by Sandip on 17th April 2019 for RFC0018439 -- This api returns the section wise thresold limit for accessing the Schema application.
        //added by Sandip on 8th Jan 2020 for RFC0023594 -- API modified to allow ReportOfficerID as an input parameter
        public async Task<HashSet<Dictionary<string, object>>> GetUsageTrackingSectionThresholdAync(string ReportOfficerID)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _usageTrackingDataService.GetUsageTrackingSectionThresholdAync(ReportOfficerID);
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
        //added by Sandip on 22nd April 2019 for RFC0018439 -- This api returns the list of those users only who have accessed Schema application for logged in manager
        //added by Sandip on 19th Aug 2019 for RFC0020665 -- Added FromDate and ToDate parameters
        public async Task<HashSet<Dictionary<string, object>>> GetUsageTrackingSchemaUserAync(string ReportOfficerID, string FromDate, string ToDate)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _usageTrackingDataService.GetUsageTrackingSchemaUserAync(ReportOfficerID, FromDate, ToDate);
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
        //added by Sandip on 14th May 2019 for RFC0018439 -- This api returns Total Accessible, Unaccessible, Blocked and Unblocked users count for logged in manager
        //added by Sandip on 19th Aug 2019 for RFC0020665 -- Added FromDate and ToDate parameters
        public async Task<HashSet<Dictionary<string, object>>> GetUsageTrackingSummaryAsync(string ReportOfficerID, string FromDate, string ToDate)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _usageTrackingDataService.GetUsageTrackingSummaryAsync(ReportOfficerID, FromDate, ToDate);
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
        //added by Sandip on 15th May 2019 for RFC0018439 -- This api returns map sheet information accessed by user in both GIS and SLD
        //added by Sandip on 19th Aug 2019 for RFC0020665 -- Added FromDate and ToDate parameters
        public async Task<HashSet<Dictionary<string, object>>> GetUsageTrackingMapSheetsAsync(string UserID, string FromDate, string ToDate)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _usageTrackingDataService.GetUsageTrackingMapSheetsAsync(UserID, FromDate, ToDate);
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
        //added by Sandip on 7th May 2019 for RFC0018439 -- This api returns the list of Dormant Users who have not accessed the Schema application more than 90 days.
        public async Task<HashSet<Dictionary<string, object>>> GetUsageTrackingDormantUserAync(string ReportOfficerID)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _usageTrackingDataService.GetUsageTrackingDormantUserAync(ReportOfficerID);
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
        //added by Sandip on 3rd May 2019 for RFC0018439 -- Unblocks blocked Schema users. 
        public async Task<Dictionary<string, object>> UpdateUsageTrackingUnBlockUserAsync(object JsonObj, string ReportOfficerID)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "updateUCONUnBlockUser");
                var rowInfo = JsonConvert.DeserializeObject<List<UCONListItem>>(jsonVal[0]);

                result = await _usageTrackingDataService.UpdateUsageTrackingUnBlockUserAsync(rowInfo, ReportOfficerID);

                //Sending Email
                //Get Email IDs of the immediate and hierarchy managers
                string toAddress = string.Empty;
                HashSet<Dictionary<string, object>> emailID = await _usageTrackingDataService.GetUserEmailIDAsync(rowInfo[0].userid);

                foreach (Dictionary<string, object> item in (IEnumerable)emailID)
                {
                    if (item["email"] != null)
                        toAddress = item["email"].ToString();
                }

                string fromAddress = System.Configuration.ConfigurationManager.AppSettings["EmailFromAddress"];
                string subject = "Account Unblock Notification.";
                string body = "Dear Sir/Mam, @@Your account is unblocked! You can now access the Schema application. @@Thank you,";
                bool uconStatsSendEmail = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["UCONStatsSendEmail"]);
                string response = string.Empty;
                if (uconStatsSendEmail == true)
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
        //added by Sandip on 5th May 2019 for RFC0018439 -- Updates new user thresold value
        public async Task<Dictionary<string, object>> UpdateUsageTrackingUserThresholdAsync(object JsonObj, string ReportOfficerID, int isITD)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "updateUCONUserThreshold");
                var rowInfo = JsonConvert.DeserializeObject<List<UCONListItem>>(jsonVal[0]);

                result = await _usageTrackingDataService.UpdateUsageTrackingUserThresholdAsync(rowInfo, ReportOfficerID, isITD);
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
        //added by Sandip on 17th May 2019 for RFC0018439 -- Updates new section thresold value
        public async Task<Dictionary<string, object>> UpdateUsageTrackingSectionThresholdAsync(object JsonObj, string ReportOfficerID, int isITD)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "updateUCONSectionThreshold");
                var rowInfo = JsonConvert.DeserializeObject<List<UCONListItem>>(jsonVal[0]);

                result = await _usageTrackingDataService.UpdateUsageTrackingSectionThresholdAsync(rowInfo, ReportOfficerID, isITD);
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
        //added by Sandip on 22th May 2019 for RFC0018439 -- Deletes the updated user threshold value and resets it back to original thresold limit.
        public async Task<Dictionary<string, object>> DeleteUsageTrackingUserThresholdAsync(object JsonObj, string ReportOfficerID, int isITD)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "deleteUCONUserThreshold");
                var rowInfo = JsonConvert.DeserializeObject<List<UCONListItem>>(jsonVal[0]);

                result = await _usageTrackingDataService.DeleteUsageTrackingUserThresholdAsync(rowInfo, ReportOfficerID, isITD);
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
        //added by Sandip on 24th May 2019 for RFC0018439 -- Get complete list of Schema batch jobs and their status
        public async Task<HashSet<Dictionary<string, object>>> GetUsageTrackingJobInfoAync()
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _usageTrackingDataService.GetUsageTrackingJobInfoAync();
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
        //added by Sandip on 25th May 2019 for RFC0018439 -- Resets the 'PROCESSING' or 'FAIL' batch job status to 'PENDING'
        public async Task<Dictionary<string, object>> UpdateUsageTrackingJobInfoAsync()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                result = await _usageTrackingDataService.UpdateUsageTrackingJobInfoAsync();
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
        //added by Sandip on 19th Aug 2019 for RFC0020665 -- This api returns list of usernames on click of each type in summary of Schema Stats.
        public async Task<HashSet<Dictionary<string, object>>> GetUsageTrackingListOfUsernameAsync(string ReportOfficerID, Int16 Type, string FromDate, string ToDate, Int16 DirectFlag = 0)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _usageTrackingDataService.GetUsageTrackingListOfUsernameAsync(ReportOfficerID, Type, FromDate, ToDate, DirectFlag);
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
        public async Task<HashSet<Dictionary<string, object>>> GetUsageTrackingBlockedUserHistoryAsync(string ReportOfficerID, Int16 Type, Int16 DirectFlag = 0)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _usageTrackingDataService.GetUsageTrackingBlockedUserHistoryAsync(ReportOfficerID, Type, DirectFlag);
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
