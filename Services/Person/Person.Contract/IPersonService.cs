using System.Threading;
using System.Threading.Tasks;
using Moedi.Cqrs.Messages;

namespace Person.Contract
{
    public interface IPersonService
    {
        Task<int> CreatePerson(CreatePersonCommandModel command, CrossContext ctx, CancellationToken token);
        Task<bool> UpdatePerson(int personId, CreatePersonCommandModel command, CrossContext ctx, CancellationToken token);
        Task<PersonRemovedIntegrationEvent> RemovePerson(int personId, CrossContext ctx, CancellationToken token);
    }
}