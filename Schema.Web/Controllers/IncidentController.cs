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
using System.Web.Security;

namespace Schema.Web.Controllers
{
    [RoutePrefix("incidentmanagement")]
    public class IncidentController : BaseApiController
    {
        IIncidentService _incidentService;
        ILoggingService _loggingService;

        public IncidentController(IIncidentService IncidentService, ILoggingService loggingService)
        {
            _incidentService = IncidentService;
            _loggingService = loggingService;
        }
        //added by Sandip on 05th Feb 2020 for RFC0024551 -- This api returns the incident information.
        [CustomAuthorize]
        [Route("getincident")]
        [HttpGet]
        public async Task<IHttpActionResult> GetHTOutageIncidentAsync(string RequestType, string IncidentID, string FromDate = null, string ToDate = null)
        {
            var result = await _incidentService.GetHTOutageIncidentAsync(RequestType, IncidentID, FromDate, ToDate);
            return Ok(result);
        }
        //added by Sandip on 11th Feb 2020 for RFC0024551 -- This api returns the information details of the particular incident.
        [CustomAuthorize]
        [Route("getincidentdetails")]
        [HttpGet]
        public async Task<IHttpActionResult> GetHTOutageIncidentDetailsAsync(Int64 IncidentID)
        {
            var result = await _incidentService.GetHTOutageIncidentDetailAsync(IncidentID);
            return Ok(result);
        }
        //added by Sandip on 14th Feb 2020 for RFC0024551 -- This api inserts newly created incident details in database.
        [CustomAuthorize]
        [Route("insertincident")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateHTOutageIncidentAsync(object JsonObj)
        {
            string UserID = ((IPrincipal)User).Identity.Name;
            string[] splitString = UserID.Split('\\');
            UserID = splitString[splitString.Length - 1].Trim();

            var result = await _incidentService.CreateHTOutageIncidentAsync(JsonObj, UserID);
            return Ok(result);
        }
        //added by Sandip on 18th Feb 2020 for RFC0024551 -- This api inserts manually searched records based on 'Postal Code/Block Number/By selecting building on map' by users.
        [CustomAuthorize]
        [Route("insertincidentdetailbyuser")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateHTOutageIncidentByUserAsync(object JsonObj)
        {
            string UserID = ((IPrincipal)User).Identity.Name;
            string[] splitString = UserID.Split('\\');
            UserID = splitString[splitString.Length - 1].Trim();

            var result = await _incidentService.CreateHTOutageIncidentByUserAsync(JsonObj, UserID);
            return Ok(result);
        }
        //added by Sandip on 20th Feb 2020 for RFC0024551 -- This api allows user to update the incident details.
        [CustomAuthorize]
        [Route("updateincident")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateHTOutageIncidentAsync(object JsonObj)
        {
            string UserID = ((IPrincipal)User).Identity.Name;
            string[] splitString = UserID.Split('\\');
            UserID = splitString[splitString.Length - 1].Trim();

            var result = await _incidentService.UpdateHTOutageIncidentAsync(JsonObj, UserID);
            return Ok(result);
        }
        //added by Sandip on 21th Feb 2020 for RFC0024551 -- This api allows to update the customer status of the incident.
        [CustomAuthorize]
        [Route("updateincidentcustomerstatus")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateHTOutageAffectedCustomerStatusAsync(object JsonObj)
        {
            string UserID = ((IPrincipal)User).Identity.Name;
            string[] splitString = UserID.Split('\\');
            UserID = splitString[splitString.Length - 1].Trim();

            var result = await _incidentService.UpdateHTOutageAffectedCustomerStatusAsync(JsonObj, UserID);
            return Ok(result);
        }
        //added by Sandip on 24th Feb 2020 for RFC0024551 -- This api allows to delete indiviual customer of the particular incident.
        [CustomAuthorize]
        [Route("deleteincidentdetailbyuser")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteHTOutageIncidentByUserAsync(object JsonObj)
        {
            string UserID = ((IPrincipal)User).Identity.Name;
            string[] splitString = UserID.Split('\\');
            UserID = splitString[splitString.Length - 1].Trim();

            var result = await _incidentService.DeleteHTOutageIncidentByUserAsync(JsonObj, UserID);
            return Ok(result);
        }
        //added by Sandip on 25th Feb 2020 for RFC0024551 -- This api is consumed by admin portal to delete any previously created incident. 
        [CustomAuthorize]
        [Route("deleteincident")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteHTOutageIncidentAsync(object JsonObj)
        {
            string UserID = ((IPrincipal)User).Identity.Name;
            string[] splitString = UserID.Split('\\');
            UserID = splitString[splitString.Length - 1].Trim();

            var result = await _incidentService.DeleteHTOutageIncidentAsync(JsonObj, UserID);
            return Ok(result);
        }
        //added by Sandip on 27th Feb 2020 for RFC0024551 -- This api is used to generate incident boundary in Schema. 
        [CustomAuthorize]
        [Route("generateincidentboundary")]
        [HttpPost]
        public async Task<IHttpActionResult> GenerateIncidentBoundaryAsync(object JsonObj)
        {
            string UserID = ((IPrincipal)User).Identity.Name;
            string[] splitString = UserID.Split('\\');
            UserID = splitString[splitString.Length - 1].Trim();

            var result = await _incidentService.GenerateIncidentBoundaryAsync(JsonObj, UserID);
            return Ok(result);
        }
        //added by Sandip on 28th Feb 2020 for RFC0024551 -- This api returns the transformer list for the searched substation based on MRC. 
        [CustomAuthorize]
        [Route("getincidenttransformers")]
        [HttpGet]
        public async Task<IHttpActionResult> GetIncidentTransformersAsync(string MRC)
        {
            string UserID = ((IPrincipal)User).Identity.Name;
            string[] splitString = UserID.Split('\\');
            UserID = splitString[splitString.Length - 1].Trim();

            var result = await _incidentService.GetIncidentTransformersAsync(MRC);
            return Ok(result);
        }
        //added by Sandip on 01st March 2020 for RFC0024551 -- This api perform trace based on feeder/ transformer and generates affected customers and returns network data.
        [CustomAuthorize]
        [Route("getincidentcustomertrace")]
        [HttpGet]
        public async Task<IHttpActionResult> GetIncidentCustomerTraceAsync(Int64 IncidentID, string TRFIDs = null, string FID = null, string EID = null, string DirectionFlag = null, string SLDBarriers = null, string GISBarriers = null, string OverwriteFlag = null)
        {
            string UserID = ((IPrincipal)User).Identity.Name;
            string[] splitString = UserID.Split('\\');
            UserID = splitString[splitString.Length - 1].Trim();

            var result = await _incidentService.GetIncidentCustomerTraceAsync(IncidentID, UserID, TRFIDs, FID, EID, DirectionFlag, SLDBarriers, GISBarriers, OverwriteFlag);
            return Ok(result);
        }
        //added by Sandip on 08th May 2020 for RFC0021956 -- This api returns the transformer details within a specified extent.
        [CustomAuthorize]
        [Route("getincidentselecttransformers")]
        [HttpGet]
        public async Task<IHttpActionResult> GetIncidentTransformersOnMapSelectAsync(string Geometry)
        {
            string UserID = ((IPrincipal)User).Identity.Name;
            string[] splitString = UserID.Split('\\');
            UserID = splitString[splitString.Length - 1].Trim();

            var result = await _incidentService.GetIncidentTransformersOnMapSelectAsync(Geometry);
            return Ok(result);
        }
        //added by Sandip on 27th May 2020 for RFC00XXXX -- This api inserts user entered feedback from the intranet application into database table.
        [CustomAuthorize]
        [Route("insertfeedback")]
        [HttpPost]
        public async Task<IHttpActionResult> InsertFeedbackAsync(object JsonObj)
        {
            var httpContext = HttpContext.Current;
            string UserName = string.Empty;
            string EmailID = string.Empty;

            string UserID = httpContext.User.Identity.Name;
            string[] splitString = UserID.Split('\\');
            UserID = splitString[splitString.Length - 1].Trim();
                       
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "singaporepower.local");
            UserPrincipal user = UserPrincipal.FindByIdentity(ctx, UserID);
            UserName = user.DisplayName;
            EmailID = user.EmailAddress;

            var result = await _incidentService.InsertFeedbackAsync(JsonObj, UserID, UserName, EmailID);
            return Ok(result);
        }
    }
}