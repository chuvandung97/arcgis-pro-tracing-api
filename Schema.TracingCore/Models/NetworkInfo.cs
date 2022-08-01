namespace Schema.TracingCore.Models
{
    public class NetworkInfo
    {
        public string NetworkId { get; set; }
        public int TerminalId { get; set; } = 1;
        public double PercentAlong { get; set; } = -1;
        public string Tier { get; set; } = string.Empty;
        public string TargetTier { get; set; } = string.Empty;
    }
}
