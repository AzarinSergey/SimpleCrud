using System.Collections.Generic;

namespace Projection.Contract
{
    public class PagedResult<T>
    {
        public List<T> Result { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public int PageTotalCount { get; set; }

        public int TotalCount { get; set; }
    }
}