using System.Threading;
using System.Threading.Tasks;
using Core.Service.Interfaces;
using Gis.Domain.External.Provider.Model;

namespace Gis.Domain.External.Provider
{
    public interface IGisDataProvider : IExternalHttpService
    {
        Task<(string, string, AirportPosition)> GetAirportPosition(string iataKey, CancellationToken token);
    }
}