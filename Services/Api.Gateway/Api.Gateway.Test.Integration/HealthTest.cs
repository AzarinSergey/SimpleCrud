using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Net.Http;

namespace Api.Gateway.Test.Integration
{
    public class HealthTest
    {
        private string _host;

        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            _host = config.GetSection("TestConfig")["ServiceHost"];
        }

        [Test]
        public void HealthCheck()
        {
            using (var client = new HttpClient())
            {
                var valuesEndpoint = $"{_host}/tool/health";

                var response = client.GetAsync(valuesEndpoint).GetAwaiter().GetResult();

                Assert.IsTrue(response.IsSuccessStatusCode);
            }
        }
    }
}