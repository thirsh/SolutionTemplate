using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http;
using System.Web.Http.Routing;

namespace SolutionTemplate.RestApi.Entities
{
    public class PaginationHeader
    {
        public PaginationHeader(HttpRequestMessage request, string routeName, string sort, int pageNumber, int pageSize, int totalCount)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;

            TotalPages = pageSize == 0
                ? 0
                : (int)Math.Ceiling((double)TotalCount / PageSize);

            var urlHelper = new UrlHelper(request);

            PreviousPageLink = pageNumber <= 1
                ? null
                : urlHelper.Link(routeName, new { sort, pageNumber = pageNumber - 1, pageSize });

            NextPageLink = pageNumber >= TotalPages
                ? null
                : urlHelper.Link(routeName, new { sort, pageNumber = pageNumber + 1, pageSize });
        }

        public int PageNumber { get; }
        public int PageSize { get; }
        public string PreviousPageLink { get; }
        public string NextPageLink { get; }
        public int TotalCount { get; }
        public int TotalPages { get; }

        public string JsonSerialize()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
    }
}