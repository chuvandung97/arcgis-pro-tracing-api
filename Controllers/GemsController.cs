using Schema.Core.Services;
using Schema.Web.AuthorizeUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Http;

namespace Schema.Web.Controllers
{
    [RoutePrefix("gems")]
    public class GemsController : BaseApiController
    {
        IGemsService _gemsService;
        ILoggingService _loggingService;

        public GemsController(IGemsService GemsService, ILoggingService loggingService)
        {
            _gemsService = GemsService;
            _loggingService = loggingService;
            //_customLogService = CustomLogService;
        }
        [CustomAuthorize]
        [Route("getasbuiltdrawings")]
        [HttpGet]
        public async Task<IHttpActionResult> AsBuiltDrawingsAsync(string Voltage, string Geometry, string SubtypeCD)
        {
            //var logInfo = await _customLogService.LogInfo();
            var result = await _gemsService.AsBuiltDrawingsAsync(Voltage, Geometry, SubtypeCD);

            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getxsectioninfo")]
        public async Task<IHttpActionResult> GetXSectionInfoAsync(string Geometry, string Query, int CriteriaID)
        {
            var result = await _gemsService.GetXSectionInfoAsync(Geometry, Query, CriteriaID);
            return Ok(result);
        }
        /*[CustomAuthorize]
        [Route("getgridgeometry")]
        public async Task<IHttpActionResult> GetGridGeometryAsync(string SegmentID)
        {
            var result = await _gemsService.GetGridGeometryAsync(SegmentID);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getsegmentgeometry")]
        public async Task<IHttpActionResult> GetSchematicSegmentGeomAsync(int SegNum, string SheetID)
        {
            var result = await _gemsService.GetSchematicSegmentGeomAsync(SegNum, SheetID);
            return Ok(result);
        }*/
        [CustomAuthorize]
        [Route("getfeaturedetailsfornmacs")]
        public async Task<IHttpActionResult> GetNMACSDetailsAsync(double X = 0.0, double Y = 0.0, string Voltage = null, string VoltageType = null, string Geometry = null, string MapSheetID = null)
        {
            var result = await _gemsService.GetNMACSDetailsAsync(X, Y, Voltage, VoltageType, Geometry, MapSheetID);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getbuildingcustomercount")]
        public async Task<IHttpActionResult> GetCustomerCountAsync(string Geometry = null, double X = 0.0, double Y = 0.0)
        {
            string UserID = ((IPrincipal)User).Identity.Name;
            string[] splitString = UserID.Split('\\');
            UserID = splitString[splitString.Length - 1].Trim();

            var result = await _gemsService.GetCustomerCountAsync(UserID, Geometry, X, Y);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("get66kvcustomercountlayers")]
        public async Task<IHttpActionResult> Get66kvSupplyZoneLayersAsync()
        {
            var result = await _gemsService.Get66kvSupplyZoneLayersAsync();
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("get66kvcustomercount")]
        public async Task<IHttpActionResult> Get66KVCustomerCountAsync(double X, double Y)
        {
            var result = await _gemsService.Get66KVCustomerCountAsync(X, Y);
            return Ok(result);
        }
        /*[HttpPost]
        [CustomAuthorize]
        [Route("getamioutage")]
        public async Task<IHttpActionResult> GetAMIOutageCustomerCountAsync(object JsonObj)
        {
            var result = await _gemsService.GetAMIOutageCustomerCountAsync(JsonObj);
            return Ok(result);
        }

        [Route("gettifweb")]
        public async Task<IHttpActionResult> GetTifWebAsync(string Geometry, string Query, int CriteriaID)
        {
            var result = await _gemsService.GetTifWebAsync(Geometry, Query, CriteriaID);
            return Ok(result);
        }
        [Route("gettifmob")]
        public async Task<IHttpActionResult> GetTifMobAsync(string Geometry, string Query, int CriteriaID)
        {
            var result = await _gemsService.GetTifMobAsync(Geometry, Query, CriteriaID);
            return Ok(result);
        }*/

    }
}