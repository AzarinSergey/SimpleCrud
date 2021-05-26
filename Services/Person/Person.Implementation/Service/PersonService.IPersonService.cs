using Moedi.Cqrs.Extensions;
using Moedi.Cqrs.Messages;
using Person.Contract;
using Person.Domain.Command;
using Person.Domain.Event;
using Person.Domain.Handler;
using Person.Domain.Validator;
using System.Threading.Tasks;

namespace Person.Implementation.Service
{
    public partial class PersonService : IPersonService
    {
        public async Task<int> CreatePerson(CreatePersonCommandModel command, CrossContext ctx)
        {
            var events = await _processor
                .Command<CreatePersonDomainCommand>(command, ctx)
                .UseDomain(_mapper.Map<CreatePersonDomainCommand>(command))
                .UseValidator(new CreatePersonCommandValidator())
                .RunWithEvents(() => new CreatePersonCommandHandler());

            return events.MapSingleEvent<PersonCreatedDomainEvent, int>(x => x.PersonId);
        }

        public async Task<bool> UpdatePerson(int personId, CreatePersonCommandModel command, CrossContext ctx)
        {
            var domainCommand = _mapper.Map<UpdatePersonDomainCommand>(command);
            domainCommand.Id = personId;

            var events = await _processor
                .Command<UpdatePersonDomainCommand>(command, ctx)
                .UseTransaction()
                .UseDomain(domainCommand)
                .UseValidator(new UpdatePersonCommandValidator())
                .RunWithEvents(() => new UpdatePersonCommandHandler());

            return events.MapOptionalSingleEvent<PersonUpdatedDomainEvent, bool>(x => x != null);
        }

        public async Task<PersonRemovedIntegrationEvent> RemovePerson(int personId, CrossContext ctx)
        {
            var events = await _processor
                .Command<RemovePersonDomainCommand>(personId, ctx)
                .UseTransaction()
                .UseDomain(new RemovePersonDomainCommand { Id = personId })
                .RunWithEvents(() => new RemovePersonCommandHandler());

            return events.MapOptionalSingleEvent<PersonRemovedDomainEvent, PersonRemovedIntegrationEvent>(
                x => _mapper.Map<PersonRemovedIntegrationEvent>(x));
        }
    }
}
