using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;
using System.Net.Http;
using System.ServiceProcess;

namespace Schema.Web.Controllers
{
    [RoutePrefix("managewmi")]
    public class ManageWMIController : BaseApiController
    {

        [Route("stopsv")]
        [HttpGet]
        public async Task<HttpResponseMessage> StopService(string Name, string Agent = null)
        {
            string result = string.Empty;

            string userID = System.Configuration.ConfigurationManager.AppSettings["ValidUserID"];
            string ipAddress = System.Configuration.ConfigurationManager.AppSettings["ValidIPAddress"];
            WriteLog("StartSV UserID: " + userID);

            try
            {
                if (!string.IsNullOrEmpty(Agent) && Agent.ToUpper() == "PRTG")
                {
                    var httpContext = HttpContext.Current;
                    string username = httpContext.User.Identity.Name;
                  
                    WriteLog("StopSV Username: " + username);

                    if (!string.IsNullOrEmpty(username) && username.ToUpper() == userID.ToUpper())
                    {
                        ServiceController serviceController = new ServiceController(Name);
                        serviceController.Stop();
                        result = "Sucess";
                    }
                    else
                        result = "error: Not valid user";
                }
                else
                {
                    result = "error: Wrong Parameter";
                }
                return new HttpResponseMessage { Content = new StringContent(result, System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
                result = "error: Fail";
                return new HttpResponseMessage { Content = new StringContent(result, System.Text.Encoding.UTF8, "application/json") };
            }
        }

        [Route("startsv")]
        [HttpGet]
        public async Task<HttpResponseMessage> StartService(string Name, string Agent = null)
        {
            string result = string.Empty;
            string ipAddress = System.Configuration.ConfigurationManager.AppSettings["ValidIPAddress"];
            string userID = System.Configuration.ConfigurationManager.AppSettings["ValidUserID"];

            WriteLog("StartSV UserID: " + userID);

            try
            {
                if (!string.IsNullOrEmpty(Agent) && Agent.ToUpper() == "PRTG")
                {
                    var httpContext = HttpContext.Current;
                    string username = httpContext.User.Identity.Name;
                   
                    WriteLog("StartSV Username: " + username);

                    if (!string.IsNullOrEmpty(username) && username.ToUpper() == userID.ToUpper())
                    {
                        ServiceController serviceController = new ServiceController(Name);
                        serviceController.Start();
                        result = "Sucess";
                    }
                    else
                        result = "error: Not valid user";
                }
                else
                {
                    result = "error: Wrong Parameter";
                }
                return new HttpResponseMessage { Content = new StringContent(result, System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                result = "error: Fail";
                return new HttpResponseMessage { Content = new StringContent(result, System.Text.Encoding.UTF8, "application/json") };
            }
        }

        public void WriteLog(string message)
        {
            string _filePath = System.Configuration.ConfigurationManager.AppSettings["LogPath"];
            string content = DateTime.Now + Environment.NewLine + message + Environment.NewLine;
            System.IO.File.AppendAllText(_filePath, content);
        }
    }
}