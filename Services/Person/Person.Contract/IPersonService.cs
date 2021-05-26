using Core.Service.Interfaces;
using Moedi.Cqrs.Messages;
using System.Threading.Tasks;

namespace Person.Contract
{
    public interface IPersonService : IInternalHttpService
    {
        Task<int> CreatePerson(CreatePersonCommandModel command, CrossContext ctx);
        Task<bool> UpdatePerson(int personId, CreatePersonCommandModel command, CrossContext ctx);
        Task<PersonRemovedIntegrationEvent> RemovePerson(int personId, CrossContext ctx);
    }
}