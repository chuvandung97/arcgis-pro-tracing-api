using Schema.TracingCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.TracingCore.Factory
{
    public abstract class TraceFactory
    {
        public static ITraceService GetTraceType(string traceType)
        {
            ITraceService traceService = null;
            if(traceType == "feeder")
            {
                traceService = new FeederTraceService();
            }
            /*if(traceType == "circuit")
            {
                traceService = new CircuitTraceService();
            }*/
            return traceService;
        }

    }
}
