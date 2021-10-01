using Core.Service.Host;
using Core.Service.Host.ServiceCollectionExtensions;
using Gis.Contract;
using Gis.Implementation.Service;
using Gis.Model;
using Microsoft.Extensions.DependencyInjection;
using Moedi.Data.Core.Access;
using Moedi.Data.Ef;
using System;
using Moedi.Cqrs;
using Moedi.Cqrs.Processor;

namespace Gis.Implementation
{
    public class Startup : StatelessServiceStartup
    {
        protected override Type[] ServiceContractTypes => new []
        {
            typeof(IGisService)
        };

        protected override void RegisterStatelessService(IServiceCollection c)
        {
            c.RegisterStatelessServices()
                .AddHttpService<GisService, IGisService>()
                .AddBackgroundService<GisService>();

            c.AddTransient<IProcessorFactory, ProcessorFactory>();

            c.AddDbContextFactory<GisDbContext, DbContextFactory<GisDbContext>>();
            c.AddSingleton<IUowFactory, UowFactory<GisDbContext>>();

            c.AddAutoMapper(MapperConfiguration.Register);
        }
    }
}
