using Microsoft.EntityFrameworkCore;
using Moedi.Cqrs.Handler;
using Projection.Domain.Model;
using Projection.Domain.SearchFilter.Implementations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Projection.Domain.Query
{
    public class SearchPersonFilterQuery : QueryHandler<SearchPersonResult>
    {
        private readonly SearchPersonFilter _filter;

        public SearchPersonFilterQuery(SearchPersonFilter filter)
        {
            _filter = filter;
        }

        public override async Task<SearchPersonResult> Query(CancellationToken token)
        {
            _filter.NormalizePagination();

            var query = _filter.GetQuery(RepositoryFactory);

            var result = await query.Query
                .Select(x => new SearchPersonResultItem
                {
                    Id = x.Id,
                    LastName = x.LastName,
                    FirstName = x.FirstName,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,
                    StreetAddress = x.StreetAddress,
                    City = x.City,
                    ZipCode = x.ZipCode,
                })
                .ToListAsync(token);

            return new SearchPersonResult
            {
                Items = result,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                TotalCount = query.TotalCount
            };
        }
    }
}
