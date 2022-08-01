using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Schema.Core.Services;
using Schema.Web.AuthorizeUser;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using SPGEMS.Utilities;
namespace Schema.Web.Controllers
{
    [RoutePrefix("nmacs")]
    public class NMACSController : ApiController
    {
        ILoggingService _loggingService;
        internal IConfigService _configService;
        string _userName = string.Empty;
        string _pwd = string.Empty;
        public NMACSController(ILoggingService loggingService)
        {
            DbInteractionSQL _dbInteractionSQL = new DbInteractionSQL(System.Configuration.ConfigurationManager.AppSettings["PGConnectionLogPath"], System.Configuration.ConfigurationManager.AppSettings["DBConfigPath"]);
            string connectionstring = _dbInteractionSQL.GetConnectionStringfromXML(System.Configuration.ConfigurationManager.AppSettings["PPOConnectionString"]);
            _userName = connectionstring.Split(';')[0];
            _pwd = connectionstring.Split(';')[1];
            _loggingService = loggingService;
        }
        [CustomAuthorize]
        [Route("getnmacsdetails")]
        [HttpPost]
       
        public async Task<HttpResponseMessage> GetDetails(object JsonObj)
        {
            //commnented this line -- for QA serve certificate issue

           //ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(delegate { return true; });
           var result = await PostRequestJson(JsonObj);

            return new HttpResponseMessage { Content = new StringContent(result, System.Text.Encoding.UTF8, "application/json") };
        }
        public async Task<string> PostRequestJson(object json)
        {
            var result = "";
            string jsonResponse = string.Empty;
            string uriVal = string.Empty;
            string searchType = string.Empty;
            string keyName = string.Empty;
            string finalJsonInput = string.Empty;
            try
            {
                // Create string to hold JSON response
                JObject obj = JObject.Parse(json.ToString());
                int count = 0;
                foreach (var item in obj)
                {
                    keyName = item.Key;
                    searchType = item.Value["searchType"].ToString().ToUpper();
                    count = item.Value.Count();
                }
                int index = count - 1;
                obj[keyName].ToArray()[index].Remove();
                finalJsonInput = JsonConvert.SerializeObject(obj, Formatting.Indented);

                uriVal = System.Configuration.ConfigurationManager.AppSettings[searchType];
            
                //string username = System.Configuration.ConfigurationManager.AppSettings["username"];
                //string password = System.Configuration.ConfigurationManager.AppSettings["password"];

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(uriVal);
                Encoding encoding = new UTF8Encoding();
                string JSONRequest = finalJsonInput; //json.ToString();
                byte[] data = encoding.GetBytes(JSONRequest);

                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                string _auth = string.Format("{0}:{1}", _userName, _pwd);
                string _enc = Convert.ToBase64String(Encoding.ASCII.GetBytes(_auth));
                string _cred = string.Format("{0} {1}", "Basic", _enc);
                httpWebRequest.Headers[HttpRequestHeader.Authorization] = _cred;
                httpWebRequest.ContentLength = data.Length;

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(JSONRequest);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                    result = result.ToString().Replace("\\", "");
                }
            }
            catch (Exception ex)
            {
                _loggingService.Error(ex);
                throw new Exception("Error");
            }
            return JsonConvert.SerializeObject(result);
        }
    }
}