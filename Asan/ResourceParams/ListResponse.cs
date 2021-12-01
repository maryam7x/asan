using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.ResourceParams
{
    public class ListResponse<T> : List<T>
    {
        public int TotalCount { get; private set; }       
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public bool HasPreviousPage { get { return (PageNumber > 1); } }
        public bool HasNextPage { get { return (PageNumber < TotalPages); } }

        public ListResponse(List<T> list, int totalCount, int listCount, int pageNumber, int pageSize)
        {
            AddRange(list);
            TotalCount = totalCount;
            PageSize = pageSize;
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling((decimal)listCount / (decimal)pageSize);
        }

        public static ListResponse<T> Generate(int totalCount, List<T> list, int pageNumber, int pageSize)
        {
            return new ListResponse<T>(list, totalCount, list.Count(), pageNumber, pageSize);
        }
    }
}
