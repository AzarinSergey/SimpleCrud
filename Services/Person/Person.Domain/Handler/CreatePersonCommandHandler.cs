using System.Threading;
using System.Threading.Tasks;
using Moedi.Cqrs.Handler;
using Person.Domain.Command;
using Person.Domain.Event;

namespace Person.Domain.Handler
{
    public class CreatePersonCommandHandler : CommandHandler<CreatePersonDomainCommand>
    {
        public override async Task Execute(CreatePersonDomainCommand command, CancellationToken token)
        {
            var result = await GetRepository<Model.Entity.Person>()
                .CreateOrUpdateAsync(new Model.Entity.Person
                {
                    FirstName = command.FirstName,
                    ZipCode = command.ZipCode,
                    PhoneNumber = command.PhoneNumber,
                    City = command.City,
                    Email = command.Email,
                    LastName = command.LastName,
                    StreetAddress = command.StreetAddress
                });

            AddEvent(new PersonCreatedDomainEvent
            {
                PersonId = result
            });
        }
    }
}