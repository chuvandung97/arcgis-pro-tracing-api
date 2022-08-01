using Autofac;
using Schema.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Services
{
    public class ServiceModule : Module
    {
        public ServiceModule() { }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SearchService>().As<ISearchService>();
            builder.RegisterType<SLDService>().As<ISLDService>();
            builder.RegisterType<GemsService>().As<IGemsService>();
            builder.RegisterType<TraceService>().As<ITraceService>();
            builder.RegisterType<DMISService>().As<IDMISService>();
            builder.RegisterType<OWTSIRService>().As<IOWTSIRService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<SLDReportService>().As<ISLDReportService>();
            builder.RegisterType<QAQCService>().As<IQAQCService>();
            builder.RegisterType<AdminService>().As<IAdminService>();
            builder.RegisterType<SupplyZoneService>().As<ISupplyZoneService>();
            builder.RegisterType<SupplyZoneOldService>().As<ISupplyZoneOldService>();
            builder.RegisterType<UsageTrackingService>().As<IUsageTrackingService>();
            builder.RegisterType<CustomAuthorizeService>().As<ICustomAuthorizeService>();
            builder.RegisterType<POVerificationService>().As<IPOVerificationService>();
            builder.RegisterType<IncidentService>().As<IIncidentService>();
            builder.RegisterType<GasInternalPipeDrawingsService>().As<IGasInternalPipeDrawingsService>();
            base.Load(builder);
        }
    }
}
