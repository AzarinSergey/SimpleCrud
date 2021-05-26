using Cmn.Models;
using Moedi.Cqrs.Messages;
using Projection.Contract.Models;
using System.Threading.Tasks;

namespace Projection.Contract
{
    public interface IPersonProjection
    {
        Task<PagedResult<SearchPersonResultPrj>> SearchPerson(SearchPersonFilterPrj filter, CrossContext ctx);
    }
}
