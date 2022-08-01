namespace Schema.TracingCore.Models
{
    public class FeatureServiceInfo
    {
        public long ObjectId { get; set; }
        public string AssetGroup { get; set; }
        public string AssetType { get; set; }
        public string GlobalId { get; set; }
        public long LayerId { get; set; }
        public int TerminalId { get; set; } = 1;
        public double PercentAlong { get; set; } = -1;
        public string Tier { get; set; } = string.Empty;
        public string TargetTier { get; set; } = string.Empty;
    }
}
