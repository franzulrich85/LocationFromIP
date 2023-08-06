using Microsoft.AspNetCore.Mvc.Testing;

namespace LocationFromIP.Api.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;

        protected IntegrationTest() {
            var appFactory = new WebApplicationFactory<Program>();
            TestClient = appFactory.CreateDefaultClient();
        }
    }
}
