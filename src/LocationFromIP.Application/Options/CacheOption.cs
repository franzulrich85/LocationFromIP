namespace LocationFromIP.Application.Options
{
    public class CacheOption
    {
        public int AbsoluteExpirationInMinutes { get; set; }
        public int SlidingExpirationInMinutes { get; set; }
    }
}
