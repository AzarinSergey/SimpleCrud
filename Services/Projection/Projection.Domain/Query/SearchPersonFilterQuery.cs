using System.Linq;
using Cmn.Models;
using Moedi.Cqrs.Handler;
using Projection.Domain.Model;
using Projection.Domain.SearchFilter.Implementations;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Projection.Domain.Query
{
    public class SearchPersonFilterQuery : QueryHandler<PagedResult<SearchPersonResult>>
    {
        private readonly SearchPersonFilter _filter;

        public SearchPersonFilterQuery(SearchPersonFilter filter)
        {
            _filter = filter;
        }

        public override async Task<PagedResult<SearchPersonResult>> Query(CancellationToken token)
        {
            _filter.NormalizePagination();

            var query = _filter.GetQuery(RepositoryFactory);

            var totalCount = await query.CountAsync(token);
            var result = await query
                .Select(x => new SearchPersonResult
                {
                    Id = x.Id,
                    LastName = x.LastName,
                    FirstName = x.FirstName,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,
                    StreetAddress = x.StreetAddress,
                    City = x.City,
                    ZipCode = x.ZipCode
                })
                .ToListAsync(token);

            return new PagedResult<SearchPersonResult>(_filter.PageSize, _filter.PageNumber, totalCount, result);
        }
    }
}
