using System.Text.RegularExpressions;

namespace LocationFromIP.Application.Helpers
{
    public static class IpV4AddressHelper
    {
        public static bool IsValidateIp(string address)
        {
            if (string.IsNullOrEmpty(address))
                return false;

            Regex validateIPv4Regex = new Regex("^(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");

            return validateIPv4Regex.IsMatch(address, 0);
        }
    }
}
