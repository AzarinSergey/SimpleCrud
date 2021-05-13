using Projection.Contract.Models;
using System.Threading;
using System.Threading.Tasks;
using Cmn.Models;

namespace Projection.Contract
{
    public interface IPersonProjection
    {
        Task<PagedResult<SearchPersonResultPrj>> SearchPerson(SearchPersonFilterPrj filter, CancellationToken token);
    }
}
