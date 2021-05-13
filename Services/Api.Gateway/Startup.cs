using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moedi.Cqrs;
using Moedi.Cqrs.Processor;
using Moedi.Data.Core.Access;
using Moedi.Data.Ef;
using Person.Contract;
using Person.Implementation.Monolithic;
using Person.Model;
using Projection.Contract;
using Projection.Domain;
using Projection.Implementation.Monolithic;
using AutoMapper;

namespace Api.Gateway
{
    public class Startup
    {
        public Startup()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile($"appsettings.json", reloadOnChange: true, optional: false)
                .AddJsonFile($"appsettings.{environmentName}.json", reloadOnChange: true, optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IProcessorFactory, ProcessorFactory>();

            services.AddDbContextFactory<PersonDbContext, DbContextFactory<PersonDbContext>>();
            services.AddSingleton<IUowFactory, UowFactory<PersonDbContext>>();
            services.AddScoped<IPersonService, PersonService>();

            services.AddDbContextFactory<ProjectionDbContext, DbContextFactory<ProjectionDbContext>>();
            services.AddSingleton<IUowFactory, UowFactory<ProjectionDbContext>>();
            services.AddScoped<IPersonProjection, PersonProjection>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddAutoMapper(x =>
            {
                Person.Implementation.Monolithic.MapperConfiguration.Register(x);
                Projection.Implementation.Monolithic.MapperConfiguration.Register(x);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Simple CRUD Api");
                c.RoutePrefix = "swagger";
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
