using Schema.Core.Services;
using Schema.Web.AuthorizeUser;
using System.Threading.Tasks;
using System.Web.Http;

namespace Schema.Web.Controllers
{
    [RoutePrefix("trace")]
    public class TraceController : BaseApiController
    {
        ITraceService _traceService;
        ILoggingService _loggingService;
        public TraceController(ITraceService TraceService, ILoggingService loggingService)
        {
            _traceService = TraceService;
            _loggingService = loggingService;
        }

        //[Route("cables")]
        //[HttpGet]
        //public async Task<IHttpActionResult> CablesAsync(string Geometry, string Voltage, string Subtypecd)
        //{
        //    var result = await _traceService.CablesAsync(Geometry, Voltage, Subtypecd);
        //    return Ok(result);
        //}

        //[Route("barriers")]
        //[HttpGet]
        //public async Task<IHttpActionResult> BarriersAsync(string Geometry)
        //{
        //    var result = await _traceService.BarriersAsync(Geometry);
        //    return Ok(result);
        //}

        //[Route("substation")]
        //[HttpGet]
        //public async Task<IHttpActionResult> SubstationsAsync(int Eid, int Fid)
        //{
        //    var result = await _traceService.SubstationsAsync(Eid, Fid);
        //    return Ok(result);
        //}

        //[Route("substation/distribution")]
        //[HttpGet]
        //public async Task<IHttpActionResult> DistributionTraceAsync(string Geometry, string Voltage, string Subtypecd)
        //{
        //    var result = await _traceService.DistributionTraceAsync(Geometry, Voltage, Subtypecd);
        //    return Ok(result);
        //}

        //[Route("substation/distribution/desktop")]
        //[HttpGet]
        //public async Task<IHttpActionResult> DistributionTraceAsync(string GlobalIds)
        //{
        //    var result = await _traceService.DistributionTraceAsync(GlobalIds);
        //    return Ok(result);
        //}

        //[Route("substation/transmission")]
        //[HttpGet]
        //public async Task<IHttpActionResult> TransmissionTraceAsync(string Geometry, string Voltage, string Subtypecd)
        //{
        //    var result = await _traceService.TransmissionTraceAsync(Geometry, Voltage, Subtypecd);
        //    return Ok(result);
        //}

        //[Route("downstream")]
        //[HttpGet]
        //public async Task<IHttpActionResult> DownstreamTraceAsync(int EdgeId, int FeederId, int VoltageDepth, string Barriers)
        //{
        //    var result = await _traceService.DownstreamTraceAsync(EdgeId, FeederId, VoltageDepth, Barriers);
        //    return Ok(result);
        //}

        //[Route("upstream")]
        //[HttpGet]
        //public async Task<IHttpActionResult> UpstreamTraceAsync(int EdgeId, int FeederId)
        //{
        //    var result = await _traceService.UpstreamTraceAsync(EdgeId, FeederId);
        //    return Ok(result);
        //}

        //[Route("sld/ring")]
        //[HttpGet]
        //public async Task<IHttpActionResult> SLDRingTraceAsync(string Geometry)
        //{
        //    var result = await _traceService.SLDRingTraceAsync(Geometry);
        //    return Ok(result);
        //}

        //[Route("sld/multiring")]
        //[HttpGet]
        //public async Task<IHttpActionResult> SLDMultiRingTraceAsync(string Geometry)
        //{
        //    var result = await _traceService.SLDMultiRingTraceAsync(Geometry);
        //    return Ok(result);
        //}

        //[Route("sld/ring/desktop")]
        //[HttpGet]
        //public async Task<IHttpActionResult> SLDRingTraceForDesktopAsync(string GlobalIds)
        //{
        //    var result = await _traceService.SLDRingTraceDesktopAsync(GlobalIds);
        //    return Ok(result);
        //}

        //[Route("gis/ring/desktop")]
        //[HttpGet]
        //public async Task<IHttpActionResult> GISRingTraceForDesktopAsync(int SLDFeederID)
        //{
        //    var result = await _traceService.GISRingTraceForDesktopAsync(SLDFeederID);
        //    return Ok(result);
        //}

        //[Route("sld/multiring/desktop")]
        //[HttpGet]
        //public async Task<IHttpActionResult> SLDMultiRingTraceForDesktopAsync(string GlobalIds)
        //{
        //    var result = await _traceService.SLDMultiRingTraceDesktopAsync(GlobalIds);
        //    return Ok(result);
        //}

        //[Route("sld/xvoltage")]
        //[HttpGet]
        //public async Task<IHttpActionResult> SLDXVoltageTraceAsync(string Geometry)
        //{
        //    var result = await _traceService.SLDXVoltageTraceAsync(Geometry);
        //    return Ok(result);
        //}

        //New
        [CustomAuthorize]
        [Route("getcabletrace")]
        [HttpGet]
        public async Task<IHttpActionResult> CableTraceAsync(string Geometry = null, string Voltage = null, string Subtypecd = null, string GlobalIds = null)
        {
            var result = await _traceService.CableTraceAsync(Geometry, Voltage, Subtypecd, GlobalIds);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getcontextmenucabletrace")]
        [HttpGet]
        public async Task<IHttpActionResult> ContextMenuCableTraceAsync(string CableIds)
        {
            var result = await _traceService.ContextMenuCableTraceAsync(CableIds);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getsldsinglering")]
        [HttpGet]
        public async Task<IHttpActionResult> SLDSingleRingTraceAsync(string Geometry = null, string GlobalIds = null)
        {
            var result = await _traceService.SLDSingleRingTraceAsync(Geometry, GlobalIds);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getsldmultiring")]
        [HttpGet]
        public async Task<IHttpActionResult> SLDMultiRingTraceAsync(string Geometry = null, string GlobalIds = null)
        {
            var result = await _traceService.SLDMultiRingTraceAsync(Geometry, GlobalIds);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getsldringreport")]
        [HttpGet]
        public async Task<IHttpActionResult> SLDRingReportAsync(string FeederIds)
        {
            var result = await _traceService.SLDRingReportAsync(FeederIds);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getsldpropagatering")]
        [HttpGet]
        public async Task<IHttpActionResult> SLDPropagateRingAsync(int SLDFeederID)
        {
            var result = await _traceService.SLDPropagateRingAsync(SLDFeederID);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getsldxvoltagesubstations")]
        [HttpGet]
        public async Task<IHttpActionResult> SLDXVoltSubstationsAsync(string Name = null, string RequestFlag="XVOLT")
        {
            var result = await _traceService.SLDXVoltSubstationsAsync(Name, RequestFlag);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getsldxvoltagetransformers")]
        [HttpGet]
        public async Task<IHttpActionResult> SLDXVoltTransformersAsync(string MRC)
        {
            var result = await _traceService.SLDXVoltTransformersAsync(MRC);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getsldsinglefeeders")]
        [HttpGet]
        public async Task<IHttpActionResult> SLDSingleFeedersAync(string Geometry, bool IsMultiFeeder = false)
        {
            var result = await _traceService.SLDSingleFeedersAync(Geometry, IsMultiFeeder);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getsldxvoltagebarriers")]
        [HttpGet]
        public async Task<IHttpActionResult> SLDXVoltageSelectBarriersAsync(string Geometry)
        {
            var result = await _traceService.SLDXVoltageSelectBarriersAsync(Geometry);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getsldxvoltage")]
        [HttpGet]
        public async Task<IHttpActionResult> SLDXVoltTraceAsync(int TransformerId, int Voltage, string Barriers = null)
        {
            var result = await _traceService.SLDXVoltTraceAsync(TransformerId, Voltage, Barriers);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getsldcustomercount")]
        [HttpGet]       
        /*public async Task<IHttpActionResult> SLDCustomerCountTraceAsync(string Geometry)
        {
            var result = await _traceService.SLDCustomerCountTraceAsync(Geometry);
            return Ok(result);
        }*/
        //Sandip
        public async Task<IHttpActionResult> SLDCustomerCountTraceAsync(int FeederId, int ElementId, string Barriers = null, int Direction = 0)
        {
            var result = await _traceService.SLDCustomerCountTraceAsync(FeederId, ElementId, Barriers, Direction);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getlvogbtrace")]
        [HttpGet]
        public async Task<IHttpActionResult> LVOGBTraceAsync(int Direction, string Geometry = null, string GlobalId = null)
        {
            var result = await _traceService.LVOGBTraceAsync(Direction, Geometry, GlobalId);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getplanningcables")]
        [HttpGet]
        public async Task<IHttpActionResult> PlanningSelectCablesAsync(string Geometry, string Voltage, string Subtypecd)
        {
            var result = await _traceService.PlanningSelectCablesAsync(Geometry, Voltage, Subtypecd);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getplanningbarriers")]
        [HttpGet]
        public async Task<IHttpActionResult> PlanningSelectBarriersAsync(string Geometry)
        {
            var result = await _traceService.PlanningSelectBarriersAsync(Geometry);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getplanningdownstream")]
        [HttpGet]
        public async Task<IHttpActionResult> PlanningDownstreamTraceAsync(int EdgeId, int FeederId, int VoltageDepth, string Barriers = null)
        {
            var result = await _traceService.PlanningDownstreamTraceAsync(EdgeId, FeederId, VoltageDepth, Barriers);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("getplanningupstream")]
        [HttpGet]
        public async Task<IHttpActionResult> PlanningUpstreamTraceAsync(int EdgeId, int FeederId)
        {
            var result = await _traceService.PlanningUpstreamTraceAsync(EdgeId, FeederId);
            return Ok(result);
        }
    }
}