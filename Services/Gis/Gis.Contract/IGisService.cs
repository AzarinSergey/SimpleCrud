using Core.Service.Interfaces;
using Moedi.Cqrs.Messages;
using System.Threading.Tasks;

namespace Gis.Contract
{
    public interface IGisService : IInternalHttpService
    {
        Task<double> GetDistance(GetDistanceModel model, CrossContext ctx);
    }
}