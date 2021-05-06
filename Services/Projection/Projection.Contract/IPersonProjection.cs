using Projection.Contract.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Projection.Contract
{
    public interface IPersonProjection
    {
        Task<PagedResult<PersonPrj>> GetPagedPerson(CancellationToken token);
    }
}
