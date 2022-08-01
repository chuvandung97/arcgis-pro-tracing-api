using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Schema.Core.Services;
using Schema.Web.AuthorizeUser;
using System.Web.Http;
using System.Threading.Tasks;
using System.Security.Principal;
using System.DirectoryServices.AccountManagement;

namespace Schema.Web.Controllers
{
    [RoutePrefix("usagetracking")]
    public class UsageTrackingController : BaseApiController
    {
        // GET: UsageTracking
        IUsageTrackingService _usageTrackingService;
        ILoggingService _loggingService;
        public UsageTrackingController(IUsageTrackingService UsageTrackingService, ILoggingService loggingService)
        {
            _usageTrackingService = UsageTrackingService;
            _loggingService = loggingService;
        }
        //added by Sandip on 9th April 2019 for RFC0018439 -- This api returns the list of users reporting under the logged in manager 
        //and their thresold limit to access the Schema application.
        //added by Sandip on 8th Jan 2020 for RFC0023594 -- API modified to allow ReportOfficerID as an input parameter
        [CustomAuthorize]
        [Route("getuconstatsuserthreshold")]
        public async Task<IHttpActionResult> GetUsageTrackingUserThresholdAync(string ReportOfficerID = null)
        {
            if (string.IsNullOrEmpty(ReportOfficerID))
            {
                ReportOfficerID = ((IPrincipal)User).Identity.Name;
                string[] splitString = ReportOfficerID.Split('\\');
                ReportOfficerID = splitString[splitString.Length - 1].Trim();
            }
            var result = await _usageTrackingService.GetUsageTrackingUserThresholdAync(ReportOfficerID);
            return Ok(result);
        }
        //added by Sandip on 17th April 2019 for RFC0018439 -- This api returns the section wise thresold limit for accessing the Schema application.
        //added by Sandip on 8th Jan 2020 for RFC0023594 -- API modified to allow ReportOfficerID as an input parameter
        [CustomAuthorize]
        [Route("getuconstatssectionthreshold")]
        public async Task<IHttpActionResult> GetUsageTrackingSectionThresholdAync(string ReportOfficerID = null)
        {
            if (string.IsNullOrEmpty(ReportOfficerID))
            {
                ReportOfficerID = ((IPrincipal)User).Identity.Name;
                string[] splitString = ReportOfficerID.Split('\\');
                ReportOfficerID = splitString[splitString.Length - 1].Trim();
            }
            var result = await _usageTrackingService.GetUsageTrackingSectionThresholdAync(ReportOfficerID);
            return Ok(result);
        }
        //added by Sandip on 22nd April 2019 for RFC0018439 -- This api returns the list of those users only who have accessed Schema application for logged in manager
        //added by Sandip on 19th Aug 2019 for RFC0020665 -- Added FromDate and ToDate parameters
        [CustomAuthorize]
        [Route("getuconstatsschemauser")]
        public async Task<IHttpActionResult> GetUsageTrackingSchemaUserAync(string FromDate, string ToDate, string ReportOfficerID = null)
        {
            if (string.IsNullOrEmpty(ReportOfficerID))
            {
                ReportOfficerID = ((IPrincipal)User).Identity.Name;
                string[] splitString = ReportOfficerID.Split('\\');
                ReportOfficerID = splitString[splitString.Length - 1].Trim();
            }
            var result = await _usageTrackingService.GetUsageTrackingSchemaUserAync(ReportOfficerID, FromDate, ToDate);
            return Ok(result);
        }
        //added by Sandip on 14th May 2019 for RFC0018439 -- This api returns Total Accessible, Unaccessible, Blocked and Unblocked users count for logged in manager
        //added by Sandip on 19th Aug 2019 for RFC0020665 -- Added FromDate and ToDate parameters
        [CustomAuthorize]
        [Route("getuconstatssummary")]
        public async Task<IHttpActionResult> GetUsageTrackingSummaryAsync(string FromDate, string ToDate, string ReportOfficerID = null)
        {
            if (string.IsNullOrEmpty(ReportOfficerID))
            {
                ReportOfficerID = ((IPrincipal)User).Identity.Name;
                string[] splitString = ReportOfficerID.Split('\\');
                ReportOfficerID = splitString[splitString.Length - 1].Trim();
            }
            var result = await _usageTrackingService.GetUsageTrackingSummaryAsync(ReportOfficerID, FromDate, ToDate);
            return Ok(result);
        }
        //added by Sandip on 15th May 2019 for RFC0018439 -- This api returns map sheet information accessed by user in both GIS and SLD
        //added by Sandip on 19th Aug 2019 for RFC0020665 -- Added FromDate and ToDate parameters
        [CustomAuthorize]
        [Route("getuconstatsmapsheets")]
        public async Task<IHttpActionResult> GetUsageTrackingMapSheetsAsync(string UserID, string FromDate, string ToDate)
        {

            var result = await _usageTrackingService.GetUsageTrackingMapSheetsAsync(UserID, FromDate, ToDate);
            return Ok(result);
        }
        //added by Sandip on 7th May 2019 for RFC0018439 -- This api returns the list of Dormant Users who have not accessed the Schema application more than 90 days.
        [CustomAuthorize]
        [Route("getuconstatsdormantuser")]
        public async Task<IHttpActionResult> GetUsageTrackingDormantUserAync(string ReportOfficerID = null)
        {

            if (string.IsNullOrEmpty(ReportOfficerID))
            {
                ReportOfficerID = ((IPrincipal)User).Identity.Name;
                string[] splitString = ReportOfficerID.Split('\\');
                ReportOfficerID = splitString[splitString.Length - 1].Trim();
            }
            var result = await _usageTrackingService.GetUsageTrackingDormantUserAync(ReportOfficerID);
            return Ok(result);
        }
        //added by Sandip on 3rd May 2019 for RFC0018439 -- Unblocks blocked Schema users. 
        [CustomAuthorize]
        [Route("updateuconstatsunblockuser")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateUsageTrackingUnBlockUserAsync(object JsonObj)
        {
            string ReportOfficerID = ((IPrincipal)User).Identity.Name;
            string[] splitString = ReportOfficerID.Split('\\');
            ReportOfficerID = splitString[splitString.Length - 1].Trim();

            var result = await _usageTrackingService.UpdateUsageTrackingUnBlockUserAsync(JsonObj, ReportOfficerID);
            return Ok(result);
        }
        //added by Sandip on 5th May 2019 for RFC0018439 -- Updates new user thresold value 
        [CustomAuthorize]
        [Route("updateuconstatsuserthreshold")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateUsageTrackingUserThresholdAsync(object JsonObj)
        {
            int isAdmin = 0;
            string ReportOfficerID = ((IPrincipal)User).Identity.Name;
            string[] splitString = ReportOfficerID.Split('\\');
            ReportOfficerID = splitString[splitString.Length - 1].Trim();

            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "singaporepower.local");
            UserPrincipal user = UserPrincipal.FindByIdentity(ctx, HttpContext.Current.User.Identity.Name);
            if (user != null)
            {
                PrincipalSearchResult<Principal> groups = user.GetGroups();
                foreach (Principal p in groups)
                {
                    if (p is GroupPrincipal)
                    {
                        if (p.Name.ToUpper() == "GEMS_USAGE_TRACKING_ADMIN")
                            isAdmin = 1;                        
                    }
                }
            }

            var result = await _usageTrackingService.UpdateUsageTrackingUserThresholdAsync(JsonObj, ReportOfficerID, isAdmin);
            return Ok(result);
        }
        //added by Sandip on 17th May 2019 for RFC0018439 -- Updates new section thresold value
        [CustomAuthorize]
        [Route("updateuconstatssectionthreshold")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateUsageTrackingSectionThresholdAsync(object JsonObj)
        {
            int isAdmin = 0;
            string ReportOfficerID = ((IPrincipal)User).Identity.Name;
            string[] splitString = ReportOfficerID.Split('\\');
            ReportOfficerID = splitString[splitString.Length - 1].Trim();

            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "singaporepower.local");
            UserPrincipal user = UserPrincipal.FindByIdentity(ctx, HttpContext.Current.User.Identity.Name);
            if (user != null)
            {
                PrincipalSearchResult<Principal> groups = user.GetGroups();
                foreach (Principal p in groups)
                {
                    if (p is GroupPrincipal)
                    {
                        if (p.Name.ToUpper() == "GEMS_USAGE_TRACKING_ADMIN")
                            isAdmin = 1;
                    }
                }
            }
            var result = await _usageTrackingService.UpdateUsageTrackingSectionThresholdAsync(JsonObj, ReportOfficerID, isAdmin);
            return Ok(result);
        }
        //added by Sandip on 22th May 2019 for RFC0018439 -- Deletes the updated user threshold value and resets it back to original thresold limit.
        [CustomAuthorize]
        [Route("deleteuconstatsuserthreshold")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteUsageTrackingUserThresholdAsync(object JsonObj)
        {
            int isAdmin = 0;
            string ReportOfficerID = ((IPrincipal)User).Identity.Name;
            string[] splitString = ReportOfficerID.Split('\\');
            ReportOfficerID = splitString[splitString.Length - 1].Trim();

            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "singaporepower.local");
            UserPrincipal user = UserPrincipal.FindByIdentity(ctx, HttpContext.Current.User.Identity.Name);
            if (user != null)
            {
                PrincipalSearchResult<Principal> groups = user.GetGroups();
                foreach (Principal p in groups)
                {
                    if (p is GroupPrincipal)
                    {
                        if (p.Name.ToUpper() == "GEMS_USAGE_TRACKING_ADMIN")
                            isAdmin = 1;
                    }
                }
            }

            var result = await _usageTrackingService.DeleteUsageTrackingUserThresholdAsync(JsonObj, ReportOfficerID, isAdmin);
            return Ok(result);
        }
        //added by Sandip on 24th May 2019 for RFC0018439 -- Get complete list of Schema batch jobs and their status
        [CustomAuthorize]
        [Route("getuconstatsjobinfo")]
        public async Task<IHttpActionResult> GetUCONStatsJobInfoAsync()
        {
            var result = await _usageTrackingService.GetUsageTrackingJobInfoAync();
            return Ok(result);
        }
        //added by Sandip on 25th May 2019 for RFC0018439 -- Resets the 'PROCESSING' or 'FAIL' batch job status to 'PENDING'
        [CustomAuthorize]
        [Route("updateuconstatsjobinfo")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateUCONStatsJobInfoAsync()
        {
            var result = await _usageTrackingService.UpdateUsageTrackingJobInfoAsync();
            return Ok(result);
        }
        //added by Sandip on 19th Aug 2019 for RFC0020665 -- This api returns list of usernames on click of each type in summary of Schema Stats.
        [CustomAuthorize]
        [Route("getuconstatslistusers")]
        public async Task<IHttpActionResult> GetUsageTrackingListOfUsernameAsync(Int16 Type, string FromDate, string ToDate, Int16 DirectFlag = 0, string ReportOfficerID = null)
        {
            if (string.IsNullOrEmpty(ReportOfficerID))
            {
                ReportOfficerID = ((IPrincipal)User).Identity.Name;
                string[] splitString = ReportOfficerID.Split('\\');
                ReportOfficerID = splitString[splitString.Length - 1].Trim();
            }
            var result = await _usageTrackingService.GetUsageTrackingListOfUsernameAsync(ReportOfficerID, Type, FromDate, ToDate, DirectFlag);
            return Ok(result);
        }
        //added by Sandip on 04th Sept 2019 -- This api returns list of blocked user history for the logged in ROI.
        [CustomAuthorize]
        [Route("getuconstatsblockedhistory")]
        public async Task<IHttpActionResult> GetUsageTrackingBlockedUserHistoryAsync(Int16 Type, Int16 DirectFlag = 0, string ReportOfficerID = null)
        {
            if (string.IsNullOrEmpty(ReportOfficerID))
            {
                ReportOfficerID = ((IPrincipal)User).Identity.Name;
                string[] splitString = ReportOfficerID.Split('\\');
                ReportOfficerID = splitString[splitString.Length - 1].Trim();
            }
            var result = await _usageTrackingService.GetUsageTrackingBlockedUserHistoryAsync(ReportOfficerID, Type, DirectFlag = 0);
            return Ok(result);
        }
        /*public void WriteErrorLog(string message)
        {
            string _filePath = System.Configuration.ConfigurationManager.AppSettings["LogPath"];
            string content = DateTime.Now + Environment.NewLine + message + Environment.NewLine;
            System.IO.File.AppendAllText(_filePath, content);
        }*/
    }
}