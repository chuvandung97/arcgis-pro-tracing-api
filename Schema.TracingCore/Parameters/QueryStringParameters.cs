namespace Schema.TracingCore.Parameters
{
    public abstract class QueryStringParameters
    {
        public string NetworkInfos { get; set; }
        public string DomainNetwork { get; set; } = Constant.DOMAIN_NETWORK_NAME;
    }
}