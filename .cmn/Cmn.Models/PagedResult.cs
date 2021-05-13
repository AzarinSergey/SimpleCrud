using System.Collections.Generic;

namespace Cmn.Models
{
    public class PagedResult<T>
    {
        public List<T> Result { get; }

        public int PageSize { get; }

        public int PageNumber { get; }

        public int PageTotalCount { get; }

        public int TotalCount { get; }

        public PagedResult(int pageSize, int pageNumber, int totalCount, List<T> results)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            PageNumber = pageNumber;
            PageTotalCount = TotalCount / PageSize + (TotalCount % PageSize > 0 ? 1 : 0);
            Result = results;
        }
    }
}