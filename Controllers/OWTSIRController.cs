using Schema.Core.Services;
using Schema.Web.AuthorizeUser;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Schema.Web.Controllers
{
    [RoutePrefix("owtsir")]
    public class OWTSIRController : BaseApiController
    {
        IOWTSIRService _owtsirService;
        ILoggingService _loggingService;
        public OWTSIRController(IOWTSIRService OWTSIRService, ILoggingService loggingService)
        {
            _owtsirService = OWTSIRService;
            _loggingService = loggingService;
        }
        [CustomAuthorize]
        [Route("getcompletedtasks")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCompletedTasksAync(string FromDate, string ToDate)
        {
            string Username = ((IPrincipal)User).Identity.Name;
            string[] splitString = Username.Split('\\');
            Username = splitString[splitString.Length - 1].Trim();
            var result = await _owtsirService.GetCompletedTasksAync(Username, FromDate, ToDate);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getpendingitems")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPendingItemsAync()
        {
            string Username = ((IPrincipal)User).Identity.Name;
            string[] splitString = Username.Split('\\');
            Username = splitString[splitString.Length - 1].Trim();
            var result = await _owtsirService.GetPendingItemsAync(Username);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getreadingdetails")]
        [HttpGet]
        public async Task<IHttpActionResult> GetReadingDetailsAsync(string ID)
        {
            var result = await _owtsirService.GetReadingDetailsAsync(ID);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getprevreadings")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPrevReadingsAsync(string SrcStationName, string SrcStationBoardNo, string SrcStationPanelNo, string TgtStationName, string TgtStationBoardNo, string TgtStationPanelNo)
        {
            var result = await _owtsirService.GetPrevReadingsAsync(SrcStationName, SrcStationBoardNo, SrcStationPanelNo, TgtStationName, TgtStationBoardNo, TgtStationPanelNo);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getreadingwithinextent")]
        [HttpGet]
        public async Task<IHttpActionResult> GetReadingWithinExtentAsync(string Geometry)
        {
            var result = await _owtsirService.GetReadingWithinExtentAsync(Geometry);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getowtsireditors")]
        [HttpGet]
        public async Task<IHttpActionResult> GetOWTSIREditors()
        {          
            var result = await _owtsirService.GetOWTSIREditors();
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getowtsirapprovalofficers")]
        [HttpGet]
        public async Task<IHttpActionResult> GetApprovalOfficersAsync()
        {
            string Username = ((IPrincipal)User).Identity.Name;
            string[] splitString = Username.Split('\\');
            Username = splitString[splitString.Length - 1].Trim();
            var result = await _owtsirService.GetApprovalOfficersAsync(Username);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("owtsirapprovalprocess")]
        [HttpPost]
        public async Task<IHttpActionResult> ApprovalProcessAsync(object JsonObj)
        {

            //Get complete name of the approver 
            var httpContext = HttpContext.Current;
            IPrincipal principal = httpContext.User as IPrincipal;
            var identity = principal;
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "singaporepower.local");
            UserPrincipal user = UserPrincipal.FindByIdentity(ctx, httpContext.User.Identity.Name);
            string approverName = user.DisplayName;
            //
            var result = await _owtsirService.ApprovalProcessAsync(JsonObj, approverName);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("addreading")]
        [HttpPost]
        public async Task<IHttpActionResult> AddReadingAsync(object JsonObj)
        {
            string Username = ((IPrincipal)User).Identity.Name;
            string[] splitString = Username.Split('\\');
            Username = splitString[splitString.Length - 1].Trim();
            var result = await _owtsirService.AddReadingAsync(JsonObj, Username);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("updatereading")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateReadingAsync(object JsonObj)
        {
            string Username = ((IPrincipal)User).Identity.Name;
            string[] splitString = Username.Split('\\');
            Username = splitString[splitString.Length - 1].Trim();
            var result = await _owtsirService.UpdateReadingAsync(JsonObj, Username);
            return Ok(result);
        }

    }
}