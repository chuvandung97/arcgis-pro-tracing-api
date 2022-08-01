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
using System.Web.Mvc;

namespace Schema.Services
{
    public class SLDService : ISLDService
    {
        ILoggingService _loggingService;
        ISLDDataService _sldDataService;

        Dictionary<string, object> errorLogInfo;
        public SLDService(ILoggingService LoggingService, ISLDDataService SLDDataService)
        {
            _loggingService = LoggingService;
            _sldDataService = SLDDataService;
        }
        public ICustomAuthorizeService CustomAuthorizeService
        {
            get
            {
                return DependencyResolver.Current.GetService<ICustomAuthorizeService>();
            }
        }
        public async Task<HashSet<Dictionary<string, object>>> GetSubstationListAsync(int Segment = 0, string SubstationName = null, string SheetID = null, string Voltage = null)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _sldDataService.GetSubstationListAsync(Segment, SubstationName, SheetID, Voltage);
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
        public async Task<HashSet<Dictionary<string, object>>> PropagateSLDToGISAsync(string Voltage, string Geometry, string MapSheetID)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _sldDataService.PropagateSLDToGISAsync(Voltage, Geometry, MapSheetID);
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
        public async Task<HashSet<Dictionary<string, object>>> PropagateGISToSLDAsync(string Voltage, string Geometry)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _sldDataService.PropagateGISToSLDAsync(Voltage, Geometry);
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
        /*public async Task<HashSet<Dictionary<string, object>>> SheetIDsListingAsync()
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _sldDataService.SheetIDsListingAsync();
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
        public async Task<HashSet<Dictionary<string, object>>> JumpToDestinationAsync(string Geometry)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _sldDataService.JumpToDestinationAsync(Geometry);
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
        public async Task<SchemaResult> GetCableLengthAsync(string Voltage, string Geometry, string MapSheetID)
        {
            SchemaResult result = new SchemaResult();
            HashSet<Dictionary<string, object>> sldResult = new HashSet<Dictionary<string, object>>();
            try
            {
                sldResult = await _sldDataService.GetCableLengthAsync(Voltage, Geometry, MapSheetID);
                int sldCableCount = 0;
                foreach (Dictionary<string, object> item in (IEnumerable)sldResult)
                {
                    if (item["sldgeometries"] != null)
                    {
                        sldCableCount = sldCableCount + 1;
                    }                  
                }
                if (sldCableCount > 1)
                {
                    result.Result = null;
                    result.Message = "Select only one cable.";
                }
                else
                    result.Result = sldResult;
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
        //added by Sandip on 2nd April 2019 for RFC0018439 -- To track and log searched SLD mapsheet/substation in database
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

                results = await _sldDataService.InsertSearchedLocationInDBAsync(rowInfo);
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
