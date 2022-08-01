using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.TracingCore.Models
{
    public class ConnectivityItem
    {
        public int ViaNetworkSourceId { get; set; }
        public string ViaGlobalId { get; set; }
        public long ViaObjectId { get; set; }
        public int FromNetworkSourceId { get; set; }
        public string FromGlobalId { get; set; }
        public long FromObjectId { get; set; }
        public long FromTerminalId { get; set; }
        public int ToNetworkSourceId { get; set; }
        public string ToGlobalId { get; set; }
        public long ToObjectId { get; set; }
        public long ToTerminalId { get; set; }
    }
}
