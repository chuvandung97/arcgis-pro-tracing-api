using Schema.Core.Services;
using Schema.Web.AuthorizeUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Schema.Web.Controllers
{
    [RoutePrefix("sld")]
    public class SLDController : BaseApiController
    {
        ISLDService _sldService;
        ILoggingService _loggingService;
        public SLDController(ISLDService SLDService, ILoggingService loggingService)
        {
            _sldService = SLDService;
            _loggingService = loggingService;
        }
        [CustomAuthorize]
        [Route("getsldsubstationlist")]
        public async Task<IHttpActionResult> GetSubstationListAsync(int Segment = 0, string SubstationName = null, string SheetID = null, string Voltage = null)
        {
            var result = await _sldService.GetSubstationListAsync(Segment, SubstationName, SheetID, Voltage);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("propagatesldtogis")]
        [HttpGet]
        public async Task<IHttpActionResult> PropagateSLDToGISAsync(string Voltage, string Geometry, string MapSheetID)
        {
            var result = await _sldService.PropagateSLDToGISAsync(Voltage, Geometry, MapSheetID);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("propagategistosld")]
        [HttpGet]
        public async Task<IHttpActionResult> PropagateGISToSLDAsync(string Voltage, string Geometry)
        {
            var result = await _sldService.PropagateGISToSLDAsync(Voltage, Geometry);
            return Ok(result);
        }
        /*[CustomAuthorize]
        [Route("getsheetIDs")]
        [HttpGet]
        public async Task<IHttpActionResult> SheetIDsListingAsync()
        {
            var result = await _sldService.SheetIDsListingAsync();
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("jumptodestination")]
        [HttpGet]
        public async Task<IHttpActionResult> JumpToDestinationAsync(string Geometry)
        {
            var result = await _sldService.JumpToDestinationAsync(Geometry);
            return Ok(result);
        }*/
        [CustomAuthorize]
        [Route("propagatesldtogiscablelength")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCableLengthAsync(string Voltage, string Geometry, string MapSheetID)
        {
            var result = await _sldService.GetCableLengthAsync(Voltage, Geometry, MapSheetID);
            return Ok(result);
        }
        //added by Sandip on 2nd April 2019 for RFC0018439 -- To track and log SLD searched mapsheet in database 
        [CustomAuthorize]
        [Route("insertsldmapsheet")]
        [HttpPost]
        public async Task<IHttpActionResult> InsertSLDMapSheetInfoAsync(object JsonObj)
        {
            var result = await _sldService.InsertSearchedLocationInDBAsync(JsonObj);
            return Ok(result);
        }
        //added by Sandip on 2nd April 2019 for RFC0018439 -- To track and log SLD searched substation in database 
        [CustomAuthorize]
        [Route("insertsldsubstation")]
        [HttpPost]
        public async Task<IHttpActionResult> InsertSLDSubstationInfoAsync(object JsonObj)
        {
            var result = await _sldService.InsertSearchedLocationInDBAsync(JsonObj);
            return Ok(result);
        }
    }
}