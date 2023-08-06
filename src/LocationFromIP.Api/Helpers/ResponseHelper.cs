using LocationFromIP.Api.Models;

namespace LocationFromIP.Api.Helpers
{
    public static class ResponseHelper
    {
        public static DataResponse<TData> Create<TData>(TData data) => new(data: data, links: new Dictionary<string, string>());

        public static DataResponse<TData> Create<TData>(TData data, IDictionary<string, string> links) => new(data: data, links: links);
    }
}
