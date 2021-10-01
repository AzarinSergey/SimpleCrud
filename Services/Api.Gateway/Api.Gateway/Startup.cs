using Core.Service.Host;
using Core.Service.Host.Client.ServiceCollectionExtensions;
using Gis.Contract;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Moedi.Cqrs;
using Moedi.Cqrs.Processor;
using Moedi.Data.Core.Access;
using Moedi.Data.Ef;
using System;
using System.IO;
using System.Reflection;

namespace Api.Gateway
{
    public class Startup : StatelessServiceStartup
    {
        public Startup()
        {
            ConfigureSwagger(
            op =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                op.IncludeXmlComments(xmlPath);
            },
            (provider, options) => { },
            (sp, op) =>
            {
                op.SwaggerEndpoint("/swagger/v1/swagger.json", "Simple CRUD Api");
                op.RoutePrefix = "swagger";
            });
     }

        protected override void RegisterStatelessService(IServiceCollection services)
        {
            //Register GisService as internal 
            services.RegisterInternalServiceProxy<IGisService>();
        }
    }
}
