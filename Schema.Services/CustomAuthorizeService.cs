using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Schema.Core.Data;
using Schema.Core.Models;
using Schema.Core.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Schema.Services
{
    public class CustomAuthorizeService : ICustomAuthorizeService
    {
        ILoggingService _loggingService;
        ICustomAuthorizeDataService _customAuthorizeDataService;

        Dictionary<string, object> errorLogInfo;
        public CustomAuthorizeService(ILoggingService LoggingService, ICustomAuthorizeDataService CustomAuthorizeDataService)
        {
            _loggingService = LoggingService;
            _customAuthorizeDataService = CustomAuthorizeDataService;
        }
        //string username, string url, string parameters, string functionName, string clientIP, string referrer, string browser, string useragent, string type
        public async Task<Dictionary<string, object>> InsertFunctionLogInfoInDB()
        {
            //string result = string.Empty;
            Dictionary<string, object> results = new Dictionary<string, object>();
            try
            {
                //results = await _customAuthorizeDataService.InsertFunctionLogInfoInDB(username, url, parameters, functionName, clientIP, referrer, browser, useragent, type);               
                results = await _customAuthorizeDataService.InsertFunctionLogInfoInDB();
            }
            catch (Exception ex)
            {
                //WriteErrorLog("Request Log Info Error " + ex.Message.ToString());
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await _customAuthorizeDataService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await _customAuthorizeDataService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return results;
        }
        public async Task<Dictionary<string, object>> InsertErrorLogInfoInDB(string errorMsg)
        {
            Dictionary<string, object> results = new Dictionary<string, object>();
            try
            {
                results = await _customAuthorizeDataService.InsertErrorLogInfoInDB(errorMsg);
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await _customAuthorizeDataService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await _customAuthorizeDataService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return results;
        }
        public async Task<Dictionary<string, object>> InsertMapServiceRequestInDB(object JsonObj)
        {
            //string result = string.Empty;
            Dictionary<string, object> results = new Dictionary<string, object>();           
            try
            {               
                var jsonVal = UnWrapObjects(JsonObj, "insertmapservicerequest");
                var rowInfo = JsonConvert.DeserializeObject<List<MapServiceItem>>(jsonVal[0]);
                string Username = rowInfo[0].username;
                string URL = rowInfo[0].url;
                string ClientIP = rowInfo[0].clientip;
                string Referrer = rowInfo[0].referrer;
                string Browser = rowInfo[0].browser;
                string UserAgent = rowInfo[0].useragent;
                string Parameter = rowInfo[0].parameter;
                double ResponseTime = rowInfo[0].responsetime;
                                               
                //results = await _customAuthorizeDataService.InsertFunctionLogInfoInDB(username, url, parameters, functionName, clientIP, referrer, browser, useragent, type);               
                results = await _customAuthorizeDataService.InsertMapServiceRequestInDB(Username, URL, Parameter, ClientIP, Referrer, Browser, UserAgent, ResponseTime);
            }
            catch (Exception ex)
            {              
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await _customAuthorizeDataService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await _customAuthorizeDataService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return results;
        }
        public async Task<Dictionary<string, object>> InsertMapServiceErrorInDB(object JsonObj)
        {
            //string result = string.Empty;
            Dictionary<string, object> results = new Dictionary<string, object>();
            try
            {
                var jsonVal = UnWrapObjects(JsonObj, "insertmapserviceerror");
                var rowInfo = JsonConvert.DeserializeObject<List<MapServiceItem>>(jsonVal[0]);
                string Username = rowInfo[0].username;
                string URL = rowInfo[0].url;
                string Parameter = rowInfo[0].parameter;
                string FunctionName = rowInfo[0].functionname;
                string ClientIP = rowInfo[0].clientip;
                string Referrer = rowInfo[0].referrer;
                string ErrorMsg = rowInfo[0].errormsg;
                string Type = rowInfo[0].type;

                results = await _customAuthorizeDataService.InsertMapServiceErrorInDB(Username, URL, Parameter, FunctionName, ClientIP, Referrer, ErrorMsg, Type);
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await _customAuthorizeDataService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await _customAuthorizeDataService.InsertErrorLogInfoInDB(ex.Message);
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
        /*public void WriteErrorLog(string message)
        {
            string _filePath = System.Configuration.ConfigurationManager.AppSettings["LogPath"];
            string content = DateTime.Now + Environment.NewLine + message + Environment.NewLine;
            System.IO.File.AppendAllText(_filePath, content);
        }*/
        /*public void WriteErrorLog1(object message)
        {
            string _filePath = System.Configuration.ConfigurationManager.AppSettings["LogPath"];
            string content = DateTime.Now + Environment.NewLine + message + Environment.NewLine;
            System.IO.File.AppendAllText(_filePath, content);
        }*/
    }
}
