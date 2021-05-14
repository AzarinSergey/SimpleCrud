using System.Threading;
using System.Threading.Tasks;
using Moedi.Cqrs.Extensions;
using Moedi.Cqrs.Messages;
using Person.Contract;
using Person.Domain.Command;
using Person.Domain.Event;
using Person.Domain.Handler;
using Person.Domain.Validator;

namespace Person.Implementation.Service
{
    public partial class PersonService : IPersonService
    {
        public async Task<int> CreatePerson(CreatePersonCommandModel command, CrossContext ctx, CancellationToken token)
        {
            var events = await _processor
                .Command<CreatePersonDomainCommand>(command, ctx)
                .UseDomain(_mapper.Map<CreatePersonDomainCommand>(command))
                .UseValidator(new CreatePersonCommandValidator())
                .RunWithEvents(() => new CreatePersonCommandHandler(), token);

            return events.MapSingleEvent<PersonCreatedDomainEvent, int>(x => x.PersonId);
        }

        public async Task<bool> UpdatePerson(int personId, CreatePersonCommandModel command, CrossContext ctx, CancellationToken token)
        {
            var domainCommand = _mapper.Map<UpdatePersonDomainCommand>(command);
            domainCommand.Id = personId;

            var events = await _processor
                .Command<UpdatePersonDomainCommand>(command, ctx)
                .UseTransaction()
                .UseDomain(domainCommand)
                .UseValidator(new UpdatePersonCommandValidator())
                .RunWithEvents(() => new UpdatePersonCommandHandler(), token);

            return events.MapOptionalSingleEvent<PersonUpdatedDomainEvent, bool>(x => x != null);
        }

        public async Task<PersonRemovedIntegrationEvent> RemovePerson(int personId, CrossContext ctx, CancellationToken token)
        {
            var events = await _processor
                .Command<RemovePersonDomainCommand>(personId, ctx)
                .UseTransaction()
                .UseDomain(new RemovePersonDomainCommand { Id = personId })
                .RunWithEvents(() => new RemovePersonCommandHandler(), token);

            return events.MapOptionalSingleEvent<PersonRemovedDomainEvent, PersonRemovedIntegrationEvent>(
                x => _mapper.Map<PersonRemovedIntegrationEvent>(x));
        }
    }
}
