using Schema.Core.Services;
using Schema.Web.AuthorizeUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Schema.Web.Controllers
{
    [RoutePrefix("oldsupplyzone")]
    public class SupplyZoneOldController : BaseApiController
    {
        // GET: SupplyZone
        ISupplyZoneOldService _supplyZoneService;
        ILoggingService _loggingService;
        public SupplyZoneOldController(ISupplyZoneOldService SupplyZoneOldService, ILoggingService loggingService)
        {
            _supplyZoneService = SupplyZoneOldService;
            _loggingService = loggingService;
        }
        [CustomAuthorize]
        [Route("oldgetszeventdata")]
        [HttpGet]
        public async Task<IHttpActionResult> BindEventDataAsync(string EventID)
        {
            var results = await _supplyZoneService.BindEventDataAsync(EventID);
            return Ok(results);
        }
        [CustomAuthorize]
        [Route("oldgetszeffectedboundary")]
        [HttpGet]
        public async Task<IHttpActionResult> GetEffectedBoundaryAsync(string EventID)
        {
            var results = await _supplyZoneService.GetEffectedBoundaryAsync(EventID);
            return Ok(results);
        }
        [CustomAuthorize]
        [Route("oldgetszsummarydata")]
        [HttpGet]
        public async Task<IHttpActionResult> RetrieveSummaryDataAsync(string EventID)
        {
            var results = await _supplyZoneService.RetrieveSummaryDataAsync(EventID);
            return Ok(results);
        }
        [CustomAuthorize]
        [Route("oldgetszallevent")]
        [HttpGet]
        public async Task<IHttpActionResult> RetrieveEventAllAsync()
        {
            var results = await _supplyZoneService.RetrieveEventAllAsync();
            return Ok(results);
        }
        /*[CustomAuthorize]
        [Route("getszlatestevent")]
        [HttpGet]
        public async Task<IHttpActionResult> RetrieveLatestEventAsync()
        {
            var results = await _supplyZoneService.RetrieveLatestEventAsync();
            return Ok(results);
        }*/
        [CustomAuthorize]
        [Route("oldgetszeventtotalcount")]
        [HttpGet]
        public async Task<IHttpActionResult> RetrieveEventTotalCountAsync(string EventID)
        {
            var results = await _supplyZoneService.RetrieveEventTotalCountAsync(EventID);
            return Ok(results);
        }
        [CustomAuthorize]
        [Route("oldgetszeventtotalmrccount")]
        [HttpGet]
        public async Task<IHttpActionResult> RetrieveEventTotalMRCCountAsync(string MRC)
        {
            var results = await _supplyZoneService.RetrieveEventTotalMRCCountAsync(MRC);
            return Ok(results);
        }
        [CustomAuthorize]
        [Route("oldgetszbldgcount")]
        [HttpGet]
        public async Task<IHttpActionResult> RetrieveEventBuildingCountAsync(string EventID)
        {
            var results = await _supplyZoneService.RetrieveEventBuildingCountAsync(EventID);
            return Ok(results);
        }
        [CustomAuthorize]
        [Route("oldgetszstcount")]
        [HttpGet]
        public async Task<IHttpActionResult> RetrieveEventStreetCountAsync(string EventID)
        {
            var results = await _supplyZoneService.RetrieveEventStreetCountAsync(EventID);
            return Ok(results);
        }
        [CustomAuthorize]
        [Route("oldgetszbldgcountmrc")]
        [HttpGet]
        public async Task<IHttpActionResult> RetrieveEventBuildingCountMRCAsync(string MRC)
        {
            var results = await _supplyZoneService.RetrieveEventBuildingCountMRCAsync(MRC);
            return Ok(results);
        }
        [CustomAuthorize]
        [Route("oldgetszstcountmrc")]
        [HttpGet]
        public async Task<IHttpActionResult> RetrieveEventStreetCountMRCAsync(string MRC)
        {
            var results = await _supplyZoneService.RetrieveEventStreetCountMRCAsync(MRC);
            return Ok(results);
        }
        [CustomAuthorize]
        [Route("oldgetszstcountusingstcode")]
        [HttpGet]
        public async Task<IHttpActionResult> RetrieveEventStreetCountUsingStreetCodeAsync(string StreetCode, string EventID = null, string MRC = null)
        {
            var results = await _supplyZoneService.RetrieveEventStreetCountUsingStreetCodeAsync(StreetCode, EventID, MRC);
            return Ok(results);
        }
        /*[CustomAuthorize]
        [Route("getszlayermapping")]
        [HttpGet]
        public async Task<IHttpActionResult> LayerMappingAsync()
        {
            var results = await _supplyZoneService.LayerMappingAsync();
            return Ok(results);
        }*/
        [CustomAuthorize]
        [Route("oldcreateszevent")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateEventAsync(object JsonObj)
        {
            string EmpID = ((IPrincipal)User).Identity.Name;
            string[] splitString = EmpID.Split('\\');
            EmpID = splitString[splitString.Length - 1].Trim();

            var result = await _supplyZoneService.CreateEventAsync(JsonObj, EmpID);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("oldupdateszevent")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateEventAsync(object JsonObj)
        {
            string EmpID = ((IPrincipal)User).Identity.Name;
            string[] splitString = EmpID.Split('\\');
            EmpID = splitString[splitString.Length - 1].Trim();

            var result = await _supplyZoneService.UpdateEventAsync(JsonObj, EmpID);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("oldget66kvcustomercountlayers")]
        public async Task<IHttpActionResult> Get66kvSupplyZoneLayersAsync()
        {
            var result = await _supplyZoneService.Get66kvSupplyZoneLayersAsync();
            return Ok(result);
        }
        /*[CustomAuthorize]
        [Route("binduserdetails")]
        [HttpGet]
        public async Task<IHttpActionResult> BindUserDetailsAsync()
        {
            var result = await _supplyZoneService.BindUserDetailsAsync();
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getusergroup")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUserGroupAsync(string UserID)
        {
            var result = await _supplyZoneService.GetUserGroupAsync(UserID);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("adduser")]
        [HttpPost]
        public async Task<IHttpActionResult> AddUserAsync(object JsonObj)
        {          
            var result = await _supplyZoneService.AddUserAsync(JsonObj);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("deleteuser")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteUserAsync(object JsonObj)
        {
            var result = await _supplyZoneService.DeleteUserAsync(JsonObj);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("updateuserrole")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateUserRoleAsync(object JsonObj)
        {
            var result = await _supplyZoneService.UpdateUserRoleAsync(JsonObj);
            return Ok(result);
        }*/
    }
}