using AutoFixture;
using FluentAssertions;
using LocationFromIP.Application.Entities;
using LocationFromIP.Application.Exceptions;
using LocationFromIP.Application.Interfaces.Geoapify;
using LocationFromIP.Application.Interfaces.Persistence;
using LocationFromIP.Application.Models.Geoapify;
using LocationFromIP.Application.Services;
using Moq;

namespace LocationFromIP.Application.UnitTests.Services
{
    public class LocationServiceTest
    {
        private Fixture _fixture;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _fixture = new Fixture();
        }

        [Test]
        public async Task Get_ShouldReturnCachedData()
        {
            //Arrange
            var ipLocation = _fixture.Create<IpLocation>();
            var mockCacheService = new Mock<ICacheService>();
            mockCacheService.Setup(x => x.TryGet(It.IsAny<string>(), out ipLocation)).Returns(true);

            var locationService = new LocationService(mockCacheService.Object, null, null);

            //Act
            var response = await locationService.Get("1.1.1.1");

            //Assert
            response.Should().Be(ipLocation);
            mockCacheService.Verify(x => x.TryGet(It.IsAny<string>(), out ipLocation), Times.Once);
        }

        [Test]
        public async Task Get_ShouldReturnDataFromThirdPartyCall()
        {
            //Arrange
            IpLocation cachedIpLocation;
            var ipLocation = _fixture.Create<IpLocation>();
            var mockCacheService = new Mock<ICacheService>();
            mockCacheService.Setup(x => x.TryGet(It.IsAny<string>(), out cachedIpLocation)).Returns(false);
            mockCacheService.Setup(x => x.Set(It.IsAny<string>(), It.IsAny<IpLocation>())).Returns(ipLocation);

            var ipLookupResponse = _fixture.Create<IpLookupResponse>();
            var mockGeoapifyClient = new Mock<IGeoapifyClient>();
            mockGeoapifyClient.Setup(x => x.GetLocation(It.IsAny<string>())).ReturnsAsync(ipLookupResponse);

            var mockIpLocationRepository = new Mock<IIpLocationRepository>();
            mockIpLocationRepository.Setup(x => x.AddAsync(It.IsAny<IpLocation>())).ReturnsAsync(ipLocation);

            var locationService = new LocationService(mockCacheService.Object, mockIpLocationRepository.Object, mockGeoapifyClient.Object);

            //Act
            var response = await locationService.Get("1.1.1.1");

            //Assert
            response.Should().Be(ipLocation);
            mockCacheService.Verify(x => x.TryGet(It.IsAny<string>(), out ipLocation), Times.Once);
            mockCacheService.Verify(x => x.Set(It.IsAny<string>(), It.IsAny<IpLocation>()), Times.Once);
            mockGeoapifyClient.Verify(x => x.GetLocation(It.IsAny<string>()), Times.Once);
            mockIpLocationRepository.Verify(x => x.AddAsync(It.IsAny<IpLocation>()), Times.Once);
        }

        [Test]
        public async Task Get_ShouldReturnBadRequestException()
        {
            //Arrange
            var locationService = new LocationService(null, null, null);

            //Act
            Func<Task> act = async () => await locationService.Get("NOT_AN_IP");

            //Assert
            await act.Should().ThrowAsync<BadRequestException>();
        }
    }
}
