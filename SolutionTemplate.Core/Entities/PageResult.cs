using System;
using System.Collections.Generic;

namespace SolutionTemplate.Core.Entities
{
    public class PageResult<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get { return PageSize == 0 ? 0 : (int)Math.Ceiling((double)TotalCount / PageSize); } }
        public List<T> Items { get; set; }
    }
}