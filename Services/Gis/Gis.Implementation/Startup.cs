using Core.Service.Host;
using Core.Service.Host.Client.ServiceCollectionExtensions;
using Core.Service.Host.ServiceCollectionExtensions;
using Gis.Contract;
using Gis.Domain.External.Provider;
using Gis.Implementation.Service;
using Gis.Model;
using Microsoft.Extensions.DependencyInjection;
using Moedi.Data.Core.Access;
using Moedi.Data.Ef;
using System;

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
                .AddBackgroundService<GisService>()
                .UseServiceProcessorFactory();

            c.RegisterExternalServiceProxy<IGisDataProvider, GisDataProvider>((provider, client) =>
            {
                client.BaseAddress = new Uri("https://places-dev.cteleport.com");
            });

            c.AddDbContextFactory<GisDbContext, DbContextFactory<GisDbContext>>();
            c.AddSingleton<IUowFactory, UowFactory<GisDbContext>>();

            c.AddAutoMapper(MapperConfiguration.Register);
        }
    }
}
