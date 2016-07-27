using SharpRepository.Repository.Queries;

namespace SolutionTemplate.Service.Core.Extensions
{
    public static class StringExtensions
    {
        public static SortingOptions<T> ToSortingOptions<T>(this string sort)
        {
            var sorting = sort.Split(',');

            //TODO: Map business model fields to their data model equivalents.
            var sortingOptions = new SortingOptions<T>(sorting[0].TrimStart('-'), sorting[0].StartsWith("-"));

            if (sorting.Length > 1)
            {
                for (var i = 1; i < sorting.Length; i++)
                {
                    sortingOptions.ThenSortBy(sorting[i].TrimStart('-'), sorting[i].StartsWith("-"));
                }
            }

            return sortingOptions;
        }

        public static PagingOptions<T> ToPagingOptions<T>(this string sort, int pageNumber, int pageSize)
        {
            var sorting = sort.Split(',');

            //TODO: Map business model fields to their data model equivalents.
            var pagingOptions = new PagingOptions<T>(pageNumber, pageSize, sorting[0].TrimStart('-'), sorting[0].StartsWith("-"));

            if (sorting.Length > 1)
            {
                for (var i = 1; i < sorting.Length; i++)
                {
                    pagingOptions.ThenSortBy(sorting[i].TrimStart('-'), sorting[i].StartsWith("-"));
                }
            }

            return pagingOptions;
        }
    }
}