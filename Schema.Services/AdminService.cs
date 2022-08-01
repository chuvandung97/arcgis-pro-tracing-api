using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Schema.Core.Data;
using Schema.Core.Models;
using Schema.Core.Services;
using Schema.Core.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Schema.Services
{
    public class AdminService : IAdminService
    {

        ILoggingService _loggingService;
        IAdminDataService _adminDataService;

        Dictionary<string, object> errorLogInfo;
        CommonUtilities _commonUtilities = new CommonUtilities();
        public AdminService(ILoggingService LoggingService, IAdminDataService AdminDataService)
        {
            _loggingService = LoggingService;
            _adminDataService = AdminDataService;
        }
        public ICustomAuthorizeService CustomAuthorizeService
        {
            get
            {
                return DependencyResolver.Current.GetService<ICustomAuthorizeService>();
            }
        }
        public async Task<Dictionary<string, object>> GetHitCountAsync()
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();

            Dictionary<string, object> finalResult = new Dictionary<string, object>();

            string mapServiceCount = string.Empty;
            string functionUsageCount = string.Empty;
            string loginCount = string.Empty;
            string errorCount = string.Empty;
            try
            {
                //Get Map Service Count
                result = await _adminDataService.GetMapServiceCount();
                foreach (Dictionary<string, object> item in (IEnumerable)result)
                {
                    if (item["hitcount"] != null)
                        mapServiceCount = item["hitcount"].ToString();
                }
                finalResult.Add("serviceCount", mapServiceCount);

                // Get Function Count
                result = new HashSet<Dictionary<string, object>>();
                result = await _adminDataService.GetFunctionUsageCount();
                foreach (Dictionary<string, object> item in (IEnumerable)result)
                {
                    if (item["hitcount"] != null)
                        functionUsageCount = item["hitcount"].ToString();
                }
                finalResult.Add("functionCount", functionUsageCount);

                //Get Login Count 
                result = new HashSet<Dictionary<string, object>>();
                result = await _adminDataService.GetLoginCount();
                foreach (Dictionary<string, object> item in (IEnumerable)result)
                {
                    if (item["hitcount"] != null)
                        loginCount = item["hitcount"].ToString();
                }
                finalResult.Add("loginCount", loginCount);

                //Get Error Count 
                result = new HashSet<Dictionary<string, object>>();
                result = await _adminDataService.GetErrorCount();
                Int32 hitCount = 0;
                foreach (Dictionary<string, object> item in (IEnumerable)result)
                {

                    if (item["hitcount"] != null)
                    {
                        Int32 count = Convert.ToInt32(item["hitcount"]);
                        hitCount = hitCount + count;
                    }
                }
                errorCount = hitCount.ToString();
                finalResult.Add("errorCount", errorCount);
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
            return finalResult;
        }
        public async Task<HashSet<Dictionary<string, object>>> GetWeeklyLoginActivityAsync()
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _adminDataService.GetWeeklyLoginActivityAsync();
                foreach (Dictionary<string, object> item in (IEnumerable)result)
                {
                    if (item.ContainsKey("sn"))
                        item.Remove("sn");

                    if (item.ContainsKey("usercount"))
                        item.Remove("usercount");
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
        public async Task<HashSet<Dictionary<string, object>>> GetEachMapServiceUsageAsync()
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _adminDataService.GetEachMapServiceUsageAsync();

                foreach (Dictionary<string, object> item in (IEnumerable)result)
                {
                    if (item.ContainsKey("sn"))
                        item.Remove("sn");

                    if (item.ContainsKey("counterfromdate"))
                        item.Remove("counterfromdate");

                    if (item.ContainsKey("countertodate"))
                        item.Remove("countertodate");

                    if (item.ContainsKey("usercount"))
                        item.Remove("usercount");
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
        public async Task<HashSet<Dictionary<string, object>>> GetEachFunctionUsageAync()
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _adminDataService.GetEachFunctionUsageAync();

                foreach (Dictionary<string, object> item in (IEnumerable)result)
                {
                    if (item.ContainsKey("sn"))
                        item.Remove("sn");

                    if (item.ContainsKey("counterfromdate"))
                        item.Remove("counterfromdate");

                    if (item.ContainsKey("countertodate"))
                        item.Remove("countertodate");

                    if (item.ContainsKey("usercount"))
                        item.Remove("usercount");
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
        public async Task<HashSet<Dictionary<string, object>>> GetEachErrorUsageAsync()
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _adminDataService.GetEachErrorUsageAsync();
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
        public async Task<HashSet<Dictionary<string, object>>> GetDailyUserActivitiesAsync(int Year, int Month)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _adminDataService.GetDailyUserActivitiesAsync(Year, Month);
                foreach (Dictionary<string, object> item in (IEnumerable)result)
                {
                    if (item.ContainsKey("sn"))
                        item.Remove("sn");
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
        public async Task<HashSet<Dictionary<string, object>>> GetBrowserActivitiesAsync(int Year, int Month)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _adminDataService.GetBrowserActivitiesAsync(Year, Month);
                foreach (Dictionary<string, object> item in (IEnumerable)result)
                {
                    if (item.ContainsKey("sn"))
                        item.Remove("sn");

                    if (item.ContainsKey("usercount"))
                        item.Remove("usercount");
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
        public async Task<Dictionary<string, object>> InsertConfigUrlMappingAsync(object JsonObj)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "insertNewUrlKey");
                var rowInfo = JsonConvert.DeserializeObject<List<APIManagementItems>>(jsonVal[0]);

                result = await _adminDataService.InsertConfigUrlMappingAsync(rowInfo[0].URLKey, rowInfo[0].ServiceType, rowInfo[0].URL, rowInfo[0].IsTracking);

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
        public async Task<Dictionary<string, object>> UpdateConfigUrlMappingAsync(object JsonObj)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "updateExistingUrlKey");
                var rowInfo = JsonConvert.DeserializeObject<List<APIManagementItems>>(jsonVal[0]);

                result = await _adminDataService.UpdateConfigUrlMappingAsync(rowInfo[0].URLKey, rowInfo[0].ServiceType, rowInfo[0].URL, rowInfo[0].IsTracking);

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
        public async Task<Dictionary<string, object>> DeleteConfigUrlMappingAsync(object JsonObj)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "deleteUrlKey");
                var rowInfo = JsonConvert.DeserializeObject<List<APIManagementItems>>(jsonVal[0]);

                result = await _adminDataService.DeleteConfigUrlMappingAsync(rowInfo[0].URLKey);

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
        public async Task<HashSet<Dictionary<string, object>>> GetUrlKeysFromConfigURLAsync()
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _adminDataService.GetUrlKeysFromConfigURLAsync();

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
        public async Task<SchemaResult> InsertACMAPIAsync(object JsonObj)
        {
            SchemaResult finalResult = new SchemaResult();
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "insertACMAPI");
                var rowInfo = JsonConvert.DeserializeObject<List<APIManagementItems>>(jsonVal[0]);

                result = await _adminDataService.CheckIfAPIExists(rowInfo[0].FunctionName.ToUpper());

                Int32 count = new Int32();
                count = Convert.ToInt32(result["APICount"]);
                if (count > 0)
                {
                    result = await _adminDataService.InsertACMAPIAsync(rowInfo[0].Module, rowInfo[0].FunctionName);
                    finalResult.Result = result;
                }
                else
                {
                    finalResult.Result = null;
                    finalResult.Message = "No such API Name exists in Config URLs table!";
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
            return finalResult;
        }
        public async Task<SchemaResult> UpdateACMAPIAsync(object JsonObj)
        {
            SchemaResult finalResult = new SchemaResult();
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "updateACMAPI");
                var rowInfo = JsonConvert.DeserializeObject<List<APIManagementItems>>(jsonVal[0]);

                result = await _adminDataService.CheckIfAPIExists(rowInfo[0].FunctionName.ToUpper());

                Int32 count = new Int32();
                count = Convert.ToInt32(result["APICount"]);
                if (count > 0)
                {
                    result = await _adminDataService.UpdateACMAPIAsync(rowInfo[0].APIKey, rowInfo[0].Module, rowInfo[0].FunctionName);
                    finalResult.Result = result;
                }
                else
                {
                    finalResult.Result = null;
                    finalResult.Message = "No such API Name exists in Config URLs table!";
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
            return finalResult;
        }
        public async Task<Dictionary<string, object>> DeleteACMAPIAsync(object JsonObj)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "deleteACMAPI");
                var rowInfo = JsonConvert.DeserializeObject<List<APIManagementItems>>(jsonVal[0]);

                result = await _adminDataService.DeleteACMAPIAsync(rowInfo[0].APIKey);

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
        public async Task<HashSet<Dictionary<string, object>>> GetAPINameAsync()
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _adminDataService.GetAPINameAsync();
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
        public async Task<Dictionary<string, object>> InsertACMFunctionalityAsync(object JsonObj)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "insertACMFunctionality");
                var rowInfo = JsonConvert.DeserializeObject<List<APIManagementItems>>(jsonVal[0]);

                result = await _adminDataService.InsertACMFunctionalityAsync(rowInfo[0].Module, rowInfo[0].Functionality);
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
        public async Task<Dictionary<string, object>> UpdateACMFunctionalityAsync(object JsonObj)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "updateACMFunctionality");
                var rowInfo = JsonConvert.DeserializeObject<List<APIManagementItems>>(jsonVal[0]);

                result = await _adminDataService.UpdateACMFunctionalityAsync(rowInfo[0].FunctionalityKey, rowInfo[0].Module, rowInfo[0].Functionality);
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
        public async Task<Dictionary<string, object>> DeleteACMFunctionalityAsync(object JsonObj)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "deleteACMFunctionality");
                var rowInfo = JsonConvert.DeserializeObject<List<APIManagementItems>>(jsonVal[0]);

                result = await _adminDataService.DeleteACMFunctionalityAsync(rowInfo[0].FunctionalityKey);
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
        public async Task<HashSet<Dictionary<string, object>>> GetACMModuleNameAsync()
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _adminDataService.GetACMModuleNameAsync();
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
        public async Task<SchemaResult> InsertACMADGroupAsync(object JsonObj)
        {
            SchemaResult finalResult = new SchemaResult();
            Dictionary<string, object> result = new Dictionary<string, object>();
            string ADGroup = string.Empty;
            string Role = string.Empty;

            /*var httpContext = HttpContext.Current;
            IPrincipal principal = httpContext.User as IPrincipal;
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "singaporepower.local");
            UserPrincipal user = UserPrincipal.FindByIdentity(ctx, httpContext.User.Identity.Name);
            int count = 0;            
            //added by sandip on 5th Nov 2019. This section is added to see whether the newly created ADGroup exists or not.
            /*PrincipalSearchResult<Principal> groups = user.GetGroups();
            foreach (Principal p in groups)
            {
                if (p is GroupPrincipal)
                {
                    if (p.Name.ToUpper().Contains(ADGroup.ToUpper()))
                    {
                        count = 1;
                        break;
                    }
                }
            }*/
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "insertACMADGroup");
                var rowInfo = JsonConvert.DeserializeObject<List<APIManagementItems>>(jsonVal[0]);

                ADGroup = rowInfo[0].ADGroup;
                Role = rowInfo[0].Role;

                if (!string.IsNullOrEmpty(ADGroup))
                {
                    if (ADGroup.Contains("SINGAPOREPOWER\\"))
                        ADGroup = ADGroup.Replace("SINGAPOREPOWER\\", "");
                }

                result = await _adminDataService.CheckIfADGroupExists(ADGroup.ToUpper());
                Int32 count = new Int32();
                count = Convert.ToInt32(result["ADGroupCount"]);

                if (count > 0)
                {
                    result = await _adminDataService.CheckIfADGroupExistsInACMADGroup(ADGroup.ToUpper());
                    count = Convert.ToInt32(result["ACMADGroupCount"]);

                    if (count > 0)
                    {
                        finalResult.Result = null;
                        finalResult.Message = "ADGroup already exists!";
                    }
                    else
                    {
                        result = await _adminDataService.InsertACMADGroupAsync(ADGroup, Role);
                        finalResult.Result = result;
                    }
                }
                else
                {
                    finalResult.Result = null;
                    finalResult.Message = "No such ADGroup exists!";
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
            return finalResult;
        }
        public async Task<Dictionary<string, object>> UpdateACMADGroupAsync(object JsonObj)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            string ADGroup = string.Empty;
            string Role = string.Empty;
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "updateACMADGroup");
                var rowInfo = JsonConvert.DeserializeObject<List<APIManagementItems>>(jsonVal[0]);

                ADGroup = rowInfo[0].ADGroup;
                Role = rowInfo[0].Role;

                if (ADGroup.Contains("SINGAPOREPOWER\\"))
                    ADGroup = ADGroup.Replace("SINGAPOREPOWER\\", "");

                result = await _adminDataService.UpdateACMADGroupAsync(rowInfo[0].ADGroupKey, ADGroup, Role);

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
        public async Task<Dictionary<string, object>> DeleteACMADGroupAsync(object JsonObj)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "deleteACMADGroup");
                var rowInfo = JsonConvert.DeserializeObject<List<APIManagementItems>>(jsonVal[0]);

                result = await _adminDataService.DeleteACMADGroupAsync(rowInfo[0].ADGroupKey);
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
        public async Task<HashSet<Dictionary<string, object>>> GetACMADGroupAsync()
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _adminDataService.GetACMADGroupAsync();
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
        public async Task<Dictionary<string, object>> InsertADGroupToFunctionMappingAsync(object JsonObj)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "insertADGrpToFuncMapping");
                var rowInfo = JsonConvert.DeserializeObject<List<APIManagementItems>>(jsonVal[0]);

                result = await _adminDataService.InsertADGroupToFunctionMappingAsync(rowInfo[0].ADGroupKey, rowInfo[0].FunctionalityMultipleKeys);
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                result.Add("Message", ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }
        public async Task<Dictionary<string, object>> DeleteADGroupToFunctionMappingAsync(object JsonObj)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "deleteADGrpToFuncMapping");
                var rowInfo = JsonConvert.DeserializeObject<List<APIManagementItems>>(jsonVal[0]);

                result = await _adminDataService.DeleteADGroupToFunctionMappingAsync(rowInfo[0].ADGroupKey, rowInfo[0].FunctionalityKey);

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
        public async Task<HashSet<Dictionary<string, object>>> GetADGroupToFunctionMappingAsync(int FunctionalityKey, int ADGroupKey)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _adminDataService.GetADGroupToFunctionMappingAsync(FunctionalityKey, ADGroupKey);

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
        public async Task<Dictionary<string, object>> InsertADGroupToAPIMappingAsync(object JsonObj)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "insertADGrpToAPIMapping");
                var rowInfo = JsonConvert.DeserializeObject<List<APIManagementItems>>(jsonVal[0]);

                result = await _adminDataService.InsertADGroupToAPIMappingAsync(rowInfo[0].ADGroupKey, rowInfo[0].APIMultipleKeys);

            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                result.Add("Message", ex.Message);
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);

                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }
        public async Task<Dictionary<string, object>> DeleteADGroupToAPIMappingAsync(object JsonObj)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "deleteADGrpToAPIMapping");
                var rowInfo = JsonConvert.DeserializeObject<List<APIManagementItems>>(jsonVal[0]);

                result = await _adminDataService.DeleteADGroupToAPIMappingAsync(rowInfo[0].ADGroupKey, rowInfo[0].APIKey);
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
        public async Task<HashSet<Dictionary<string, object>>> GetADGroupToAPIMappingAsync(int APIKey, int ADGroupKey)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _adminDataService.GetADGroupToAPIMappingAsync(APIKey, ADGroupKey);
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
        /*public async Task<HashSet<Dictionary<string, object>>> GetAPIMapSheetReportAsync(string InputDate, string ReportType, string Username)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _adminDataService.GetAPIMapSheetReportAsync(InputDate, ReportType, Username);
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
        }*/
        public async Task<HashSet<Dictionary<string, object>>> GetAPIMapSheetUserListAsync()
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _adminDataService.GetAPIMapSheetUserListAsync();
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
        //added by Sandip on 22nd Oct 2019 for RFC0021956 -- This api returns list of Project officers/MEA Editors based on UserType.
        public async Task<HashSet<Dictionary<string, object>>> GetPOMEAListAsync(string ProjectOfficerID, string UserType)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _adminDataService.GetPOMEAListAsync(ProjectOfficerID, UserType);
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
        //added by Sandip on 4th Sept 2019 for RFC0021956 -- This api returns complete list of PO JobInfo table. It is used in admin portal.
        public async Task<HashSet<Dictionary<string, object>>> GetCompletePOJobInfoDataAsync(string UserType, string OfficerID)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _adminDataService.GetCompletePOJobInfoDataAsync(UserType, OfficerID);
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

        //added by Sandip on 22nd Aug 2019 for RFC0021956 -- This api returns complete list of POVID's that are assigned to PO for verification. It is used in admin portal.
        public async Task<HashSet<Dictionary<string, object>>> GetAllPOVIDSToUpdatePOInfoAsync(string RejectSession = null)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _adminDataService.GetAllPOVIDSToUpdatePOInfoAsync(RejectSession);
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
        //added by Sandip on  14th Aug 2019 for RFC0021956 -- This api will be consumed from admin portal to change/update Project Officer ID for a particular POVID.
        public async Task<Dictionary<string, object>> UpdatePOIDAsync(object JsonObj)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                string results = string.Empty;
                string ccAddress = string.Empty;
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "updatePOID");
                var rowInfo = JsonConvert.DeserializeObject<List<POVerficationListItem>>(jsonVal[0]);
                result = await _adminDataService.UpdatePOIDAsync(rowInfo);

                if (result.Values.Contains("Success"))
                {
                    string toAddress = string.Empty;
                    string subject = string.Empty;
                    string body = string.Empty;
                    string response = string.Empty;
                    string image = string.Empty;

                    string fromAddress = System.Configuration.ConfigurationManager.AppSettings["EmailFromAddress"];
                    string adminURL = System.Configuration.ConfigurationManager.AppSettings["AdminURL"];
                    toAddress = rowInfo[0].poemail;
                    ccAddress = "SCHEMA.SUPPORT@cyient.com";

                    subject = "As-Built project assinged to PO.";
                    body = "Dear Sir/Mam, @@Session Name - '" + rowInfo[0].sessionname + "' has been assigned to you for verification. " +
                           "@@To check status or change assigned PO click on - " + adminURL + "?id=" + rowInfo[0].povid + " @@Thank You";

                    response = await _commonUtilities.SendEmailMethod(fromAddress, toAddress, subject, body, ccAddress);
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

        //added by Sandip on  22nd Sept 2019 for RFC0021956 -- This api will be consumed from admin portal to reject sessions by MEA.
        public async Task<Dictionary<string, object>> UpdateMEARejectSessionAsync(object JsonObj)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            string toAddress = string.Empty;
            string ccAddress = string.Empty;
            string subject = string.Empty;
            string body = string.Empty;
            string response = string.Empty;
            string fromAddress = string.Empty;

            try
            {
                string results = string.Empty;
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "updateMEARejectSession");
                var rowInfo = JsonConvert.DeserializeObject<List<POVerficationListItem>>(jsonVal[0]);
                result = await _adminDataService.UpdateMEARejectSessionAsync(rowInfo);

                if (result.Values.Contains("Success"))
                {
                    fromAddress = System.Configuration.ConfigurationManager.AppSettings["EmailFromAddress"];
                    toAddress = rowInfo[0].contractoremail;
                    ccAddress = rowInfo[0].meaeditoremail + "," + "SCHEMA.SUPPORT@cyient.com";
                    subject = "As-Built project has been rejected by MEA.";
                    body = "Dear Sir/Mam, @@Session Name - '" + rowInfo[0].sessionname + "' has been rejected by " + rowInfo[0].meaeditorname + " for further verificaton. @@Remarks - " + rowInfo[0].mearemarks + ". @@Thank You";
                    //RFC0031811 - Added by Sandip on 14th April 2021, to allow MEA admin to attach files at time of rejection
                    response = await _commonUtilities.SendEmailMethod(fromAddress, toAddress, subject, body, ccAddress,rowInfo[0].imageattachment);
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
        //added by Sandip on 25th Oct 2019 for RFC0021956 -- This api returns complete contractor info list.
        public async Task<HashSet<Dictionary<string, object>>> GetPOVContractorInfoAsync()
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _adminDataService.GetPOVContractorInfoAsync();
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
        //added by Sandip on 25th Oct 2019 for RFC0021956 -- This api updates contractor information based on unique contractor id.
        public async Task<Dictionary<string, object>> UpdatePOVContractorInfoAsync(object JsonObj, string OfficerID)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                string results = string.Empty;
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "updatePOVContractorInfo");
                var rowInfo = JsonConvert.DeserializeObject<List<POVerficationListItem>>(jsonVal[0]);
                result = await _adminDataService.UpdatePOVContractorInfoAsync(rowInfo, OfficerID);
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
        //added by Sandip on 25th Oct 2019 for RFC0021956 -- This api inserts contractor information in the table.
        public async Task<Dictionary<string, object>> InsertPOVContractorInfoAsync(object JsonObj, string OfficerID)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                string results = string.Empty;
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "insertPOVContractorInfo");
                var rowInfo = JsonConvert.DeserializeObject<List<POVerficationListItem>>(jsonVal[0]);
                result = await _adminDataService.InsertPOVContractorInfoAsync(rowInfo, OfficerID);
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
        //added by Sandip on 4th Nov 2019 for RFC0021956 -- This api returns complete history of the session from created to final posted stage.
        public async Task<HashSet<Dictionary<string, object>>> GetPOVSessionHistoryAsync(string UserType = null, string OfficerID = null, string FromDate = null, string ToDate = null, string POVID = null)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _adminDataService.GetPOVSessionHistoryAsync(UserType, OfficerID, FromDate, ToDate, POVID);
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
        //added by Sandip on 18th Aug 2021 for RFC0034769 -- This api updates job status of the session from Admin portal.
        public async Task<Dictionary<string, object>> UpdatePOVJobStatusAsync(object JsonObj, string OfficerID)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                string results = string.Empty;
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "updatePOVJobStatus");
                var rowInfo = JsonConvert.DeserializeObject<List<POVerficationListItem>>(jsonVal[0]);
                result = await _adminDataService.UpdatePOVJobStatusAsync(rowInfo, OfficerID);
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
        //added by Sandip on 2nd Mar 2020 for RFC0024551 -- This api returns the list of jobs details.
        public async Task<HashSet<Dictionary<string, object>>> GetJobSyncAsync()
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _adminDataService.GetJobSyncAsync();
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
        //added by Sandip on 2nd Mar 2020 for RFC0024551 -- This api inserts new job info details in the job sync table.
        public async Task<Dictionary<string, object>> InsertJobSyncAsync(object JsonObj, string UserID)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                string results = string.Empty;
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "insertjobsync");
                var rowInfo = JsonConvert.DeserializeObject<List<IncidentManagementItems>>(jsonVal[0]);
                result = await _adminDataService.InsertJobSyncAsync(rowInfo, UserID);
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
        //added by Sandip on 2nd Mar 2020 for RFC0024551 -- This api updates existing job info details in the job sync table.
        public async Task<Dictionary<string, object>> UpdateJobSyncAsync(object JsonObj, string UserID)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                string results = string.Empty;
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "updatejobsync");
                var rowInfo = JsonConvert.DeserializeObject<List<IncidentManagementItems>>(jsonVal[0]);
                result = await _adminDataService.UpdateJobSyncAsync(rowInfo, UserID);
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
        //added by Sandip on 17th Oct 2020 for RFC0029411 -- This api returns the list of problematic cable joints for the admin portal.
        public async Task<HashSet<Dictionary<string, object>>> GetProblematicCableJointAsync()
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _adminDataService.GetProblematicCableJointAsync();
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
        //added by Sandip on 17th Oct 2020 for RFC0029411 -- This api returns the report path of the pre-generated problematic cable joint
        public async Task<string> GetProblematicCableJointReportPathAsync()
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            string pcjReportDataPath = string.Empty;
            string reportPath = string.Empty;
            string jsonData = string.Empty;
            try
            {
                pcjReportDataPath = ConfigurationManager.AppSettings["PCJReportDataPath"];

                for (int i = 0; i <= pcjReportDataPath.Split(';').Length - 1; i++)
                {
                    result = await _adminDataService.GetProblematicCableJointReportPathAsync(pcjReportDataPath.Split(';')[i].ToString());

                    foreach (Dictionary<string, object> item in (IEnumerable)result)
                    {
                        if (item["keyvalue"] != null)
                        {
                            if (string.IsNullOrEmpty(reportPath))
                                reportPath = item["keyvalue"].ToString();
                            else
                                reportPath = reportPath + item["keyvalue"].ToString();
                        }
                    }
                }
                jsonData = await _commonUtilities.ConvertCsvFileToJsonObject(reportPath);
            }
            catch (Exception ex)
            {
                WriteErrorLog("Exception :- " + ex.Message.ToString());
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return jsonData;
        }
        //added by Sandip on 17th Oct 2020 for RFC0029411 -- This api inserts new problematic cable joint data
        public async Task<SchemaResult> InsertProblematicCableJointDataAsync(object JsonObj, string UserID)
        {
            SchemaResult schemaResult = new SchemaResult();

            HashSet<Dictionary<string, object>> validateResults = new HashSet<Dictionary<string, object>>();
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "insertPCJData");
                validateResults = await _adminDataService.GetValidateProblematicCableJointDataAsync(string.Join(".",jsonVal));

                if (validateResults.Count == 0)
                {
                    result = await _adminDataService.InsertProblematicCableJointDataAsync(string.Join(".", jsonVal), UserID);
                    schemaResult.Result = result;
                }
                else
                {
                    schemaResult.Message = "Fail";
                    schemaResult.Result = validateResults;
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
            return schemaResult;
        }
        public void WriteErrorLog(string message)
        {
            string _filePath = System.Configuration.ConfigurationManager.AppSettings["LogPath"];
            string content = DateTime.Now + " " + " " + message + Environment.NewLine;
            System.IO.File.AppendAllText(_filePath, content);
        }
        /*public async Task<HashSet<Dictionary<string, object>>> GetAPIMapSheetSummaryReportAsync(string Username)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _adminDataService.GetAPIMapSheetSummaryReportAsync(Username);
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
        public async Task<HashSet<Dictionary<string, object>>> GetAPIMapSheetGeometryAsync(string InputDate, string ReportType, string Username)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _adminDataService.GetAPIMapSheetGeometryAsync(InputDate, ReportType, Username);
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
        }*/
    }
}
