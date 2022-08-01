using Schema.TracingCore.Services;
using Schema.TracingCore.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Schema.TracingCore.Factory;
using Schema.TracingCore.Models;
using System.Threading.Tasks;

namespace Schema.Web.Controllers.TracingAPI
{
    [RoutePrefix("api/trace")]
    public class TraceApiController : BaseApiController
    {
        [Route("feeder")]
        [HttpPost]
        public async Task<Dictionary<string, List<FeederTraceResult>>> Get([FromBody] List<NetworkInfo> parameters)
        {
            var results = await TraceFactory.GetTraceType("feeder").RunTrace(parameters); 
            return results;
        }
    }
}