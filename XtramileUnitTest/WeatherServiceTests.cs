using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XtramileTask2.Services.Interfaces;
using XtramileTask2.Services;
using Xunit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Castle.Core.Configuration;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using Moq.Protected;
using System.Net;
using System.Threading;

namespace XtramileUnitTest
{
    public class WeatherServiceTests
    {
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly WeatherService _weatherService;

        public WeatherServiceTests()
        {
            var httpClientMock = new Mock<HttpMessageHandler>();

            httpClientMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(GetSampleWeatherJson())
                });

            var httpClient = new HttpClient(httpClientMock.Object);
            _configurationMock = new Mock<IConfiguration>();

            _configurationMock.SetupGet(config => config["OpenWeatherMap:ApiKey"]).Returns("a1cb7b8f6b6fe870e1d6f85fabc3466b");

            var loggerMock = new Mock<ILogger<WeatherService>>();

            _weatherService = new WeatherService(httpClient, _configurationMock.Object, loggerMock.Object);
        }

        [Fact]
        public async Task GetWeatherAsync_ValidCityAndCountryCode_ReturnsWeatherInfo()
        {
            var cityName = "London";
            var countryCode = "GB";

            var weatherInfo = await _weatherService.GetWeatherAsync(cityName, countryCode);

            Assert.NotNull(weatherInfo);
            Assert.Equal($"{cityName}, {countryCode}", weatherInfo.Location);
        }

        private string GetSampleWeatherJson()
        {
            return @"
            {
                ""coord"": {""lon"": -0.1257, ""lat"": 51.5085},
                ""weather"": [{""id"": 300, ""main"": ""Drizzle"", ""description"": ""light intensity drizzle"", ""icon"": ""09d""}],
                ""base"": ""stations"",
                ""main"": {""temp"": 280.32, ""feels_like"": 277.59, ""temp_min"": 279.15, ""temp_max"": 281.48, ""pressure"": 1012, ""humidity"": 81},
                ""visibility"": 10000,
                ""wind"": {""speed"": 4.12, ""deg"": 80},
                ""clouds"": {""all"": 90},
                ""dt"": 1485789600,
                ""sys"": {""type"": 1, ""id"": 5091, ""country"": ""GB"", ""sunrise"": 1485762037, ""sunset"": 1485794875},
                ""id"": 2643743,
                ""name"": ""London"",
                ""cod"": 200
            }";
        }
    }
}
