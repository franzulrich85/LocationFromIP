namespace LocationFromIP.Api.Models
{
    public class IpLocationResponse
    {
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string Region { get; set; }
        public string RegionName { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}
