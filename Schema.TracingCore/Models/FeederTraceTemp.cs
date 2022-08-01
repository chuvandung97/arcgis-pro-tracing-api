using System.Collections.Generic;

namespace Schema.TracingCore.Models
{
    public class FeederTraceTemp
    {
        public int? Rank1 { get; set; }
        public int Rank2 { get; set; }
        public long? EID { get; set; }
        public string EGID { get; set; } = string.Empty;
        public List<long?> FIDs { get; set; }
        public List<long?> TIDs { get; set; }
        public List<long?> Path2 { get; set; }
        public List<long?> Path1 { get; set; }

    }
}
