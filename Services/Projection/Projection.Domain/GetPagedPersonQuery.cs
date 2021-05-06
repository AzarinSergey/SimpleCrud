using Projection.Contract;
using Projection.Contract.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Projection.Domain
{
    public class GetPagedPersonQuery
    {
        private readonly int _pageNumber;
        private readonly int _pageSize;

        public GetPagedPersonQuery(int pageNumber, int pageSize)
        {
            _pageNumber = pageNumber;
            _pageSize = pageSize;
        }

        public Task<PagedResult<PersonPrj>> Query(CancellationToken token)
        {
            return Task.FromResult(new PagedResult<PersonPrj>());
        }
    }
}
