using Core.Test;
using Gis.Domain.External.Provider;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;

namespace Gis.Test.Unit
{
    public class GisDataProviderGetAirportPositionTests
    {
        private ILogger<GisDataProvider> _logger;

        [SetUp]
        public void Setup()
        {
            _logger = Substitute.For<ILogger<GisDataProvider>>();
        }

        [Test]
        public void ShouldThrowUnhandledException()
        {
            var httpClient = new HttpClient();
            var instance = new GisDataProvider(httpClient, _logger);

            Assert.CatchAsync<NullReferenceException>(() => instance.GetAirportPosition(null, CancellationToken.None));
        }

        [Test]
        public void ShouldReturnValueWithNotConfiguredHttpClientAndLogError()
        {
            var httpClient = new HttpClient();
            var instance = new GisDataProvider(httpClient, _logger);

            var (request, response, responseModel) = instance.GetAirportPosition("AAA", CancellationToken.None)
                .GetAwaiter()
                .GetResult();

            Assert.AreEqual("/airports/AAA", request);
            Assert.AreEqual("not retrieved", response);
            Assert.AreEqual(responseModel, null);

            _logger.ReceivedWithAnyArgs().LogError(new Exception(), "");
        }

        [Test]
        public void ShouldReturnValueForNonSuccessStatusCodeResponseWithoutAnyLog()
        {
            var httpClient = new HttpClient(new MockHttpMessageHandler("any content", HttpStatusCode.BadRequest))
            {
                BaseAddress = new Uri("http://localhost")
            };
            var instance = new GisDataProvider(httpClient, _logger);

            var (request, response, responseModel) = instance.GetAirportPosition("AAA", CancellationToken.None)
                .GetAwaiter()
                .GetResult();

            Assert.AreEqual("/airports/AAA", request);
            Assert.AreEqual("not retrieved", response);
            Assert.AreEqual(responseModel, null);

            _logger.DidNotReceiveWithAnyArgs().LogError(new Exception(), "");
        }

        [Test]
        public void ShouldReturnValueWithContentStringForNotValidResponseModelAndLogError()
        {
            var content = "not valid json";
            var httpClient = new HttpClient(new MockHttpMessageHandler(content, HttpStatusCode.OK))
            {
                BaseAddress = new Uri("http://localhost")
            };
            var instance = new GisDataProvider(httpClient, _logger);

            var (request, response, responseModel) = instance.GetAirportPosition("AAA", CancellationToken.None)
                .GetAwaiter()
                .GetResult();

            Assert.AreEqual("/airports/AAA", request);
            Assert.AreEqual(content, response);
            Assert.AreEqual(responseModel, null);

            _logger.ReceivedWithAnyArgs().LogError(new Exception(), "");
        }
    }
}