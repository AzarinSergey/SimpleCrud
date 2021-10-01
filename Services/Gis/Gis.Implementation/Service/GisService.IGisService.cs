using Gis.Contract;
using Moedi.Cqrs.Messages;
using System.Threading.Tasks;

namespace Gis.Implementation.Service
{
    public partial class GisService : IGisService
    {
        public Task<decimal> GetDistance(GetDistanceModel model, CrossContext ctx)
        {
            return Task.FromResult(1000m);
        }
    }
}
