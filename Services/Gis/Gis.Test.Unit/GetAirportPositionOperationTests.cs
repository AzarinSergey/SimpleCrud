using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gis.Domain.External.Provider;
using Gis.Domain.External.Provider.Model;
using Gis.Domain.External.Provider.Operation;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace Gis.Test.Unit
{
    public class GetAirportPositionOperationTests
    {
        private ILogger _logger;
        private IGisDataProvider _dataProvider;

        [SetUp]
        public void Setup()
        {
            _logger = Substitute.For<ILogger>();
            _dataProvider = Substitute.For<IGisDataProvider>();
        }

        [Test]
        public void ShouldThrowNotImplementedExceptionIfCountOfCodesMoreThen10()
        {
            var codes = new[] {"AAA", "AAB", "AAC", "AAE", "AAf", "AAg", "AAh", "AA1", "AAt", "AA!", "AAa"};

            // ReSharper disable once ObjectCreationAsStatement
            Assert.Catch<NotImplementedException>(() => new GetAirportPositionOperation(_dataProvider, _logger, codes));
        }

        [TestCase(10, "AAA", "AAB", "AAC", "AAE", "AAf", "AAg", "AAh", "AA1", "AAt", "AA!",
            "AAA", "AAB", "AAC", "AAE", "AAf", "AAg", "AAh", "AA1", "AAt", "AA!")]
        [TestCase(2, "AAA", "AAB", "AAA", "AAB")]
        [TestCase(1, "AAA", "AAA", "AAA", "AAA")]
        public void ShouldDistinctEqualCodesAndReturnsRequestLogItems(int outcomesCounter, params string[] arr)
        {
            var instance = new GetAirportPositionOperation(_dataProvider, _logger, arr);
            var (requestLog, airports) = instance.Run(CancellationToken.None)
                .GetAwaiter()
                .GetResult();

            Assert.AreEqual(requestLog.Length, outcomesCounter);
        }

        [TestCase(2, "AAC", "AAE", "AAA", "AAB")]
        [TestCase(0, "AAC", "AAE")]
        public void ShouldNotThrowExceptionAndLogErrorsAndReturnsRequestLogItemsWhenGetAirportPositionTaskThrowUnhandled(
            int outcomesCounter, params string[] arr)
        {
            //exception will occur first time "on value 'AAC' passed"
            _dataProvider
                .GetAirportPosition("AAC", CancellationToken.None)
                .Returns(Task.FromException<(string, string, AirportPosition)>(
                    new Exception("First unhandled exception.")));

            //exception will occur second time "on value 'AAE' passed"
            _dataProvider
                .GetAirportPosition("AAE", CancellationToken.None)
                .Returns(Task.FromException<(string, string, AirportPosition)>(
                    new Exception("Second unhandled exception.")));

            var instance = new GetAirportPositionOperation(_dataProvider, _logger, arr);
            var (requestLog, _) = instance.Run(CancellationToken.None)
                .GetAwaiter()
                .GetResult();

            Assert.AreEqual(requestLog.Length, outcomesCounter);

            //this is one call for each unhandled exception (2) + one call for common aggregate exception (1)
            _logger.ReceivedWithAnyArgs(3).LogError(new Exception(), "");
        }

        [TestCase("AAA", 4, 4, "AAT", "AAE", "AAD", "AAB")]
        public void ShouldReturnsRequestLogAndAirportItemsAndLogException(string exceptionCode, int requestLogCounter,
            int airportCounter, params string[] arr)
        {
            //configure unhandled exception
            _dataProvider
                .GetAirportPosition(exceptionCode, CancellationToken.None)
                .Returns(Task.FromException<(string, string, AirportPosition)>(
                    new Exception("Second unhandled exception.")));

            //configure results of GetAirportPosition tasks
            for (var i = 0; i < arr.Length; i++)
            {
                _dataProvider
                    .GetAirportPosition(arr[i], CancellationToken.None)
                    .Returns(Task.FromResult(("raw request", "raw response", new AirportPosition
                    {
                        Iata = arr[i],
                        Location = new Location()
                    })));
            }

            var instance = new GetAirportPositionOperation(_dataProvider, _logger, arr.Concat(new []{ exceptionCode }).ToArray());
            var (requestLog, airports) = instance.Run(CancellationToken.None)
                .GetAwaiter()
                .GetResult();

            Assert.AreEqual(requestLogCounter, requestLog.Length);
            Assert.AreEqual(airportCounter, airports.Length);

            _logger.ReceivedWithAnyArgs(2).LogError(new Exception(), "");
        }

        [Test]
        public void ShouldNotReturnsAirportAndReturnRightRequestLogValueWhenProviderDoesNotReturnsAirportPosition()
        {
            var request = "raw request";
            var response = "raw response";

            _dataProvider
                .GetAirportPosition(Arg.Any<string>(), CancellationToken.None)
                .Returns(Task.FromResult<(string, string, AirportPosition)>((request, response, null)));

            var instance = new GetAirportPositionOperation(_dataProvider, _logger, new[] {"AAA"});
            var (requestLog, airports) = instance.Run(CancellationToken.None)
                .GetAwaiter()
                .GetResult();

            Assert.AreEqual(0, airports.Length);
            Assert.AreEqual(request, requestLog[0].Request);
            Assert.AreEqual(response, requestLog[0].Response);
            Assert.AreEqual(false, requestLog[0].Success);
        }

        [Test]
        public void ShouldReturnsRightAirportValueAndReturnRightRequestLogValueWhenProviderReturnsAirportPosition()
        {
            var request = "raw request";
            var response = "raw response";

            var airportPosition = new AirportPosition
            {
                Iata = "IATA",
                Name = "Test",
                Location = new Location { Lat = 60.3456345, Lon = 30.445 }
            };

            _dataProvider
                .GetAirportPosition(Arg.Any<string>(), CancellationToken.None)
                .Returns(Task.FromResult<(string, string, AirportPosition)>((request, response, airportPosition)));

            var instance = new GetAirportPositionOperation(_dataProvider, _logger, new[] { "AAA" });
            var (requestLog, airports) = instance.Run(CancellationToken.None)
                .GetAwaiter()
                .GetResult();

            Assert.AreEqual(1, airports.Length);
            Assert.AreEqual(airportPosition.Iata, airports[0].Iata);
            Assert.AreEqual(airportPosition.Location.Lat, airports[0].Lat);
            Assert.AreEqual(airportPosition.Location.Lon, airports[0].Lon);
            Assert.AreEqual(airportPosition.Name, airports[0].Name);

            Assert.AreEqual(1, requestLog.Length);
            Assert.AreEqual(request, requestLog[0].Request);
            Assert.AreEqual(response, requestLog[0].Response);
            Assert.AreEqual(true, requestLog[0].Success);
        }
    }
}