using FluentAssertions;
using LocationFromIP.Api.Enums;
using LocationFromIP.Api.Models;
using LocationFromIP.Application.Enums;
using Newtonsoft.Json;
using System.Net;

namespace LocationFromIP.Api.IntegrationTests.Controllers
{
    public class LocationControllerTest : IntegrationTest
    {
        [Test]
        public async Task GetLocation_ShouldReturnOkWithData()
        {
            //Arrange
            var ipv4Address = "1.1.1.1";

            //Act
            var response = await TestClient.GetAsync($"/api/v1.0/Location/{ipv4Address}");

            //Assert
            var rawData = await response.Content.ReadAsStringAsync();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            rawData.Should().NotBeNullOrEmpty();

            var jsonData = JsonConvert.DeserializeObject<DataResponse<IpLocationResponse>>(rawData);
            jsonData?.Data.Should().NotBeNull();
            //CAN'T MATCH UP THE RESPONSE TO EXACT VALUES AS LOCATION MIGHT CHANGE.
            jsonData?.Data?.Country.Should().NotBeNullOrEmpty();
            jsonData?.Data?.CountryCode.Should().NotBeNullOrEmpty();
            jsonData?.Data?.Region.Should().NotBeNullOrEmpty();
            jsonData?.Data?.RegionName.Should().NotBeNullOrEmpty();
            jsonData?.Data?.City.Should().NotBeNullOrEmpty();
            jsonData?.Data?.Zip.Should().NotBeNullOrEmpty();
            jsonData?.Data?.Latitude.Should().NotBe(0);
            jsonData?.Data?.Longitude.Should().NotBe(0);
        }

        [Test]
        public async Task GetLocation_ShouldReturnBadRequestWithErrorResponse()
        {
            //Arrange
            var ipv4Address = "NOT_AN_IP";

            //Act
            var response = await TestClient.GetAsync($"/api/v1.0/Location/{ipv4Address}");

            //Assert
            var rawData = await response.Content.ReadAsStringAsync();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            rawData.Should().NotBeNullOrEmpty();
            var jsonData = JsonConvert.DeserializeObject<ErrorResponse>(rawData);
            jsonData.Should().NotBeNull();
            jsonData?.Code.Should().Be(ApiFailureCodes.BadRequest);
            jsonData?.Message.Should().Be("The request was invalid.");
            jsonData?.Errors.Should().NotBeNull();
            jsonData?.Errors?.Length.Should().Be(1);
            jsonData?.Errors[0]?.ErrorCode.Should().Be(ValidationErrorCode.FieldValueInvalid);
            jsonData?.Errors[0]?.Message.Should().Be($"IPv4 address '{ipv4Address}' not valid");
        }
    }
}