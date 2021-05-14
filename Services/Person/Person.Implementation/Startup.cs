using Core.Service.Host;
using Core.Service.Host.ServiceCollectionExtensions;
using Microsoft.Extensions.DependencyInjection;
using Moedi.Cqrs;
using Moedi.Cqrs.Processor;
using Moedi.Data.Core.Access;
using Moedi.Data.Ef;
using Person.Contract;
using Person.Implementation.Service;
using Person.Model;
using System;

namespace Person.Implementation
{
    public class Startup : StatelessServiceStartup
    {
        protected override Type[] ServiceContractTypes => new []
        {
            typeof(IPersonService)
        };

        protected override void RegisterStatelessService(IServiceCollection c)
        {
            c.RegisterStatelessServices()
                .AddHttpService<PersonService, IPersonService>()
                .AddBackgroundService<PersonService>();

            c.AddTransient<IProcessorFactory, ProcessorFactory>();

            c.AddDbContextFactory<PersonDbContext, DbContextFactory<PersonDbContext>>();
            c.AddSingleton<IUowFactory, UowFactory<PersonDbContext>>();

            c.AddAutoMapper(MapperConfiguration.Register);
        }
    }
}
