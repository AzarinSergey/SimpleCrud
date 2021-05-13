using System.Linq;

namespace Projection.Domain.SearchFilter.Implementations
{
    public class FilterQuery<T>
    {
        public IQueryable<T> Query { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }
    }
}