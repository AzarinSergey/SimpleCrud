using Gis.Model.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gis.Domain.External.Provider.Operation
{
    public class GetAirportPositionOperation
    {
        private readonly IGisDataProvider _provider;
        private readonly ILogger _logger;
        private readonly string[] _iataKeys;

        public GetAirportPositionOperation(
            IGisDataProvider provider, 
            ILogger logger,
            string[] iataKeys)
        {
            _provider = provider;
            _logger = logger;

            var distincted = iataKeys.Distinct().ToArray();
            _iataKeys = distincted.Length > 10 
                ? throw new NotImplementedException("Can't process more then 10 iata codes") 
                : distincted;
        }

        public async Task<(GisDataRequestLog[], Airport[])> Run(CancellationToken token)
        {
            var tasks = _iataKeys
                .Select(x => _provider.GetAirportPosition(x, token))
                .ToList();

            var (exception, result) = await Task.WhenAll(tasks)
                .ContinueWith(t => {
                    return (t.Exception, tasks
                        .Where(x => x.Status == TaskStatus.RanToCompletion)
                        .Select(x => x.Result)
                        .ToList());
                }, token);

            if (exception != null)
            {
                _logger.LogError(exception, "IGisDataProvider implementation threw unhandled exception.");
                foreach (var inner in exception.InnerExceptions)
                {
                    _logger.LogError(inner, "IGisDataProvider implementation threw unhandled exception.");
                }
            }

            return (
                result.Select(x => new GisDataRequestLog
                {
                    Request = x.Item1,
                    Response = x.Item2,
                    Success = x.Item3 != null
                }).ToArray(),
                result
                    .Where(x => x.Item3 != null && x.Item3.Location != null)
                    .Select(x => new Airport
                    {
                        Iata = x.Item3.Iata,
                        Lat = x.Item3.Location.Lat,
                        Lon = x.Item3.Location.Lon,
                        Name = x.Item3.Name
                    }).ToArray()
                );
        }
    }
}