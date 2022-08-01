using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Schema.Core.Services;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Schema.Core.Utilities
{
    public class CommonUtilities
    {
        ILoggingService _loggingService;
        Dictionary<string, object> errorLogInfo;
        public CommonUtilities()
        { }
        public ICustomAuthorizeService CustomAuthorizeService
        {
            get
            {
                return DependencyResolver.Current.GetService<ICustomAuthorizeService>();
            }
        }
        public async Task<string> SendEmailMethod(string fromAddress, string toAddress, string subject, string body, string ccAddress = null, string image = null)
        {
            string responseFromServer = string.Empty;
            try
            {
                string emailAPIUrl = string.Empty;
                emailAPIUrl = System.Configuration.ConfigurationManager.AppSettings["EmailAPIUrl"];
                emailAPIUrl = emailAPIUrl + fromAddress + "&toAddress=" + toAddress + "&subject=" + subject + "&body=" + body + "&ccAddress=" + ccAddress + "&imageAttachment=" + image;
                // Web Service URL
                // Create a request for the URL.         
                WebRequest request = WebRequest.Create(emailAPIUrl);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                responseFromServer = reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
            }
            return responseFromServer;
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
        public async Task<string> ConvertCsvFileToJsonObject(string path)
        {
            List<Dictionary<string, string>> listObjResult = new List<Dictionary<string, string>>();
            try
            {
                var csv = new List<string[]>();
                var lines = File.ReadAllLines(path);

                foreach (string line in lines)
                    csv.Add(line.Split(','));

                var properties = lines[0].Split(',');

                for (int i = 1; i < lines.Length; i++)
                {
                    var objResult = new Dictionary<string, string>();
                    for (int j = 0; j < properties.Length; j++)
                        objResult.Add(properties[j], csv[i][j]);

                    listObjResult.Add(objResult);
                }               
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
            }
            return JsonConvert.SerializeObject(listObjResult);
        }
    }
}
