using Gis.Domain.Command;
using Gis.Domain.External.Provider;
using Gis.Domain.External.Provider.Operation;
using Gis.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Moedi.Cqrs.Handler;
using Moedi.Data.Core.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gis.Domain.Handler
{
    public class CreateAirportsIfNotExistCommandHandler : CommandHandler<CreateAirportsIfNotExistDomainCommand>
    {
        (GisDataRequestLog[], Airport[]) _operationResult;

        public override async Task BeforeExecute(Func<Func<IQueryRepositoryFactory, Task>, Task> query, CreateAirportsIfNotExistDomainCommand command)
        {
            var existingAirportCodes = new List<string>(command.IataCodes.Length);
            await query(async factory =>
            {
                existingAirportCodes = await factory.GetRepository<Airport>().Query()
                    .Where(x => command.IataCodes.Contains(x.Iata))
                    .Select(x => x.Iata)
                    .ToListAsync(Token);
            });

            var iataCodesToCreate = command.IataCodes.Where(x => !existingAirportCodes.Contains(x)).ToArray();

            if (iataCodesToCreate.Length > 0)
            {
                var operation = new GetAirportPositionOperation(
                    UseExternalHttpService<IGisDataProvider>(),
                    UseLogger,
                    iataCodesToCreate);

                _operationResult = await operation.Run(Token);
            }
        }

        public override async Task Execute(ICommandRepositoryFactory factory, CreateAirportsIfNotExistDomainCommand command)
        {
            if (_operationResult.Item1 != null)
            {
                await factory.GetRepository<GisDataRequestLog>()
                    .CreateOrUpdateAsync(_operationResult.Item1);
            }

            if (_operationResult.Item2 != null)
            {
                await factory.GetRepository<Airport>()
                    .CreateOrUpdateAsync(_operationResult.Item2);
            }
        }
    }
}
