namespace Schema.TracingCore.Models
{
    public class FeederTraceResult
    {
        public int? Rank1 { get; set; }
        public int Rank2 { get; set; }
        public long? EID { get; set; }
        public string EGID { get; set; } = string.Empty;
        public string FIDs { get; set; } = string.Empty;
        public string TIDs { get; set; } = string.Empty;
        public bool EndFlag { get; set; } = false;
        public string Path2 { get; set; } = string.Empty;
        public string Path1 { get; set; } = string.Empty;

    }
}
