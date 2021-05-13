using System.Threading;
using System.Threading.Tasks;

namespace Person.Contract
{
    public interface IPersonService
    {
        Task<int> CreatePerson(CreatePersonCommandModel command, CancellationToken token);
        Task<bool> UpdatePerson(int personId, CreatePersonCommandModel command, CancellationToken token);
    }
}