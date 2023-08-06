namespace LocationFromIP.Application.Models.Geoapify
{
    public class IpLookupResponse
    {
        public string Status { get; set; }
        public string Query { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string Region { get; set; }
        public string RegionName { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public float Lat { get; set; }
        public float Lon { get; set; }
        public string Timezone { get; set; }
        public string Isp { get; set; }
        public string Org { get; set; }
        public string As { get; set; }
        public string Message { get; set; }
    }

}
