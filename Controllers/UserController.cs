using Schema.Core.Services;
using Schema.Web.AuthorizeUser;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Net.Sockets;
using System.Net;
using System.Collections;

namespace Schema.Web.Controllers
{
    [RoutePrefix("user")]
    public class UserController : BaseApiController
    {
        IUserService _userService;
        ILoggingService _loggingService;
        public UserController(IUserService UserService, ILoggingService loggingService)
        {
            _userService = UserService;
            _loggingService = loggingService;
        }
        [CustomAuthorize]
        [Route("getuserdetails")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUserDetailsAsync()
        {
            var httpContext = HttpContext.Current;
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
                        adRoles.Append("SINGAPOREPOWER\\" + p.Name.ToUpper());
                        adRoles.Append(",");
                    }
                }
            }
            //remove Sandip
            /*if (httpContext.User.Identity.Name.ToUpper().Contains("NITINBABAN") || httpContext.User.Identity.Name.ToUpper().Contains("TSAP352U"))
            {
                adRoles.Append("SINGAPOREPOWER\\GEMS_VIEWER_DISTRIBUTION");
            }
            adRoles.Append(",");*/


            /*string mandatoryRole = System.Configuration.ConfigurationManager.AppSettings["MandatoryADRole"];

            if (!adRoles.ToString().ToUpper().Contains(mandatoryRole))
            {
                if (adRoles.ToString().ToUpper().Contains("ITD_MAPS"))
                {
                    adRoles.Append(mandatoryRole);
                    adRoles.Append(",");
                }
            }*/

            //remove later on 6th Apr         
            /*if (httpContext.User.Identity.Name.ToUpper().Contains("TSUB295X"))
            {
                adRoles.Append("SINGAPOREPOWER\\GEMS_PO_Admin");
            }
            if (httpContext.User.Identity.Name.ToUpper().Contains("TTUV691M"))
            {
                adRoles.Append("SINGAPOREPOWER\\GEMS_PO_ELECTRICITY");
            }
            if (httpContext.User.Identity.Name.ToUpper().Contains("TARS179N"))
            {
                adRoles.Append("SINGAPOREPOWER\\ELECTRICITY DISTRIBUTION WRITER");
            }
            adRoles.Append(",");*/
            //remove later on 6th Apr

            string userName = httpContext.User.Identity.Name;
            string[] splitString = userName.Split('\\');
            userName = splitString[splitString.Length - 1].Trim();

            var results = await _userService.GetUserDetailsAsync(adRoles.ToString().Substring(0, adRoles.ToString().Length - 1).ToUpper(), userName);
            //var results = await _userService.GetUserDetailsAsync("SINGAPOREPOWER\\AG_VPN_SCHEMA", "tsap352u");
            return Ok(results);
        }
        //added by Sandip on 17th May 2019 for RFC0018439 -- To get the logged in user roles, user type and security clearance details
        //[CustomAuthorize]
        [Route("getuserroles")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUserRolesAsync(bool StatsFlag = false)
        {
            var httpContext = HttpContext.Current;
            IPrincipal principal = httpContext.User as IPrincipal;
            var identity = principal;
            StringBuilder adRoles = new StringBuilder();
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "singaporepower.local");
            UserPrincipal user = UserPrincipal.FindByIdentity(ctx, httpContext.User.Identity.Name);
            Dictionary<string, object> results = new Dictionary<string, object>();

            if (user != null)
            {
                PrincipalSearchResult<Principal> groups = user.GetGroups();
                foreach (Principal p in groups)
                {
                    if (p is GroupPrincipal)
                    {
                        adRoles.Append("SINGAPOREPOWER\\" + p.Name.ToUpper());
                        adRoles.Append(",");
                    }
                }
            }
            //remove Sandip
            /*if (httpContext.User.Identity.Name.ToUpper().Contains("NITINBABAN") || httpContext.User.Identity.Name.ToUpper().Contains("TSAP352U"))
            {
                adRoles.Append("SINGAPOREPOWER\\GEMS_VIEWER_DISTRIBUTION");
            }
            adRoles.Append(",");*/

            //remove later on 7th Apr
            /*if (httpContext.User.Identity.Name.ToUpper().Contains("RAJEEV"))
            {
                adRoles.Append("SINGAPOREPOWER\\GEMS_PO_Admin");
            }
            if (httpContext.User.Identity.Name.ToUpper().Contains("TSAP352U"))
            {
                adRoles.Append("SINGAPOREPOWER\\GEMS_MEA_Admin");
            }
            if (httpContext.User.Identity.Name.ToUpper().Contains("NITINBABAN"))
            {
                adRoles.Append("SINGAPOREPOWER\\GEMS_PO_Section_Admin");
            }
            if (httpContext.User.Identity.Name.ToUpper().Contains("TSUB295X"))
            {
                adRoles.Append("SINGAPOREPOWER\\GEMS_PO_Admin");
            }
            if (httpContext.User.Identity.Name.ToUpper().Contains("TTUV691M"))
            {
                adRoles.Append("SINGAPOREPOWER\\GEMS_PO_Electricity");
            }
            if (httpContext.User.Identity.Name.ToUpper().Contains("TARS179N"))
            {
                adRoles.Append("SINGAPOREPOWER\\ELECTRICITY DISTRIBUTION WRITER");
            }*/
            //remove later on 5th Apr

            //adRoles.Append("SINGAPOREPOWER\\SPIS_ITSEC_GEMS_EDITOR");

            results.Add("UserRoles", adRoles);
            //Get Username
            string Username = user.DisplayName;
            results.Add("Username", Username);

            //Get UserID
            string UserID = httpContext.User.Identity.Name;
            string[] splitString = UserID.Split('\\');
            UserID = splitString[splitString.Length - 1].Trim();

            //Get Usertype
            var userType = await _userService.GetUserTypeAsync(UserID, StatsFlag);

            string UserTypeVal = string.Empty;
            foreach (Dictionary<string, object> item in (IEnumerable)userType)
            {
                if (item["security"] != null)
                    UserTypeVal = item["security"].ToString();
                break;
            }
            results.Add("UserType", UserTypeVal);
            return Ok(results);
        }
        //added by Sandip on 5th April 2021 for RFC0018439 -- To put restrictions on exposed URLs. Allow users with GEMS AD group only to access this url.
        [CustomAuthorize]
        [Route("geturls")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUrlsAsync()
        {
            var httpContext = HttpContext.Current;
            IPrincipal principal = httpContext.User as IPrincipal;
            var identity = principal;

            //WriteErrorLog(((IPrincipal)User).Identity.Name);
            var results = await _userService.GetUrlsAsync();
            return Ok(results);
        }
        //added by Sandip on 4th April 2019 for RFC0018439 -- To check whether the logged in user account is blocked or not.
        //It also checks whether the user has access to specific map service or not.
        [Route("getuserstatus")]
        [HttpGet]
        public async Task<IHttpActionResult> CheckUserStatus(string UserID, string MapServiceURL = null)
        {
            if ((MapServiceURL.ToLower().Contains("schemastats")))
            {
                var result = "{\"status\":0,\"message\":\"Success\",\"result\":null}";
                return Ok(result);
            }
            else
            {
                var result = await _userService.CheckUserStatus(UserID, MapServiceURL);
                return Ok(result);
            }
        }
        public void WriteErrorLog(string message)
        {
            string _filePath = System.Configuration.ConfigurationManager.AppSettings["LogPath"];
            string content = DateTime.Now + Environment.NewLine + message + Environment.NewLine;
            System.IO.File.AppendAllText(_filePath, content);
        }
    }
}