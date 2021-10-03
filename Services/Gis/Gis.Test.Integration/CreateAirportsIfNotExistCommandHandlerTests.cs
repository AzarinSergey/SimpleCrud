using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gis.Domain.Command;
using Gis.Domain.External.Provider;
using Gis.Domain.External.Provider.Model;
using Gis.Domain.Handler;
using Gis.Model;
using Gis.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moedi.Cqrs;
using Moedi.Cqrs.Messages;
using Moedi.Cqrs.Processor;
using Moedi.Data.Ef;
using NSubstitute;
using NUnit.Framework;
using Z.EntityFramework.Plus;

namespace Gis.Test.Integration
{
    /// <summary>
    /// Не верно так тестить хендлер. Но времени переделать уже нет.
    /// Все-таки интеграционный тест должен происходить по контракту сервиса без лишних референсов,
    /// только модель и контракты.
    /// </summary>
    public class CreateAirportsIfNotExistCommandHandlerTests
    {
        private CreateAirportsIfNotExistCommandHandler _handler;
        private DefaultCommandProcessor<CreateAirportsIfNotExistDomainCommand> _commandProcessor;
        private IGisDataProvider _dataProvider;

        [SetUp]
        public void Setup()
        {
            _handler = new CreateAirportsIfNotExistCommandHandler();

            var context = new GisDbContext();
            context.Database.EnsureCreated();
            context.SaveChanges();
            context.Dispose();

            var dbContextFactory = Substitute.For<IDbContextFactory<GisDbContext>>();
            dbContextFactory.CreateDbContext()
                .Returns(x => new GisDbContext());

            var uowFactory = new UowFactory<GisDbContext>(dbContextFactory);
            var loggerFactory = Substitute.For<ILoggerFactory>();

            _dataProvider = Substitute.For<IGisDataProvider>();
            var externalServiceProvider = Substitute.For<IExternalServiceProvider>();
            externalServiceProvider.GetExternalHttpService<IGisDataProvider>()
                .Returns(_dataProvider);

            _commandProcessor = new DefaultCommandProcessor<CreateAirportsIfNotExistDomainCommand>(() => _handler,
                externalServiceProvider, loggerFactory, uowFactory);
        }

        [Test]
        public void NewAirportsAndRequestLogsShouldBeAdded()
        {
            var context = new GisDbContext();
            context.Airport.Delete();
            context.GisDataRequestLog.Delete();
            context.SaveChanges();

            var dataProviderResponses = new List<(string, string, AirportPosition)>()
            {
                ("request", "raw response", new AirportPosition
                {
                    Location = new Location { Lat = 43.43564, Lon = 12.34343 },
                    Iata = "AAA",
                    Name = "Aaa name"
                }),
                ("request", "raw response", new AirportPosition
                {
                    Location = new Location { Lat = 12.55555, Lon = 30.555555 },
                    Iata = "ABA",
                    Name = "Aba name"
                })
            };
            
            for (int i = 0; i < dataProviderResponses.Count; i++)
            {
                _dataProvider
                    .GetAirportPosition(dataProviderResponses[i].Item3.Iata, CancellationToken.None)
                    .Returns(Task.FromResult(dataProviderResponses[i]));

            }

            var iataCodes = dataProviderResponses.Select(x => x.Item3.Iata).ToArray();

            //act
            _commandProcessor.Process(new CreateAirportsIfNotExistDomainCommand { IataCodes = iataCodes },
                    new CrossContext("", Guid.Empty, CancellationToken.None))
                .GetAwaiter().GetResult();

            //assert
            var airports = context.Airport.ToList();
            var requestLogs = context.GisDataRequestLog.ToList();

            Assert.AreEqual(2, airports.Count);
            Assert.AreEqual(2, requestLogs.Count);
        }

        [Test]
        public void ShouldNotAnyUpdatesIfExist()
        {
            var context = new GisDbContext();
            context.Airport.Delete();
            context.GisDataRequestLog.Delete();
            context.SaveChanges();

            var airportsToAdd = new[]
            {
                new Airport
                {
                    Iata = "AAA"
                },
                new Airport
                {
                    Iata = "ABA"
                }
            };

            context.Airport.AddRange(airportsToAdd);
            context.SaveChanges();

            //act
            _commandProcessor.Process(new CreateAirportsIfNotExistDomainCommand { IataCodes = airportsToAdd.Select(x => x.Iata).ToArray() },
                    new CrossContext("", Guid.Empty, CancellationToken.None))
                .GetAwaiter().GetResult();

            //assert
            _dataProvider
                .DidNotReceiveWithAnyArgs()
                .GetAirportPosition(Arg.Any<string>(), CancellationToken.None);

            var airports = context.Airport.ToList();
            var requestLogs = context.GisDataRequestLog.ToList();

            Assert.AreEqual(2, airports.Count);
            Assert.AreEqual(0, requestLogs.Count);
        }
    }
}