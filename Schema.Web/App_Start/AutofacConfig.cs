using Autofac;
using Autofac.Integration.WebApi;
using Autofac.Integration.Mvc;
using Owin;
using Schema.Core.Services;
using Schema.Web.Services;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;

namespace Schema.Web
{
    public class AutofacConfig
    {
        public static void ConfigureContainer(IAppBuilder app, HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.Register(c => new ConfigService()).As<IConfigService>();
            builder.Register(c => new LoggingService()).As<ILoggingService>();

            builder.RegisterModule(new Schema.Services.ServiceModule());
            builder.RegisterModule(new Data.DataModule());

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            //app.UseAutofacMvc();
        }
    }
}