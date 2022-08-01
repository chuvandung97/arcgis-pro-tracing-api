using Schema.Core.Services;
using Schema.Web.AuthorizeUser;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Schema.Web.Controllers
{
    [RoutePrefix("admin")]
    public class AdminController : BaseApiController
    {
        IAdminService _adminService;
        ILoggingService _loggingService;

        public AdminController(IAdminService AdminService, ILoggingService loggingService)
        {
            _adminService = AdminService;
            _loggingService = loggingService;
        }
        [CustomAuthorize]
        [Route("gethitcount")]
        [HttpGet]
        public async Task<IHttpActionResult> GetHitCountAsync()
        {
            var result = await _adminService.GetHitCountAsync();
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getweeklyloginactivity")]
        [HttpGet]
        public async Task<IHttpActionResult> GetWeeklyLoginActivityAsync()
        {
            var result = await _adminService.GetWeeklyLoginActivityAsync();
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getmapserviceusage")]
        [HttpGet]
        public async Task<IHttpActionResult> GetEachMapServiceUsageAsync()
        {
            var result = await _adminService.GetEachMapServiceUsageAsync();
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getfunctionusage")]
        [HttpGet]
        public async Task<IHttpActionResult> GetEachFunctionUsageAync()
        {
            var result = await _adminService.GetEachFunctionUsageAync();
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("geterrorusage")]
        [HttpGet]
        public async Task<IHttpActionResult> GetEachErrorUsageAsync()
        {
            var result = await _adminService.GetEachErrorUsageAsync();
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getdailyuseractivities")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDailyUserActivitiesAsync(int Year, int Month)
        {
            var result = await _adminService.GetDailyUserActivitiesAsync(Year, Month);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getdailybrowseractivities")]
        [HttpGet]
        public async Task<IHttpActionResult> GetBrowserActivitiesAsync(int Year, int Month)
        {
            var result = await _adminService.GetBrowserActivitiesAsync(Year, Month);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("insertnewurlkeyinconfigurl")]
        [HttpPost]
        public async Task<IHttpActionResult> InsertConfigUrlMappingAsync(object JsonObj)
        {
            var result = await _adminService.InsertConfigUrlMappingAsync(JsonObj);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("updateexistingurlkeyinconfigurl")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateConfigUrlMappingAsync(object JsonObj)
        {
            var result = await _adminService.UpdateConfigUrlMappingAsync(JsonObj);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("deleteurlkeyfromconfigurl")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteConfigUrlMappingAsync(object JsonObj)
        {
            var result = await _adminService.DeleteConfigUrlMappingAsync(JsonObj);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("geturlkeysfromconfigurl")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUrlKeysFromConfigURLAsync()
        {
            var result = await _adminService.GetUrlKeysFromConfigURLAsync();
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("insertacmapi")]
        [HttpPost]
        public async Task<IHttpActionResult> InsertACMAPIAsync(object JsonObj)
        {
            var result = await _adminService.InsertACMAPIAsync(JsonObj);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("updateacmapi")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateACMAPIAsync(object JsonObj)
        {
            var result = await _adminService.UpdateACMAPIAsync(JsonObj);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("deleteacmapi")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteACMAPIAsync(object JsonObj)
        {
            var result = await _adminService.DeleteACMAPIAsync(JsonObj);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getacmapi")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAPINameAsync()
        {
            var result = await _adminService.GetAPINameAsync();
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("insertacmfunctionality")]
        [HttpPost]
        public async Task<IHttpActionResult> InsertACMFunctionalityAsync(object JsonObj)
        {
            var result = await _adminService.InsertACMFunctionalityAsync(JsonObj);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("updateacmfunctionality")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateACMFunctionalityAsync(object JsonObj)
        {
            var result = await _adminService.UpdateACMFunctionalityAsync(JsonObj);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("deleteacmfunctionality")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteACMFunctionalityAsync(object JsonObj)
        {
            var result = await _adminService.DeleteACMFunctionalityAsync(JsonObj);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getacmfunctionality")]
        [HttpGet]
        public async Task<IHttpActionResult> GetACMModuleNameAsync()
        {
            var result = await _adminService.GetACMModuleNameAsync();
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("insertacmadgroup")]
        [HttpPost]
        public async Task<IHttpActionResult> InsertACMADGroupAsync(object JsonObj)
        {
            var result = await _adminService.InsertACMADGroupAsync(JsonObj);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("updateacmadgroup")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateACMADGroupAsync(object JsonObj)
        {
            var result = await _adminService.UpdateACMADGroupAsync(JsonObj);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("deleteacmadgroup")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteACMADGroupAsync(object JsonObj)
        {
            var result = await _adminService.DeleteACMADGroupAsync(JsonObj);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getacmadgroup")]
        [HttpGet]
        public async Task<IHttpActionResult> GetACMADGroupAsync()
        {
            var result = await _adminService.GetACMADGroupAsync();
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("insertadgrptofuncmapping")]
        [HttpPost]
        public async Task<IHttpActionResult> InsertADGroupToFunctionMappingAsync(object JsonObj)
        {
            var result = await _adminService.InsertADGroupToFunctionMappingAsync(JsonObj);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("deleteadgrptofuncmapping")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteADGroupToFunctionMappingAsync(object JsonObj)
        {
            var result = await _adminService.DeleteADGroupToFunctionMappingAsync(JsonObj);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getadgrptofuncmapping")]
        [HttpGet]
        public async Task<IHttpActionResult> GetADGroupToFunctionMappingAsync(int FunctionalityKey = 0, int ADGroupKey = 0)
        {
            var result = await _adminService.GetADGroupToFunctionMappingAsync(FunctionalityKey, ADGroupKey);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("insertadgrptoapimapping")]
        [HttpPost]
        public async Task<IHttpActionResult> InsertADGroupToAPIMappingAsync(object JsonObj)
        {
            var result = await _adminService.InsertADGroupToAPIMappingAsync(JsonObj);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("deleteadgrptoapimapping")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteADGroupToAPIMappingAsync(object JsonObj)
        {
            var result = await _adminService.DeleteADGroupToAPIMappingAsync(JsonObj);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getadgrptoapimapping")]
        [HttpGet]
        public async Task<IHttpActionResult> GetADGroupToAPIMappingAsync(int APIKey = 0, int ADGroupKey = 0)
        {
            var result = await _adminService.GetADGroupToAPIMappingAsync(APIKey, ADGroupKey);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getapimapsheetuserlist")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAPIMapSheetUserListAsync()
        {
            HashSet<Dictionary<string, object>> results = new HashSet<Dictionary<string, object>>();
            var httpContext = HttpContext.Current;
            IPrincipal principal = httpContext.User as IPrincipal;
            var identity = principal;
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "singaporepower.local");
            UserPrincipal user = UserPrincipal.FindByIdentity(ctx, httpContext.User.Identity.Name);
            StringBuilder adRoles = new StringBuilder();
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
            if (adRoles.ToString().ToUpper().Contains("ITD_MAPS"))
            {
                results = await _adminService.GetAPIMapSheetUserListAsync();
            }
            return Ok(results);
        }
        //added by Sandip on 22nd Oct 2019 for RFC0021956 -- This api returns list of Project officers/MEA Editors based on UserType.
        [CustomAuthorize]
        [Route("getpovpomealist")]
        public async Task<IHttpActionResult> GetPOMEAListAsync(string UserType)
        {
            string ProjectOfficerID = ((IPrincipal)User).Identity.Name;
            string[] splitString = ProjectOfficerID.Split('\\');
            ProjectOfficerID = splitString[splitString.Length - 1].Trim();

            var result = await _adminService.GetPOMEAListAsync(ProjectOfficerID, UserType);
            return Ok(result);
        }
        //added by Sandip on 4th Sept 2019 for RFC0021956 -- This api returns complete list of PO JobInfo table. It is used in admin portal.
        [CustomAuthorize]
        [Route("getallpovpojobinfodata")]
        public async Task<IHttpActionResult> GetCompletePOJobInfoDataAsync(string UserType)
        {
            string OfficerID = ((IPrincipal)User).Identity.Name;
            string[] splitString = OfficerID.Split('\\');
            OfficerID = splitString[splitString.Length - 1].Trim();

            var result = await _adminService.GetCompletePOJobInfoDataAsync(UserType, OfficerID);
            return Ok(result);
        }
        //added by Sandip on 22nd Aug 2019 for RFC0021956 -- This api returns complete list of POVID's that are assigned to PO for verification. It is used in admin portal.
        [CustomAuthorize]
        [Route("getallpovidstoupdatepo")]
        public async Task<IHttpActionResult> GetAllPOVIDSToUpdatePOInfoAsync(string RejectSession = null)
        {
            var result = await _adminService.GetAllPOVIDSToUpdatePOInfoAsync(RejectSession);
            return Ok(result);
        }
        //added by Sandip on  14th Aug 2019 for RFC0021956 -- This api will be consumed from admin portal to update Project Officer ID for a particular POVID.
        [CustomAuthorize]
        [Route("updatepovpoid")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdatePOIDAsync(object JsonObj)
        {
            var result = await _adminService.UpdatePOIDAsync(JsonObj);
            return Ok(result);
        }
        //added by Sandip on  22th Sept 2019 for RFC0021956 -- This api will be consumed from admin portal to reject sessions by MEA.
        [CustomAuthorize]
        [Route("updatepovmearejectsession")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateMEARejectSessionAsync(object JsonObj)
        {
            var result = await _adminService.UpdateMEARejectSessionAsync(JsonObj);
            return Ok(result);
        }
        //added by Sandip on 25th Oct 2019 for RFC0021956 -- This api returns complete contractor info list.
        [CustomAuthorize]
        [Route("getpovcontractorinfo")]
        public async Task<IHttpActionResult> GetPOVContractorInfoAsync()
        {
            var result = await _adminService.GetPOVContractorInfoAsync();
            return Ok(result);
        }
        //added by Sandip on 25th Oct 2019 for RFC0021956 -- This api updates contractor information based on unique contractor id.
        [CustomAuthorize]
        [Route("updatepovcontractorinfo")]
        public async Task<IHttpActionResult> UpdatePOVContractorInfoAsync(object JsonObj)
        {
            string OfficerID = ((IPrincipal)User).Identity.Name;
            string[] splitString = OfficerID.Split('\\');
            OfficerID = splitString[splitString.Length - 1].Trim();

            var result = await _adminService.UpdatePOVContractorInfoAsync(JsonObj, OfficerID);
            return Ok(result);
        }
        //added by Sandip on 25th Oct 2019 for RFC0021956 -- This api inserts contractor information in the table.
        [CustomAuthorize]
        [Route("insertpovcontractorinfo")]
        public async Task<IHttpActionResult> InsertPOVContractorInfoAsync(object JsonObj)
        {
            string OfficerID = ((IPrincipal)User).Identity.Name;
            string[] splitString = OfficerID.Split('\\');
            OfficerID = splitString[splitString.Length - 1].Trim();

            var result = await _adminService.InsertPOVContractorInfoAsync(JsonObj, OfficerID);
            return Ok(result);
        }
        //added by Sandip on 4th Nov 2019 for RFC0021956 -- This api returns complete history of the session from created to final posted stage.
        [CustomAuthorize]
        [Route("getpovsessionhistory")]
        public async Task<IHttpActionResult> GetPOVSessionHistoryAsync(string UserType = null, string FromDate = null, string ToDate = null, string POVID = null)
        {
            string OfficerID = null;
            if (string.IsNullOrEmpty(POVID))
            {
                OfficerID = ((IPrincipal)User).Identity.Name;
                string[] splitString = OfficerID.Split('\\');
                OfficerID = splitString[splitString.Length - 1].Trim();
            }
            var result = await _adminService.GetPOVSessionHistoryAsync(UserType, OfficerID, FromDate, ToDate, POVID);
            return Ok(result);
        }
        //added by Sandip on 18th Aug 2021 for RFC0034769 -- This api updates job status of the session from Admin portal.
        [CustomAuthorize]
        [Route("updatepovjobstatus")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdatePOVJobStatusAsync(object JsonObj)
        {
            string UserID = ((IPrincipal)User).Identity.Name;
            string[] splitString = UserID.Split('\\');
            UserID = splitString[splitString.Length - 1].Trim();

            var result = await _adminService.UpdatePOVJobStatusAsync(JsonObj, UserID);
            return Ok(result);
        }
        //added by Sandip on 2nd Mar 2020 for RFC0024551 -- This api returns the list of jobs details.
        [CustomAuthorize]
        [Route("getjobsync")]
        [HttpGet]
        public async Task<IHttpActionResult> GetJobSyncAsync()
        {
            string UserID = ((IPrincipal)User).Identity.Name;
            string[] splitString = UserID.Split('\\');
            UserID = splitString[splitString.Length - 1].Trim();

            var result = await _adminService.GetJobSyncAsync();
            return Ok(result);
        }
        //added by Sandip on 2nd Mar 2020 for RFC0024551 -- This api inserts new job info details in the job sync table.
        [CustomAuthorize]
        [Route("insertjobsync")]
        [HttpPost]
        public async Task<IHttpActionResult> InsertJobSyncAsync(object JsonObj)
        {
            string UserID = ((IPrincipal)User).Identity.Name;
            string[] splitString = UserID.Split('\\');
            UserID = splitString[splitString.Length - 1].Trim();

            var result = await _adminService.InsertJobSyncAsync(JsonObj, UserID);
            return Ok(result);
        }
        //added by Sandip on 2nd Mar 2020 for RFC0024551 -- This api updates existing job info details in the job sync table.
        [CustomAuthorize]
        [Route("updatejobsync")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateJobSyncAsync(object JsonObj)
        {
            string UserID = ((IPrincipal)User).Identity.Name;
            string[] splitString = UserID.Split('\\');
            UserID = splitString[splitString.Length - 1].Trim();

            var result = await _adminService.UpdateJobSyncAsync(JsonObj, UserID);
            return Ok(result);
        }
        //added by Sandip on 17th Oct 2020 for RFC0029411 -- This api returns the list of problematic cable joints for the admin portal.
        [CustomAuthorize]
        [Route("getpcjdata")]
        [HttpGet]
        public async Task<IHttpActionResult> GetProblematicCableJointAsync()
        {
            var result = await _adminService.GetProblematicCableJointAsync();
            return Ok(result);
        }
        //added by Sandip on 17th Oct 2020 for RFC0029411 -- This api returns the report path of the pre-generated problematic cable joint
        [CustomAuthorize]
        [Route("getpcjreport")]
        [HttpGet]
        public async Task<IHttpActionResult> GetProblematicCableJointReportPathAsync()
        {
            var result = await _adminService.GetProblematicCableJointReportPathAsync();
            return Ok(result);
        }
        //added by Sandip on 17th Oct 2020 for RFC0029411 -- This api inserts new problematic cable joint data.
        [CustomAuthorize]
        [Route("insertpcjdata")]
        [HttpPost]
        public async Task<IHttpActionResult> InsertProblematicCableJointDataAsync(object JsonObj)
        {
            string UserID = ((IPrincipal)User).Identity.Name;
            string[] splitString = UserID.Split('\\');
            UserID = splitString[splitString.Length - 1].Trim();

            var result = await _adminService.InsertProblematicCableJointDataAsync(JsonObj, UserID);
            return Ok(result);
        }       
        //Commented by Sandip on 3rd June 2019
        /*[CustomAuthorize]
        [Route("getapimapsheetreport")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAPIMapSheetReportAsync(string InputDate, string ReportType, string Username = null)
        {
            var httpContext = HttpContext.Current;
            IPrincipal principal = httpContext.User as IPrincipal;
            var identity = principal;
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "singaporepower.local");
            UserPrincipal user = UserPrincipal.FindByIdentity(ctx, httpContext.User.Identity.Name);
            StringBuilder adRoles = new StringBuilder();
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();

            if (string.IsNullOrEmpty(Username) || (string.IsNullOrWhiteSpace(Username)))
            {
                Username = user.DisplayName;
                //Username = "Tan Teck Wee";
                result = await _adminService.GetAPIMapSheetReportAsync(InputDate, ReportType, Username);
            }
            else
            {
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
                if (adRoles.ToString().ToUpper().Contains("ITD_MAPS"))
                {
                    result = await _adminService.GetAPIMapSheetReportAsync(InputDate, ReportType, Username);
                }
            }
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getapimapsheetsummaryreport")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAPIMapSheetSummaryReportAsync(string Username = null)
        {
            var httpContext = HttpContext.Current;
            IPrincipal principal = httpContext.User as IPrincipal;
            var identity = principal;
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "singaporepower.local");
            UserPrincipal user = UserPrincipal.FindByIdentity(ctx, httpContext.User.Identity.Name);
            StringBuilder adRoles = new StringBuilder();
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();

            if (string.IsNullOrEmpty(Username) || (string.IsNullOrWhiteSpace(Username)))
            {
                Username = user.DisplayName;
                //Username = "Tan Teck Wee";
                result = await _adminService.GetAPIMapSheetSummaryReportAsync(Username);
            }
            else
            {
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
                if (adRoles.ToString().ToUpper().Contains("ITD_MAPS"))
                {
                    result = await _adminService.GetAPIMapSheetSummaryReportAsync(Username);
                }
            }
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getapimapsheetgeometry")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAPIMapSheetGeometryAsync(string InputDate, string ReportType, string Username)
        {
            var result = await _adminService.GetAPIMapSheetGeometryAsync(InputDate, ReportType, Username);
            return Ok(result);
        }*/
    }
}