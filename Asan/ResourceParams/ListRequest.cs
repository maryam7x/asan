using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.ResourceParams
{
    public class ListRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public Sort[] Sorts { get; set; }
        public string ContainsText { get; set; }
        public QueryFilter[] Filters { get; set; }
    }
   
    public class QueryFilter
    {
        public string Field { get; set; }
        public QueryOperator Operator { get; set; }
        public object Value { get; set; }
    }

    public class Sort
    {
        public string Field { get; set; } = "Id";
        public SortOrder Order { get; set; } = SortOrder.Asc;
    }

    public enum QueryOperator
    {
        Equal = 1,
        NotEqual = 2,
        Contains = 3
    }

    public enum SortOrder
    {
        Asc = 1,
        Desc = 2
    }
}
