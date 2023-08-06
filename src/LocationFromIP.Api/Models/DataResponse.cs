namespace LocationFromIP.Api.Models
{
    public class DataResponse<TData>
    {
        public TData Data { get;}
        public IDictionary<string, string> Links { get;}

        public DataResponse(TData data, IDictionary<string, string> links)
        {
            Data = data;
            Links = links;
        }
    }
}
