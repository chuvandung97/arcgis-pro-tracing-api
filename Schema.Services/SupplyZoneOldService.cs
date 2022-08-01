using Schema.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schema.Core.Data;
using System.Web.Mvc;
using Schema.Core.Models;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Principal;
using System.Web;

namespace Schema.Services
{
    public class SupplyZoneOldService : ISupplyZoneOldService
    {
        ILoggingService _loggingService;
        ISupplyZoneOldDataService _supplyZoneDataService;

        Dictionary<string, object> errorLogInfo;
        public SupplyZoneOldService(ILoggingService LoggingService, ISupplyZoneOldDataService SupplyZoneOldDataService)
        {
            _loggingService = LoggingService;
            _supplyZoneDataService = SupplyZoneOldDataService;
        }
        public ICustomAuthorizeService CustomAuthorizeService
        {
            get
            {
                return DependencyResolver.Current.GetService<ICustomAuthorizeService>();
            }
        }
        public IUserService UserService
        {
            get
            {
                return DependencyResolver.Current.GetService<IUserService>();
            }
        }
        public async Task<HashSet<Dictionary<string, object>>> BindEventDataAsync(string EventID)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                //added by Sandip on 11th March 2019 for access control
                result = await AuthorizeAPI();

                if (result.Count == 0)
                    result = await _supplyZoneDataService.BindEventDataAsync(EventID);
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                _loggingService.Error(ex);
                throw new Exception("Error");
            }
            return result;
        }
        public async Task<HashSet<Dictionary<string, object>>> GetEffectedBoundaryAsync(string EventID)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                //added by Sandip on 11th March 2019 for access control
                result = await AuthorizeAPI();

                if (result.Count == 0)
                    result = await _supplyZoneDataService.GetEffectedBoundaryAsync(EventID);
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                _loggingService.Error(ex);
                throw new Exception("Error");
            }
            return result;
        }
        public async Task<HashSet<Dictionary<string, object>>> RetrieveSummaryDataAsync(string EventID)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                //added by Sandip on 11th March 2019 for access control
                result = await AuthorizeAPI();

                if (result.Count == 0)
                    result = await _supplyZoneDataService.RetrieveSummaryDataAsync(EventID);
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                _loggingService.Error(ex);
                throw new Exception("Error");
            }
            return result;
        }
        public async Task<HashSet<Dictionary<string, object>>> RetrieveEventAllAsync()
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                //added by Sandip on 11th March 2019 for access control
                result = await AuthorizeAPI();

                if (result.Count == 0)
                    result = await _supplyZoneDataService.RetrieveEventAllAsync();
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                _loggingService.Error(ex);
                throw new Exception("Error");
            }
            return result;
        }
        /*public async Task<SchemaResult> RetrieveLatestEventAsync()
        {
            SchemaResult results = new SchemaResult();
            Dictionary<string, object> finalResult = new Dictionary<string, object>();
            HashSet<Dictionary<string, object>> eventResult = new HashSet<Dictionary<string, object>>();
            HashSet<Dictionary<string, object>> totalCountResult = new HashSet<Dictionary<string, object>>();
            string EventID = string.Empty;
            try
            {
                eventResult = await _supplyZoneDataService.RetrieveLatestEventAsync();
                finalResult.Add("EventDetails", eventResult);

                foreach (Dictionary<string, object> item in (IEnumerable)eventResult)
                {
                    if (item["event_id"] != null)
                        EventID = item["event_id"].ToString();
                }

                totalCountResult = await _supplyZoneDataService.RetrieveEventTotalCountAsync(EventID);
                finalResult.Add("TotalCount", totalCountResult);
                results.Result = finalResult;
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                _loggingService.Error(ex);
                throw new Exception("Error");
            }
            return results;
        }*/
        public async Task<HashSet<Dictionary<string, object>>> RetrieveEventTotalCountAsync(string EventID)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                //added by Sandip on 11th March 2019 for access control
                result = await AuthorizeAPI();

                if (result.Count == 0)
                    result = await _supplyZoneDataService.RetrieveEventTotalCountAsync(EventID);
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                _loggingService.Error(ex);
                throw new Exception("Error");
            }
            return result;
        }
        public async Task<HashSet<Dictionary<string, object>>> RetrieveEventTotalMRCCountAsync(string MRC)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                //added by Sandip on 11th March 2019 for access control
                result = await AuthorizeAPI();

                if (result.Count == 0)
                    result = await _supplyZoneDataService.RetrieveEventTotalMRCCountAsync(MRC);
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                _loggingService.Error(ex);
                throw new Exception("Error");
            }
            return result;
        }
        public async Task<HashSet<Dictionary<string, object>>> RetrieveEventBuildingCountAsync(string EventID)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                //added by Sandip on 11th March 2019 for access control
                result = await AuthorizeAPI();

                if (result.Count == 0)
                    result = await _supplyZoneDataService.RetrieveEventBuildingCountAsync(EventID);
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                _loggingService.Error(ex);
                throw new Exception("Error");
            }
            return result;
        }
        public async Task<HashSet<Dictionary<string, object>>> RetrieveEventStreetCountAsync(string EventID)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                //added by Sandip on 11th March 2019 for access control
                result = await AuthorizeAPI();

                if (result.Count == 0)
                    result = await _supplyZoneDataService.RetrieveEventStreetCountAsync(EventID);
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                _loggingService.Error(ex);
                throw new Exception("Error");
            }
            return result;
        }
        public async Task<HashSet<Dictionary<string, object>>> RetrieveEventBuildingCountMRCAsync(string MRC)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                //added by Sandip on 11th March 2019 for access control
                result = await AuthorizeAPI();

                if (result.Count == 0)
                    result = await _supplyZoneDataService.RetrieveEventBuildingCountMRCAsync(MRC);
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                _loggingService.Error(ex);
                throw new Exception("Error");
            }
            return result;
        }
        public async Task<HashSet<Dictionary<string, object>>> RetrieveEventStreetCountMRCAsync(string MRC)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                //added by Sandip on 11th March 2019 for access control
                result = await AuthorizeAPI();

                if (result.Count == 0)
                    result = await _supplyZoneDataService.RetrieveEventStreetCountMRCAsync(MRC);
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                _loggingService.Error(ex);
                throw new Exception("Error");
            }
            return result;
        }

        public async Task<HashSet<Dictionary<string, object>>> RetrieveEventStreetCountUsingStreetCodeAsync(string StreetCode, string EventID, string MRC)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                //added by Sandip on 11th March 2019 for access control
                result = await AuthorizeAPI();

                if (result.Count == 0)
                    result = await _supplyZoneDataService.RetrieveEventStreetCountUsingStreetCodeAsync(StreetCode, EventID, MRC);
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                _loggingService.Error(ex);
                throw new Exception("Error");
            }
            return result;
        }
        /*public async Task<HashSet<Dictionary<string, object>>> LayerMappingAsync()
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _supplyZoneDataService.LayerMappingAsync();
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                _loggingService.Error(ex);
                throw new Exception("Error");
            }
            return result;
        }*/
        public async Task<SchemaResult> CreateEventAsync(object JsonObj, string EmpID)
        {
            SchemaResult finalResults = new SchemaResult();
            HashSet<Dictionary<string, object>> eventResult = new HashSet<Dictionary<string, object>>();
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var eventInfojsonVal = UnWrapObjects(JsonObj, "insertSupplyZoneEventInfo");
                var eventDataGridjsonVal = UnWrapObjects(JsonObj, "insertSupplyZoneEventDataGrid");

                var eventRowInfo = JsonConvert.DeserializeObject<List<SupplyZoneItem>>(eventInfojsonVal[0]);

                var eventDataGridRowInfo = JsonConvert.DeserializeObject<List<SupplyZoneItem>>(eventDataGridjsonVal[0]);

                //check whether the event name already exists or not
                /*int count = 0;
                eventResult = await _supplyZoneDataService.CheckEventName(eventRowInfo[0].eventname);
                foreach (Dictionary<string, object> item in (IEnumerable)eventResult)
                {
                    if (item["count"] != null)
                        count = Convert.ToInt16(item["count"].ToString());
                }
                if (count == 1)
                {
                    finalResults.Message = "Event name already exists! Please provide some other event name.";
                    return finalResults;
                }*/
                //ends here

                for (int i = 0; i <= eventDataGridRowInfo.Count - 1; i++)
                {
                    Int16 pOutageFlag = Convert.ToInt16(eventDataGridRowInfo[i].poutageflag.ToString());
                    Int16 outageFlag = Convert.ToInt16(eventDataGridRowInfo[i].outageflag.ToString());

                    /*Int16 restoreFlag = Convert.ToInt16(eventDataGridRowInfo[i].restoreflag.ToString());
                    if (restoreFlag == 1)
                    {
                        finalResults.Message = "Restore flag can't be set for supply station '" + eventDataGridRowInfo[i].supplystation.ToString() + "' while creating event";
                        return finalResults;
                    }*/

                    if ((pOutageFlag + outageFlag) == 0)
                    {
                        finalResults.Message = "Can't proceed without setting any flag for supply station '" + eventDataGridRowInfo[i].supplystation.ToString() + "'";
                        return finalResults;
                    }
                    else if ((pOutageFlag + outageFlag) > 1)
                    {
                        finalResults.Message = "Both partial outage and outage flag can't be set at sametime for supply station '" + eventDataGridRowInfo[i].supplystation.ToString() + "'";
                        return finalResults;
                    }
                }
                //added by Sandip on 11th March 2019 for access control
                eventResult = await AuthorizeAPI();

                if (eventResult.Count == 0)
                {
                    Int64 EventID = await _supplyZoneDataService.GenerateEventID();
                    result = await _supplyZoneDataService.CreateEventAsync(eventRowInfo, EmpID, EventID);

                    if (result["Message"].ToString() == "Success")
                    {
                        for (int i = 0; i <= eventDataGridRowInfo.Count - 1; i++)
                        {
                            result = await _supplyZoneDataService.CreateEventDetailsAsync(eventDataGridRowInfo[i], EmpID, EventID);
                        }
                    }
                    finalResults.Result = result;
                }
                else
                {
                    finalResults.Message = "Authorization has been denied for this request.";
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
            return finalResults;
        }
        public async Task<SchemaResult> UpdateEventAsync(object JsonObj, string EmpID)
        {
            SchemaResult finalResults = new SchemaResult();
            Dictionary<string, object> result = new Dictionary<string, object>();
            HashSet<Dictionary<string, object>> results = new HashSet<Dictionary<string, object>>();
            int count = 0;
            try
            {
                //added by Sandip on 11th March 2019 for access control
                results = await AuthorizeAPI();

                if (results.Count == 0)
                {
                    var jsonVal = UnWrapObjects(JsonObj, "updateSupplyZoneEventInfo");

                    var rowInfo = JsonConvert.DeserializeObject<List<SupplyZoneItem>>(jsonVal[0]);

                    /*for (int i = 0; i <= rowInfo.Count - 1; i++)
                    {
                        Int16 pOutageFlag = Convert.ToInt16(rowInfo[i].poutageflag.ToString());
                        Int16 outageFlag = Convert.ToInt16(rowInfo[i].outageflag.ToString());
                        Int16 restoreFlag = Convert.ToInt16(rowInfo[i].restoreflag.ToString());

                        if ((pOutageFlag + outageFlag + restoreFlag) == 0)
                        {
                            finalResults.Message = "Can't proceed without setting any flag for supply station '" + rowInfo[i].supplystation.ToString() + "'";
                            return finalResults;
                        }
                        else if ((pOutageFlag + outageFlag + restoreFlag) > 1)
                        {
                            finalResults.Message = "Can't set multiple flags at sametime for supply station '" + rowInfo[i].supplystation.ToString() + "'";
                            return finalResults;
                        }
                }*/
                    for (int i = 0; i <= rowInfo.Count - 1; i++)
                    {
                        result = await _supplyZoneDataService.UpdateEventDetailsAsync(rowInfo[i], EmpID);
                        if (result["Message"].ToString() == "No records")
                        {
                            result = await _supplyZoneDataService.CreateEventDetailsAsync(rowInfo[i], EmpID, Convert.ToInt64(rowInfo[i].eventid));
                            if (result["Message"].ToString() == "Success")
                                count = count + 1;
                        }
                        else if (result["Message"].ToString() == "Success")
                            count = count + 1;
                    }
                    if (count >= 1)
                    {
                        result = new Dictionary<string, object>();
                        result.Add("Message", "Success");
                    }
                    finalResults.Result = result;
                }
                else
                {
                    finalResults.Message = "Authorization has been denied for this request.";
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
            return finalResults;
        }
        public async Task<HashSet<Dictionary<string, object>>> Get66kvSupplyZoneLayersAsync()
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                //added by Sandip on 11th March 2019 for access control
                result = await AuthorizeAPI();

                if (result.Count == 0)
                    result = await _supplyZoneDataService.Get66kvSupplyZoneLayersAsync();
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
        public async Task<HashSet<Dictionary<string, object>>> AuthorizeAPI()
        {
            HashSet<Dictionary<string, object>> userType = new HashSet<Dictionary<string, object>>();
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            string securityClearance = string.Empty;
            try
            {
                var httpContext = HttpContext.Current;
                string Username = httpContext.User.Identity.Name;
                if (Username.Contains('\\'))
                    Username = Username.Split('\\')[1].ToString();

                userType = await UserService.GetUserTypeAsync(Username);
                foreach (Dictionary<string, object> item in (IEnumerable)userType)
                {
                    if (item["security"] != null)
                        securityClearance = item["security"].ToString();
                }
                if (securityClearance.ToUpper() == "CAT2")
                {
                    Dictionary<string, object> item1 = new Dictionary<string, object>();
                    item1.Add("Message", "Authorization has been denied for this request.");
                    result.Add(item1);
                }
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                _loggingService.Error(ex);
                throw new Exception("Error");
            }
            return result;
        }
        /*public async Task<HashSet<Dictionary<string, object>>> BindUserDetailsAsync()
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _supplyZoneDataService.BindUserDetailsAsync();
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

        public async Task<HashSet<Dictionary<string, object>>> GetUserGroupAsync(string UserID)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _supplyZoneDataService.GetUserGroupAsync(UserID);
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
        public async Task<Dictionary<string, object>> AddUserAsync(object JsonObj)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = UnWrapObjects(JsonObj, "addSupplyZoneUser");

                var rowInfo = JsonConvert.DeserializeObject<List<SupplyZoneItem>>(jsonVal[0]);

                result = await _supplyZoneDataService.AddUserAsync(rowInfo);

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
        public async Task<Dictionary<string, object>> DeleteUserAsync(object JsonObj)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = UnWrapObjects(JsonObj, "deleteSupplyZoneUser");

                var rowInfo = JsonConvert.DeserializeObject<List<SupplyZoneItem>>(jsonVal[0]);

                result = await _supplyZoneDataService.DeleteUserAsync(rowInfo);

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
        public async Task<Dictionary<string, object>> UpdateUserRoleAsync(object JsonObj)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = UnWrapObjects(JsonObj, "updateSupplyZoneUserRole");

                var rowInfo = JsonConvert.DeserializeObject<List<SupplyZoneItem>>(jsonVal[0]);

                result = await _supplyZoneDataService.UpdateUserRoleAsync(rowInfo);

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
        public string[] UnWrapObjects(object objVal, string jsonId)
        {
            JObject obj = JObject.Parse(objVal.ToString());

            string[] str = new string[obj.Count];

            for (int i = 0; i < obj.Count; i++)
            {
                str[i] = obj[jsonId].ToString();
            }
            return str;
        }

    }
}
