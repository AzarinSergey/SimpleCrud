using Gis.Contract;
using Moedi.Cqrs.Messages;
using System.Threading.Tasks;
using Gis.Domain.Command;
using Gis.Domain.Handler;
using Gis.Domain.Query;
using Gis.Domain.Validator;

namespace Gis.Implementation.Service
{
    public partial class GisService : IGisService
    {
        public async Task<double> GetDistance(GetDistanceModel model, CrossContext ctx)
        {
            await _processor.Command<CreateAirportsIfNotExistDomainCommand>(model, ctx)
                .UseDomain(new CreateAirportsIfNotExistDomainCommand
                {
                    IataCodes = new []{ model.AirportCodeA, model.AirportCodeB }
                })
                .UseValidator<CreateAirportsIfNotExistValidator>()
                .UseTransaction()
                .Run(() => new CreateAirportsIfNotExistCommandHandler());

            return await _processor.Query(ctx, new GetDistanceQuery(model.AirportCodeA, model.AirportCodeB));
        }
    }
}
