using System;
using System.Collections.Generic;

namespace SolutionTemplate.Service.Core.Entities
{
    public class PageResult<T>
    {
        public PageResult(int pageNumber, int pageSize, int totalCount, List<T> items)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            Items = items;
        }

        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages { get { return PageSize == 0 ? 0 : (int)Math.Ceiling((double)TotalCount / PageSize); } }
        public List<T> Items { get; }
    }
}