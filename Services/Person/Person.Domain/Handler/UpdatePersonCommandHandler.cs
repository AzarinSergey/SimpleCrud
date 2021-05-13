using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moedi.Cqrs.Handler;
using Person.Domain.Command;
using Person.Domain.Event;

namespace Person.Domain.Handler
{
    public class RemovePersonCommandHandler : CommandHandler<RemovePersonDomainCommand>
    {
        public override async Task Execute(RemovePersonDomainCommand command, CancellationToken token)
        {
            var repo = GetRepository<Model.Entity.Person>();

            var entity = await GetRepository<Model.Entity.Person>()
                .Query()
                .Where(x => x.Id == command.Id)
                .SingleOrDefaultAsync(token);

            if (entity != null)
            {
                await GetRepository<Model.Entity.Person>().DeleteAsync(x => x.Id == command.Id);
                AddEvent(new PersonRemovedDomainEvent
                {
                    Id = entity.Id,
                    PhoneNumber = entity.PhoneNumber,
                    LastName = entity.LastName,
                    StreetAddress = entity.StreetAddress,
                    City = entity.City,
                    ZipCode = entity.ZipCode,
                    Email = entity.Email,
                    FirstName = entity.FirstName
                });
            }
        }
    }

    public class UpdatePersonCommandHandler : CommandHandler<UpdatePersonDomainCommand>
    {
        public override async Task Execute(UpdatePersonDomainCommand command, CancellationToken token)
        {
            var result = await GetRepository<Model.Entity.Person>()
                .Update(x => x.Id == command.Id, person 
                    => new Model.Entity.Person
                    {
                        PhoneNumber = command.PhoneNumber,
                        FirstName = command.FirstName,
                        LastName = command.LastName,
                        StreetAddress = command.StreetAddress,
                        City = command.City,
                        ZipCode = command.ZipCode,
                        Email = command.Email
                    });

            if (result > 0)
            {
                AddEvent(new PersonUpdatedDomainEvent());
            }
        }
    }
}