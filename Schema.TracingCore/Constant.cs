namespace Schema.TracingCore
{
    internal class Constant
    {
        public const string DOMAIN_NETWORK_NAME = "ElectricDistribution";
        public const string ELECTRIC_DEVICE = "Electric Device";
        public const string ELECTRIC_ASSEMBLY = "Electric Assembly";
        public const string ELECTRIC_JUNCION = "Electric Junction";
        public const string ELECTRIC_LINE = "Electric Line";
        public const string ELECTRIC_SUBNETLINE = "Electric SubnetLine";
        public const string STRUCTURE_JUNCTION = "Structure Junction";
        public const string STRUCTURE_LINE = "Structure Line";
        public const string STRUCTURE_BOUNDARY = "Structure Boundary";
        public const string UTILITY_NETWORK = "Utility Network";
        public const string SERVICE_TERRITORY = "ServiceTerritory";
        //public const int SERVICE_POINT_ASSET_GROUP = 7;
        public const int SERVICE_POINT_ASSET_GROUP = 11;
        public const int BUS_BAR_ASSET_GROUP = 31;
        public enum Layers
        {
            ELECTRIC_DEVICE = 0,
            ELECTRIC_ASSEMBLY = 1,
            ELECTRIC_JUNCION = 2,
            ELECTRIC_LINE = 3,
            ELECTRIC_SUBNETLINE = 4,
            SERVICE_TERRITORY = 5,
            STRUCTURE_BOUNDARY = 6,
            STRUCTURE_JUNCTION = 7,
            STRUCTURE_LINE = 8,
            UTILITY_NETWORK = 9
        }

        /*public enum sourceMapping
        {
            STRUCTURE_JUNCTION = 4,
            STRUCTURE_LINE = 5,
            STRUCTURE_BOUNDARY = 6,
            ELECTRIC_DEVICE = 9,
            ELECTRIC_LINE = 10,
            ELECTRIC_ASSEMBLY = 11,
            ELECTRIC_JUNCION = 12,
            ELECTRIC_SUBNETLINE = 13
        }*/

        public enum sourceMapping
        {
            STRUCTURE_JUNCTION = 3,
            STRUCTURE_LINE = 4,
            STRUCTURE_BOUNDARY = 5,
            ELECTRIC_DEVICE = 6,
            ELECTRIC_LINE = 7,
            ELECTRIC_ASSEMBLY = 8,
            ELECTRIC_JUNCION = 9,
            ELECTRIC_SUBNETLINE = 10
        }
    }
}