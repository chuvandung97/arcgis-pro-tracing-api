using Schema.Core.Data;
using Schema.Core.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace Schema.Data
{
    public class CustomAuthorizeDataService : PgLogDbBase, ICustomAuthorizeDataService
    {
        public CustomAuthorizeDataService(IConfigService configService, ILoggingService loggingService)
          : base(configService, loggingService)
        { }
        //string username, string url, string parameters, string functionName, string clientIP, string referrer, string browser, string useragent, string type
        public async Task<Dictionary<string, object>> InsertFunctionLogInfoInDB()
        {
            #region get values from http context
            var httpContext = HttpContext.Current;
            //WriteErrorLog("HTTP CONTEXT :- "+ httpContext);
            string username = httpContext.User.Identity.Name;
            //WriteErrorLog("USERNAME " + username);
            string url = string.Empty;
            string parameters = string.Empty;
            string referrer = string.Empty;
            string partURL = string.Empty;

            if (username.Contains('\\'))
                username = username.Split('\\')[1].ToString();
            //WriteErrorLog("USERNAME " + username);
            if (HttpContext.Current.Request.Url != null)
            {
                url = HttpContext.Current.Request.Url.ToString();
                // Modified by Tulasi on 10/11/2021 for RFC0035510 - httpruntime in web.config making url to decode
                url = HttpUtility.UrlDecode(url);
            }
                
            //WriteErrorLog("URL " + url);

            if (url.Contains("?"))
                parameters = url.Split('?')[1].ToString();
            //WriteErrorLog("PARAMS " + parameters);
            //Get the function name
            string part = string.Empty;
            if (url.Contains("?"))
                partURL = url.Substring(0, url.IndexOf('?'));
            else
                partURL = url;
            //WriteErrorLog("PART URL " + partURL);
            string[] splitString = partURL.Split('/');
            string functionName = splitString[splitString.Length - 1].Trim();
            //WriteErrorLog("FUNCTION NAME " + functionName);
            //End of function name

            string clientIP = GetClientIp(HttpContext.Current.Request);
            //WriteErrorLog("CLIENT IP " + clientIP);
            if (HttpContext.Current.Request.UrlReferrer != null)
                referrer = HttpContext.Current.Request.UrlReferrer.ToString();
            //WriteErrorLog("REFERRER " + referrer);
            string browser = HttpContext.Current.Request.Browser.Browser;
            //WriteErrorLog("BROWSER " + browser);
            string useragent = HttpContext.Current.Request.UserAgent;
            //WriteErrorLog("USER AGENT " + useragent);
            #endregion

            Dictionary<string, object> result = new Dictionary<string, object>();
            //Query the function name 
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_insert_schema_app_requestlog");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pusername";
            param1.DbType = DbType.String;
            if (!string.IsNullOrEmpty(username))
                param1.Value = username.ToUpper();
            else
                param1.Value = DBNull.Value;
            command.Parameters.Add(param1);
            //WriteErrorLog("REQUEST LOG INFO:- ");
            //WriteErrorLog("USERNAME:- " + username);
            var param2 = command.CreateParameter();
            param2.ParameterName = "purl";
            param2.DbType = DbType.String;
            if (!string.IsNullOrEmpty(url))
                param2.Value = partURL;
            else
                param2.Value = DBNull.Value;
            command.Parameters.Add(param2);
            //WriteErrorLog("URL:- " + partURL);
            var param3 = command.CreateParameter();
            param3.ParameterName = "pparam";
            param3.DbType = DbType.String;
            if (!string.IsNullOrEmpty(parameters))
                param3.Value = parameters;
            else
                param3.Value = DBNull.Value;
            command.Parameters.Add(param3);
            //WriteErrorLog("PARAMETERS:- " + parameters);
            var param4 = command.CreateParameter();
            param4.ParameterName = "pfunctionname";
            param4.DbType = DbType.String;
            if (!string.IsNullOrEmpty(functionName))
                param4.Value = functionName;
            else
                param4.Value = DBNull.Value;
            command.Parameters.Add(param4);
            //WriteErrorLog("FUNCTION NAME:- " + functionName);
            var param5 = command.CreateParameter();
            param5.ParameterName = "pclientip";
            param5.DbType = DbType.String;
            if (!string.IsNullOrEmpty(clientIP))
                param5.Value = clientIP;
            else
                param5.Value = DBNull.Value;
            command.Parameters.Add(param5);
            //WriteErrorLog("CLIENT IP:- " + clientIP);
            var param6 = command.CreateParameter();
            param6.ParameterName = "preferrer";
            param6.DbType = DbType.String;
            if (!string.IsNullOrEmpty(referrer))
                param6.Value = referrer;
            else
                param6.Value = DBNull.Value;
            command.Parameters.Add(param6);
            //WriteErrorLog("REFERRER:- " + referrer);
            var param7 = command.CreateParameter();
            param7.ParameterName = "pbrowser";
            param7.DbType = DbType.String;
            if (!string.IsNullOrEmpty(browser))
                param7.Value = browser;
            else
                param7.Value = DBNull.Value;
            command.Parameters.Add(param7);
            //WriteErrorLog("BROWSERS:- " + browser);
            var param8 = command.CreateParameter();
            param8.ParameterName = "puseragent";
            param8.DbType = DbType.String;
            if (!string.IsNullOrEmpty(useragent))
                param8.Value = useragent;
            else
                param8.Value = DBNull.Value;
            command.Parameters.Add(param8);
            //WriteErrorLog("USER AGENT:- " + useragent);
            var param9 = command.CreateParameter();
            param9.ParameterName = "ptype";
            param9.DbType = DbType.String;
            param9.Value = "CUSTOM SERVICE";
            command.Parameters.Add(param9);

            var param10 = command.CreateParameter();
            param10.ParameterName = "rowcount";
            param10.DbType = DbType.Int16;
            param10.Direction = ParameterDirection.Output;
            command.Parameters.Add(param10);

            command.CommandType = CommandType.StoredProcedure;
            result = await UpdateDataAsync(command);
            return result;
        }
        public async Task<Dictionary<string, object>> InsertErrorLogInfoInDB(string errorMsg)
        {
            var httpContext = HttpContext.Current;
            string username = httpContext.User.Identity.Name;

            string url = string.Empty;
            string parameters = string.Empty;
            string referrer = string.Empty;
            string functionName = string.Empty;
            string clientIP = string.Empty;

            #region get values from http context
            if (username.Contains('\\'))
                username = username.Split('\\')[1].ToString();

            if (HttpContext.Current.Request.Url != null)
            {
                url = HttpContext.Current.Request.Url.ToString();
                // Modified by Tulasi on 10/11/2021 for RFC0035510 - httpruntime in web.config making url to decode
                url = HttpUtility.UrlDecode(url);
            }

            if (url.Contains("?"))
                parameters = url.Split('?')[1].ToString();

            //Get the function name
            string partURL = string.Empty;
            if (url.Contains("?"))
                partURL = url.Substring(0, url.IndexOf('?'));
            else
                partURL = url;
            string[] splitString = partURL.Split('/');
            functionName = splitString[splitString.Length - 1].Trim();
            //End of function name

            clientIP = GetClientIp(HttpContext.Current.Request);

            if (HttpContext.Current.Request.UrlReferrer != null)
                referrer = HttpContext.Current.Request.UrlReferrer.ToString();
            #endregion

            Dictionary<string, object> result = new Dictionary<string, object>();

            //Query the function name 
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_insert_schema_app_errorlog");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pusername";
            param1.DbType = DbType.String;
            if (!string.IsNullOrEmpty(username))
                param1.Value = username.ToUpper();
            else
                param1.Value = DBNull.Value;
            command.Parameters.Add(param1);
            //WriteErrorLog("ERROR LOG INFO:- ");
            //WriteErrorLog("USERNAME:- " + username);
            var param2 = command.CreateParameter();
            param2.ParameterName = "purl";
            param2.DbType = DbType.String;
            if (!string.IsNullOrEmpty(url))
                param2.Value = partURL;
            else
                param2.Value = DBNull.Value;
            command.Parameters.Add(param2);
            //WriteErrorLog("URL:- " + url);
            var param3 = command.CreateParameter();
            param3.ParameterName = "pparam";
            param3.DbType = DbType.String;
            if (!string.IsNullOrEmpty(parameters))
                param3.Value = parameters;
            else
                param3.Value = DBNull.Value;
            command.Parameters.Add(param3);
            //WriteErrorLog("PARAMETERS:- " + parameters);
            var param4 = command.CreateParameter();
            param4.ParameterName = "pfunctionname";
            param4.DbType = DbType.String;
            if (!string.IsNullOrEmpty(functionName))
                param4.Value = functionName;
            else
                param4.Value = DBNull.Value;
            command.Parameters.Add(param4);
            //WriteErrorLog("FUNCTION NAME:- " + functionName);
            var param5 = command.CreateParameter();
            param5.ParameterName = "pclientip";
            param5.DbType = DbType.String;
            if (!string.IsNullOrEmpty(clientIP))
                param5.Value = clientIP;
            else
                param5.Value = DBNull.Value;
            command.Parameters.Add(param5);
            //WriteErrorLog("CLIENT IP:- " + clientIP);
            var param6 = command.CreateParameter();
            param6.ParameterName = "preferrer";
            param6.DbType = DbType.String;
            if (!string.IsNullOrEmpty(referrer))
                param6.Value = referrer;
            else
                param6.Value = DBNull.Value;
            command.Parameters.Add(param6);
            //WriteErrorLog("REFERRER:- " + referrer);
            var param7 = command.CreateParameter();
            param7.ParameterName = "perrormsg";
            param7.DbType = DbType.String;
            if (!string.IsNullOrEmpty(errorMsg))
                param7.Value = errorMsg;
            else
                param7.Value = DBNull.Value;
            command.Parameters.Add(param7);
            //WriteErrorLog("ERROR:- " + errorMsg);
            command.CommandType = CommandType.StoredProcedure;
            result = await UpdateDataAsync(command);
            return result;
        }
        public async Task<Dictionary<string, object>> InsertMapServiceRequestInDB(string Username, string URL, string Parameter, string ClientIP, string Referrer, string Browser, string UserAgent, double ResponseTime)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            //Query the function name 
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_insert_schema_app_requestlog");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pusername";
            param1.DbType = DbType.String;
            if (!string.IsNullOrEmpty(Username))
                param1.Value = Username.ToUpper();
            else
                param1.Value = DBNull.Value;
            command.Parameters.Add(param1);
            //WriteErrorLog("Username:- " + Username);

            var param2 = command.CreateParameter();
            param2.ParameterName = "purl";
            param2.DbType = DbType.String;
            if (!string.IsNullOrEmpty(URL))
                param2.Value = URL;
            else
                param2.Value = DBNull.Value;
            command.Parameters.Add(param2);
            //WriteErrorLog("URL:- " + URL);
            var param11 = command.CreateParameter();
            param11.ParameterName = "pparam";
            param11.DbType = DbType.String;
            if (!string.IsNullOrEmpty(Parameter))
                param11.Value = Parameter;
            else
                param11.Value = DBNull.Value;
            command.Parameters.Add(param11);

            var param4 = command.CreateParameter();
            param4.ParameterName = "pfunctionname";
            param4.DbType = DbType.String;
            param4.Value = "MAP SERVICE";
            command.Parameters.Add(param4);
            //WriteErrorLog("Function Name:- " + "MAP SERVICE");

            var param5 = command.CreateParameter();
            param5.ParameterName = "pclientip";
            param5.DbType = DbType.String;
            if (!string.IsNullOrEmpty(ClientIP))
                param5.Value = ClientIP;
            else
                param5.Value = DBNull.Value;
            command.Parameters.Add(param5);
            //WriteErrorLog("ClientIP:- " + ClientIP);

            var param6 = command.CreateParameter();
            param6.ParameterName = "preferrer";
            param6.DbType = DbType.String;
            if (!string.IsNullOrEmpty(Referrer))
                param6.Value = Referrer;
            else
                param6.Value = DBNull.Value;
            command.Parameters.Add(param6);
            //WriteErrorLog("Referrer:- " + Referrer);

            var param7 = command.CreateParameter();
            param7.ParameterName = "pbrowser";
            param7.DbType = DbType.String;
            if (!string.IsNullOrEmpty(Browser))
                param7.Value = Browser;
            else
                param7.Value = DBNull.Value;
            command.Parameters.Add(param7);
            //WriteErrorLog("Browser:- " + Browser);

            var param8 = command.CreateParameter();
            param8.ParameterName = "puseragent";
            param8.DbType = DbType.String;
            if (!string.IsNullOrEmpty(UserAgent))
                param8.Value = UserAgent;
            else
                param8.Value = DBNull.Value;
            command.Parameters.Add(param8);
            //WriteErrorLog("UserAgent:- " + UserAgent);

            var param9 = command.CreateParameter();
            param9.ParameterName = "ptype";
            param9.DbType = DbType.String;
            param9.Value = "PROXY SERVICE";
            command.Parameters.Add(param9);
            //WriteErrorLog("Type:- " + "PROXY SERVICE");

            var param10 = command.CreateParameter();
            param10.ParameterName = "presponsetime";
            param10.DbType = DbType.Double;
            if (!string.IsNullOrEmpty(UserAgent))
                param10.Value = ResponseTime;
            else
                param10.Value = DBNull.Value;
            command.Parameters.Add(param10);
            //WriteErrorLog("Response:- " + ResponseTime);

            var param12 = command.CreateParameter();
            param12.ParameterName = "rowcount";
            param12.DbType = DbType.Int16;
            param12.Direction = ParameterDirection.Output;
            command.Parameters.Add(param12);

            command.CommandType = CommandType.StoredProcedure;
            result = await UpdateDataAsync(command);
            return result;
        }
        public async Task<Dictionary<string, object>> InsertMapServiceErrorInDB(string Username, string URL, string Parameter, string FunctionName, string ClientIP, string Referrer, string ErrorMsg, string Type)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_insert_schema_app_errorlog");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pusername";
            param1.DbType = DbType.String;
            if (!string.IsNullOrEmpty(Username))
                param1.Value = Username.ToUpper();
            else
                param1.Value = DBNull.Value;
            command.Parameters.Add(param1);
            //WriteErrorLog("ERROR LOG INFO:- ");
            //WriteErrorLog("USERNAME:- " + username);
            var param2 = command.CreateParameter();
            param2.ParameterName = "purl";
            param2.DbType = DbType.String;
            if (!string.IsNullOrEmpty(URL))
                param2.Value = URL;
            else
                param2.Value = DBNull.Value;
            command.Parameters.Add(param2);
            //WriteErrorLog("URL:- " + url);
            var param3 = command.CreateParameter();
            param3.ParameterName = "pparam";
            param3.DbType = DbType.String;
            if (!string.IsNullOrEmpty(Parameter))
                param3.Value = Parameter;
            else
                param3.Value = DBNull.Value;
            command.Parameters.Add(param3);
            //WriteErrorLog("PARAMETERS:- " + parameters);
            var param4 = command.CreateParameter();
            param4.ParameterName = "pfunctionname";
            param4.DbType = DbType.String;
            if (!string.IsNullOrEmpty(FunctionName))
                param4.Value = FunctionName;
            else
                param4.Value = DBNull.Value;
            command.Parameters.Add(param4);
            //WriteErrorLog("FUNCTION NAME:- " + functionName);
            var param5 = command.CreateParameter();
            param5.ParameterName = "pclientip";
            param5.DbType = DbType.String;
            if (!string.IsNullOrEmpty(ClientIP))
                param5.Value = ClientIP;
            else
                param5.Value = DBNull.Value;
            command.Parameters.Add(param5);
            //WriteErrorLog("CLIENT IP:- " + clientIP);
            var param6 = command.CreateParameter();
            param6.ParameterName = "preferrer";
            param6.DbType = DbType.String;
            if (!string.IsNullOrEmpty(Referrer))
                param6.Value = Referrer;
            else
                param6.Value = DBNull.Value;
            command.Parameters.Add(param6);
            //WriteErrorLog("REFERRER:- " + referrer);
            var param7 = command.CreateParameter();
            param7.ParameterName = "perrormsg";
            param7.DbType = DbType.String;
            if (!string.IsNullOrEmpty(ErrorMsg))
                param7.Value = ErrorMsg;
            else
                param7.Value = DBNull.Value;
            command.Parameters.Add(param7);
            //WriteErrorLog("ERROR:- " + errorMsg);            
            var param8 = command.CreateParameter();
            param8.ParameterName = "ptype";
            param8.DbType = DbType.String;
            if (!string.IsNullOrEmpty(Type))
                param8.Value = Type;
            command.Parameters.Add(param8);

            var param9 = command.CreateParameter();
            param9.ParameterName = "rowcount";
            param9.DbType = DbType.Int16;
            param9.Direction = ParameterDirection.Output;
            command.Parameters.Add(param9);

            command.CommandType = CommandType.StoredProcedure;
            result = await UpdateDataAsync(command);
            return result;
        }

        public static string GetClientIp(HttpRequest request)
        {
            if (request == null)
            {
                return "NA";
            }

            string remoteAddr = request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrWhiteSpace(remoteAddr))
            {
                remoteAddr = request.ServerVariables["REMOTE_ADDR"];
            }
            else
            {
                // the HTTP_X_FORWARDED_FOR may contain an array of IP, this can happen if you connect through a proxy.
                string[] ipRange = remoteAddr.Split(',');

                remoteAddr = ipRange[ipRange.Length - 1];
            }

            return remoteAddr;
        }
        /*public void WriteErrorLog(string message)
        {
            string _filePath = System.Configuration.ConfigurationManager.AppSettings["LogPath"];
            string content = DateTime.Now + Environment.NewLine + message + Environment.NewLine;
            System.IO.File.AppendAllText(_filePath, content);
        }*/
    }
}
