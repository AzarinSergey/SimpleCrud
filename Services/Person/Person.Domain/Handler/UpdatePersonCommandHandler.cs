using System.Threading;
using System.Threading.Tasks;
using Moedi.Cqrs.Handler;
using Person.Domain.Command;

namespace Person.Domain.Handler
{
    public class UpdatePersonCommandHandler : CommandHandler<UpdatePersonDomainCommand>
    {
        public override Task Execute(UpdatePersonDomainCommand command, CancellationToken token)
        {
            return GetRepository<Model.Entity.Person>()
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
        }
    }
}