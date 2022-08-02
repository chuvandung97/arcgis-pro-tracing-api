namespace Schema.TracingCore.Models
{
    public class SequenceCircuitTraceResult
    {
        public int TraceId { get; set; }
        public int? Rank { get; set; }
        public long? EID { get; set; }
        public string EGID { get; set; } = string.Empty;
        public string FIDs { get; set; } = string.Empty;
        public string TIDs { get; set; } = string.Empty;
        public bool EndFlag { get; set; } = false;
        public string Path { get; set; } = string.Empty;
    }
}
