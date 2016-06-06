using SharpRepository.Repository.Queries;

namespace SolutionTemplate.Core.Extensions
{
    public static class StringExtensions
    {
        public static SortingOptions<T> ToSortingOptions<T>(this string sort)
        {
            var sorting = sort.Split(',');

            //TODO: Map business model fields need to their data model equivalents.
            var sortingOptions = new SortingOptions<T>(sorting[0].TrimStart('-'), sorting[0].StartsWith("-"));

            if (sorting.Length > 1)
            {
                for (int i = 1; i < sorting.Length; i++)
                {
                    sortingOptions.ThenSortBy(sorting[i].TrimStart('-'), sorting[i].StartsWith("-"));
                }
            }

            return sortingOptions;
        }
    }
}