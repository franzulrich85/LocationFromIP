namespace LocationFromIP.Application.Entities
{
    public class IpLocation : BaseEntity
    {
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string Region { get; set; }
        public string RegionName { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string Timezone { get; set; }
        public string IspProvider { get; set; }
        public string As { get; set; }
    }
}
