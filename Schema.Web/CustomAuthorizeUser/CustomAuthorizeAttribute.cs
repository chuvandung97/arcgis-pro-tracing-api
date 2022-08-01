using Autofac.Integration.WebApi;
using Schema.Core.Services;
using Schema.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Threading;
using System.Web.Http.Controllers;
using System.Security.Claims;
using System.DirectoryServices.AccountManagement;
using System.Web.Mvc;
using System.Security.Principal;
using System.Text;
using Autofac.Integration.Mvc;
using System.Net.Http;
using System.Net;
using System.Collections;
using System.Net.Http.Headers;

namespace Schema.Web.AuthorizeUser
{
    public class CustomAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        IUserService _userService;
        ILoggingService _loggingService;
        bool blockedUserFlag = false;
        string UserStatus = string.Empty;
        public IUserService UserService
        {
            get
            {
                return DependencyResolver.Current.GetService<IUserService>();
            }
        }
        public ICustomAuthorizeService CustomAuthorizeService
        {
            get
            {
                //AutofacDependencyResolver.Current.ApplicationContainer.BeginLifetimeScope("AutofacWebRequest");                
                return DependencyResolver.Current.GetService<ICustomAuthorizeService>();
            }
        }
        public ILoggingService LoggingService
        {
            get
            {
                return DependencyResolver.Current.GetService<ILoggingService>();
            }
        }
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            bool returnVal = false;
            bool conditionToEnableUsageTracking = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["UCONStatsConditionToEnableUsageTracking"]);
            try
            {
                //return true;
                var httpContext = HttpContext.Current;
                //added by Sandip on 25/09/2018
                string url = string.Empty;
                string browser = HttpContext.Current.Request.Browser.Browser;
                string useragent = HttpContext.Current.Request.UserAgent;

                if (HttpContext.Current.Request.Url != null)
                    url = HttpContext.Current.Request.Url.ToString();

                // added on 22nd May 2019 to check if user is blocked or not.
                string userID = httpContext.User.Identity.Name;
                string[] userIDString = userID.Split('\\');
                userID = userIDString[userIDString.Length - 1].Trim();

                if (conditionToEnableUsageTracking == true)
                {
                    if ((url.ToLower().Contains("admin") || url.ToLower().Contains("usagetracking")))
                    {
                    }
                    else
                    {
                        var userResults = UserService.CheckAPIUserStatus(userID);
                        UserStatus = userResults.ToString();
                        if (!string.IsNullOrEmpty(UserStatus))
                        {
                            return false;
                        }
                    }
                }

                // Commented by Tulasi on 16/09/2021 for RFC0035509 - Enable access for iPad
                // Check user status ends here
                //if ((url.ToLower().Contains("sld") || url.ToLower().Contains("owtsir")) && (useragent.ToLower().Contains("ipad") || useragent.ToLower().Contains("iphone")))
                //    return false;
                // End Commented by Tulasi on 16/09/2021 for RFC0035509

                //Login all the web request in DB tables
                #region log all webrequest to DB tables
                /*var httpContext = HttpContext.Current;
                string username = httpContext.User.Identity.Name;
                string url = string.Empty;
                string parameters = string.Empty;
                string referrer = string.Empty;

                if (username.Contains('\\'))
                    username = username.Split('\\')[1].ToString();

                if (HttpContext.Current.Request.Url != null)
                    url = HttpContext.Current.Request.Url.ToString();

                if (url.Contains("?"))
                    parameters = url.Split('?')[1].ToString();

                //Get the function name
                string part = string.Empty;
                if (url.Contains("?"))
                    part = url.Substring(0, url.IndexOf('?'));
                else
                    part = url;
                string[] splitString = part.Split('/');
                string functionName = splitString[splitString.Length - 1].Trim();
                //End of function name

                string clientIP = GetClientIp(HttpContext.Current.Request);

                if (HttpContext.Current.Request.UrlReferrer != null)
                    referrer = HttpContext.Current.Request.UrlReferrer.ToString();

                string browser = HttpContext.Current.Request.Browser.Browser;
                string useragent = HttpContext.Current.Request.UserAgent;
                string type = "CUSTOM SERVICE";


                ("USERNAME: " + username);
                WriteErrorLog("URL: " + url);
                WriteErrorLog("PARAMETERS: " + parameters);
                WriteErrorLog("FUNCTION NAME: " + functionName);
                WriteErrorLog("CLIENT IP: " + clientIP);
                WriteErrorLog("REFERRER: " + referrer);
                WriteErrorLog("BROWSER: " + browser);
                WriteErrorLog("USER AGENT: " + useragent);
                WriteErrorLog("TYPE: " + type);*/
                #endregion
                //WriteErrorLog("Entered into API Authorization");
                #region get function 
                string part = string.Empty;
                if (url.Contains("?"))
                    part = url.Substring(0, url.IndexOf('?'));
                else
                    part = url;

                string[] splitString = part.Split('/');
                string functionName = splitString[splitString.Length - 1].Trim();
                #endregion

                //RFC0034115 - commented by Sandip on 07/06/2021
                /*if (!functionName.Contains("basicsearchlocation") && !functionName.Contains("insertsldmapsheet") && !functionName.Contains("insertsldsubstation"))
                {
                    var loginfo = CustomAuthorizeService.InsertFunctionLogInfoInDB();
                }*/
                //added by Sandip on 10th June 2019 for RFC0018439 to log stats request 
                if (functionName.Contains("uconstats"))
                    return true;

                #region geting DB Roles                
                var dbRoles = UserService.GetADGroupsForMethodNames(functionName);
                //WriteErrorLog("Function Name :- " + functionName);
                //WriteErrorLog("DB Roles :-" + dbRoles.ToString());
                #endregion geting DB Roles

                if (string.IsNullOrEmpty(dbRoles.ToString()))
                    return false;

                #region geting ad roles from identity
                IPrincipal principal = httpContext.User as IPrincipal;

                var identity = principal;
                StringBuilder adRoles = new StringBuilder();

                PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "singaporepower.local");
                UserPrincipal user = UserPrincipal.FindByIdentity(ctx, httpContext.User.Identity.Name);
                if (user != null)
                {
                    PrincipalSearchResult<Principal> groups = user.GetGroups();

                    foreach (Principal p in groups)
                    {
                        if (p is GroupPrincipal)
                        {
                            //WriteErrorLog("ROLES FOR USER:-" + "SINGAPOREPOWER\\" + p.Name.ToUpper());
                            adRoles.Append("SINGAPOREPOWER\\" + p.Name.ToUpper());
                            adRoles.Append(",");
                        }
                    }
                }
                #endregion geting ad roles from identity         
                //remove Sandip
                /*if (httpContext.User.Identity.Name.ToUpper().Contains("NITINBABAN") || httpContext.User.Identity.Name.ToUpper().Contains("TSAP352U"))
                {
                    adRoles.Append("SINGAPOREPOWER\\GEMS_VIEWER_DISTRIBUTION");
                }
                adRoles.Append(",");*/               

                foreach (string rolesForUser in System.Web.Security.Roles.GetRolesForUser(httpContext.User.Identity.Name))
                {
                    //WriteErrorLog("ROLES FOR USER:-" + rolesForUser);
                    adRoles.Append(rolesForUser);
                    adRoles.Append(",");
                }

                #region checking dbroles againt adroles
                string[] dbRolesArray = dbRoles.ToString().Split(',');
                //changed by Sandip on 7th Aug 2019, added "-1" in the loop
                for (int i = 0; i <= dbRolesArray.Length - 1; i++)
                {
                    if (adRoles.ToString().ToUpper().Contains(dbRolesArray[i].ToString().ToUpper()))
                    {
                        returnVal = true;
                        //WriteErrorLog(dbRolesArray[i].ToString().ToUpper());  
                        break;
                    }
                }
                //RFC0034115 --  added by Sandip to log unauthorized request in error log table
                if (returnVal == true)
                {
                    if (!functionName.Contains("basicsearchlocation") && !functionName.Contains("insertsldmapsheet") && !functionName.Contains("insertsldsubstation"))
                    {
                        var loginfo = CustomAuthorizeService.InsertFunctionLogInfoInDB();
                    }
                }
                else
                {
                    var errorLogInfo1 = CustomAuthorizeService.InsertErrorLogInfoInDB("Authorization has been denied for this request.");
                }
                #endregion
            }
            catch (Exception ex)
            {
                //WriteErrorLog("Exception :- " + ex.Message.ToString());
                if (ex.Message.Length > 2000)
                {
                    var errorLogInfo1 = CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                }
                else
                {
                    var errorLogInfo2 = CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                }
                _loggingService.Error(ex);
            }
            return returnVal;
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
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (UserStatus == "S")
            {

                HttpResponseMessage responseMessage = new HttpResponseMessage
                {
                    Content = new StringContent("{\"message\":\"Account Blocked! Your account is blocked due to excessive usage. " +
                    "An email has been sent to you and your supervisor. Please liaise with your supervisor to unblock your account.\",\"code\":\"S\"}")
                };
                responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                actionContext.Response = responseMessage;
            }
            else if (UserStatus == "D")
            {
                HttpResponseMessage responseMessage = new HttpResponseMessage
                {
                    Content = new StringContent("{\"message\":\"Account Blocked! Your account is blocked as you have not login to the system for the past 90 days. " +
                    "An email has been sent to you and your supervisor. Please liaise with your supervisor to unblock your account.\", \"code\":\"D\"}")
                };
                responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                actionContext.Response = responseMessage;
            }
            else
            {
                HttpResponseMessage responseMessage = new HttpResponseMessage
                {
                    Content = new StringContent("{\"message\":\"Authorization has been denied for this request.\"}")
                };
                responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                actionContext.Response = responseMessage;
            }
        }
        /*public void WriteErrorLog(string message)
        {
            string _filePath = System.Configuration.ConfigurationManager.AppSettings["LogPath"];
            string content = DateTime.Now + " " + " " + message + Environment.NewLine;
            System.IO.File.AppendAllText(_filePath, content);
        }*/
    }
}