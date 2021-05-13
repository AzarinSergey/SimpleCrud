using Projection.Contract.Models;
using System.Threading;
using System.Threading.Tasks;
using Cmn.Models;
using Moedi.Cqrs.Messages;

namespace Projection.Contract
{
    public interface IPersonProjection
    {
        Task<PagedResult<SearchPersonResultPrj>> SearchPerson(SearchPersonFilterPrj filter, CrossContext ctx, CancellationToken token);
    }
}
