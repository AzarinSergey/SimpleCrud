using Moedi.Data.Core.Access;
using System.Linq;

namespace Projection.Domain.SearchFilter.Criteria
{
    public interface ISearchCriteria
    {
        IQueryable<int> GetQuery(IQueryRepositoryFactory factory);
    }
}