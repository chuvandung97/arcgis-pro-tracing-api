using Schema.TracingCore.Models;
using Schema.TracingCore.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.TracingCore.Services
{
    public interface ITraceService
    {
        Task<Dictionary<string, object>> RunTrace(List<NetworkInfo> parameters);
        string GetLayerDefs(FeatureServiceInfo featureServiceInfo);
        Dictionary<string, string> GetTraceConfig(FeatureServiceInfo featureService, List<NetworkInfo> parameters);
    }
}
