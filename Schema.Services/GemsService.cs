using Schema.Core.Data;
using Schema.Core.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Schema.Services
{
    public class GemsService : IGemsService
    {
        ILoggingService _loggingService;
        IGemsDataService _gemsDataService;
        //ICustomAuthorizeService _customAuthService;

        Dictionary<string, object> errorLogInfo;
        public GemsService(ILoggingService LoggingService, IGemsDataService GemsDataService)
        {
            _loggingService = LoggingService;
            _gemsDataService = GemsDataService;

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
        public async Task<HashSet<Dictionary<string, object>>> AsBuiltDrawingsAsync(string Voltage, string Geometry, string SubtypeCD)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                //added by Sandip on 11th March 2019 for access control
                if (Voltage.Contains("480") || Voltage.Contains("540") || Voltage.Contains("600"))
                {
                    result = await AuthorizeAPI();
                    if (result.Count == 0)
                    {
                        var subTypes = SubtypeCD.Split('|');
                        for (int i = 0; i < Voltage.Split(',').Length; i++)
                        {
                            result.UnionWith(await _gemsDataService.AsBuiltDrawingsAsync(Voltage.Split(',')[i].ToString(), Geometry, subTypes[i].ToString()));
                        }
                    }
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
        public async Task<HashSet<Dictionary<string, object>>> GetXSectionInfoAsync(string Geometry, string Query, int CriteriaID)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                //added by Sandip on 11th March 2019 for access control
                if (Query.Contains("480") || Query.Contains("540") || Query.Contains("600"))
                {
                    result = await AuthorizeAPI();
                }
                else if (CriteriaID == 13 || CriteriaID == 14)
                {
                    result = await AuthorizeAPI();
                }
                if (result.Count == 0)
                    result = await _gemsDataService.GetXSectionInfoAsync(Geometry, Query, CriteriaID);
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
        /*public async Task<HashSet<Dictionary<string, object>>> GetGridGeometryAsync(string SegmentID)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _gemsDataService.GetGridGeometryAsync(SegmentID);
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
        public async Task<HashSet<Dictionary<string, object>>> GetSchematicSegmentGeomAsync(int SegNum, string SheetID)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _gemsDataService.GetSchematicSegmentGeomAsync(SegNum, SheetID);
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
        public async Task<HashSet<Dictionary<string, object>>> GetNMACSDetailsAsync(double X, double Y, string Voltage, string VoltageType, string Geometry, string MapSheetID)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _gemsDataService.GetNMACSDetailsAsync(X, Y, Voltage, VoltageType, Geometry, MapSheetID);
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
        public async Task<HashSet<Dictionary<string, object>>> GetCustomerCountAsync(string UserID, string Geometry = null, double X = 0.0, double Y = 0.0)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _gemsDataService.GetCustomerCountAsync(UserID, Geometry, X, Y);
                if (result == null)
                { }
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
        public async Task<HashSet<Dictionary<string, object>>> Get66kvSupplyZoneLayersAsync()
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                //added by Sandip on 11th March 2019 for access control
                result = await AuthorizeAPI();

                if (result.Count == 0)
                    result = await _gemsDataService.Get66kvSupplyZoneLayersAsync();
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
        public async Task<HashSet<Dictionary<string, object>>> Get66KVCustomerCountAsync(double X, double Y)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                //added by Sandip on 11th March 2019 for access control
                result = await AuthorizeAPI();

                if (result.Count == 0)
                    result = await _gemsDataService.Get66KVCustomerCountAsync(X, Y);
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
        /*public async Task<HashSet<Dictionary<string, object>>> GetAMIOutageCustomerCountAsync(object JsonObj)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                //added by Sandip on 11th March 2019 for access control
                result = await AuthorizeAPI();

                if (result.Count == 0)
                    result = await _gemsDataService.GetAMIOutageCustomerCountAsync(JsonObj);
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
        /*public void WriteErrorLog(string message)
        {
            string _filePath = System.Configuration.ConfigurationManager.AppSettings["LogPath"];
            string content = DateTime.Now + Environment.NewLine + message + Environment.NewLine;
            System.IO.File.AppendAllText(_filePath, content);
        }*/
    }
}
