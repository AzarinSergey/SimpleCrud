using System;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core.Tool;
using Gis.Domain.External.Provider.Model;
using Microsoft.Extensions.Logging;

namespace Gis.Domain.External.Provider
{
    public class GisDataProvider : IGisDataProvider
    {
        private readonly HttpClient _client;
        private readonly ILogger<GisDataProvider> _logger;

        public GisDataProvider(HttpClient client, ILogger<GisDataProvider> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<(string, string, AirportPosition)> GetAirportPosition(string iataKey, CancellationToken token)
        {
            var path = $"/airports/{iataKey.ToUpper(CultureInfo.InvariantCulture)}";
            string content = "not retrieved";

            try
            {
                var response = await _client.GetAsync(path, token);

                if (response.IsSuccessStatusCode)
                {
                    content = await response.Content.ReadAsStringAsync();
                    return (path, content, Tools.Json.Serializer.Deserialize<AirportPosition>(content));
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Airport '{iataKey}' position error.");
            }

            return (path, content, null);
        }
    }
}