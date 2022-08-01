using Autofac;
using Schema.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Data
{
    public class DataModule : Module
    {
        public DataModule() { }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SearchDataService>().As<ISearchDataService>();
            builder.RegisterType<SLDDataService>().As<ISLDDataService>();
            builder.RegisterType<GemsDataService>().As<IGemsDataService>();
            builder.RegisterType<TraceDataService>().As<ITraceDataService>();
            builder.RegisterType<DMISDataService>().As<IDMISDataService>();
            builder.RegisterType<OWTSIRDataService>().As<IOWTSIRDataService>();
            builder.RegisterType<UserDataService>().As<IUserDataService>();
            builder.RegisterType<SLDReportDataService>().As<ISLDReportDataService>();
            builder.RegisterType<QAQCDataService>().As<IQAQCDataService>();
            builder.RegisterType<AdminDataService>().As<IAdminDataService>();
            builder.RegisterType<SupplyZoneDataService>().As<ISupplyZoneDataService>();
            builder.RegisterType<SupplyZoneOldDataService>().As<ISupplyZoneOldDataService>();
            builder.RegisterType<UsageTrackingDataService>().As<IUsageTrackingDataService>();
            builder.RegisterType<CustomAuthorizeDataService>().As<ICustomAuthorizeDataService>();
            builder.RegisterType<POVerificationDataService>().As<IPOVerificationDataService>();
            builder.RegisterType<IncidentDataService>().As<IIncidentDataService>();
            builder.RegisterType<GasInternalPipeDrawingsDataService>().As<IGasInternalPipeDrawingsDataService>();
            base.Load(builder);
        }
    }
}
