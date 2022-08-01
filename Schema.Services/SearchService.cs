using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Schema.Core.Data;
using Schema.Core.Models;
using Schema.Core.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Schema.Services
{
    public class SearchService : ISearchService
    {
        ILoggingService _loggingService;
        ISearchDataService _searchDataService;

        Dictionary<string, object> errorLogInfo;
        public SearchService(ILoggingService LoggingService, ISearchDataService SearchDataService)
        {
            _loggingService = LoggingService;
            _searchDataService = SearchDataService;
        }
        public ICustomAuthorizeService CustomAuthorizeService
        {
            get
            {
                return DependencyResolver.Current.GetService<ICustomAuthorizeService>();
            }
        }
        public async Task<HashSet<Dictionary<string, object>>> GetSearchResultsAsync(string SearchType, string Username, string Text, string UserType = null)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                if (Convert.ToInt16(SearchType) != 9)
                    result = await _searchDataService.SearchSchemaDetailsAsync(SearchType, Text, UserType);
                else
                {
                    result = await _searchDataService.SearchDMISJobIDAsync(Username, Text);
                    foreach (Dictionary<string, object> item in (IEnumerable)result)
                    {
                        string val = string.Empty;
                        if (item.ContainsKey("xyloc"))
                        {
                            val = item["xyloc"].ToString();
                            item.Remove("xyloc");
                            item.Add("shape", val);
                        }
                    }
                }
                /*switch (SearchType)
                {
                    case "0":
                        result = await _searchDataService.SearchAddressPointAsync(Text);
                        break;
                    case "1":
                        result = await _searchDataService.SearchPostalCodeAsync(Text);
                        break;

                    case "2":
                        result = await _searchDataService.SearchBuildingAsync(Text);
                        break;

                    case "3":
                        result = await _searchDataService.SearchMapSheetsAsync(Text);
                        break;

                    case "4":
                        result = await _searchDataService.SearchRoadNamesShortAsync(Text);
                        break;

                    case "5":
                        result = await _searchDataService.SearchRoadNamesLongAsync(Text);
                        break;

                    case "6":
                        result = await _searchDataService.SearchMukimLotsAsync(Text);
                        break;

                    case "7":
                        result = await _searchDataService.SearchSubstationAsync(Text, UserType);
                        break;

                    case "8":
                        result = await _searchDataService.SearchOverGroundBoxAsync(Text);
                        break;

                    case "9":
                        result = await _searchDataService.SearchDMISJobIDAsync(Username, Text);
                        foreach (Dictionary<string, object> item in (IEnumerable)result)
                        {
                            string val = string.Empty;
                            if (item.ContainsKey("xyloc"))
                            {
                                val = item["xyloc"].ToString();
                                item.Remove("xyloc");
                                item.Add("shape", val);
                            }
                        }
                        break;
                }*/
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
        public async Task<HashSet<Dictionary<string, object>>> GetRoadJunctionsAsync(string RoadName)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _searchDataService.GetRoadJunctionsAsync(RoadName);
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
        public async Task<HashSet<Dictionary<string, object>>> GetRoadInfoAsync(string RoadName)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _searchDataService.GetRoadInfoAsync(RoadName);
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
        //added by Sandip on 1st April 2019 for RFC0018439 -- To track and log exact GIS searched location in database
        public async Task<Dictionary<string, object>> InsertSearchedLocationInDBAsync(object JsonObj)
        {
            //string result = string.Empty;
            Dictionary<string, object> results = new Dictionary<string, object>();
            string subject, toAddress, fromAddress, blockedUser;
            bool blockedFlag;
            try
            {
                var jsonVal = UnWrapObjects(JsonObj, "insertsearchlocation");
                var rowInfo = JsonConvert.DeserializeObject<List<UsageTrackingItem>>(jsonVal[0]);

                results = await _searchDataService.InsertSearchedLocationInDBAsync(rowInfo);
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
            return results;
        }
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
