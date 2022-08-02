using System.Collections.Generic;

namespace Schema.TracingCore.Models
{
    public class SequenceCircuitTraceTemp
    {
        public int? Rank { get; set; }
        public long? EID { get; set; }
        public string EGID { get; set; } = string.Empty;
        public List<long?> FIDs { get; set; }
        public List<long?> TIDs { get; set; }
        public List<long?> Path { get; set; }
    }
}
