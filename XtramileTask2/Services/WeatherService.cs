using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using XtramileTask2.Models;
using XtramileTask2.Services.Interfaces;

namespace XtramileTask2.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private ILogger<WeatherService> _logger;

        public WeatherService(HttpClient httpClient, IConfiguration configuration, ILogger<WeatherService> logger)
        {
            _httpClient = httpClient;
            _apiKey = configuration["OpenWeatherMap:ApiKey"];
            _logger = logger;
        }

        public async Task<WeatherInfo> GetWeatherAsync(string cityName, string countryCode)
        {
            try
            {
                var url = $"http://api.openweathermap.org/data/2.5/weather?q={cityName},{countryCode}&appid={_apiKey}&units=metric";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Failed to fetch weather data");
                }

                var weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(await response.Content.ReadAsStringAsync());

                return ConvertWeatherDataToWeatherInfo(weatherResponse);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"GetWeatherAsync: {ex.Message}");
                throw new Exception (ex.Message);
            }
        }

        private WeatherInfo ConvertWeatherDataToWeatherInfo(WeatherResponse items)
        {
            return new WeatherInfo
            {
                Location = $"{items.name}, {items.sys.country}",
                Time = DateTimeOffset.FromUnixTimeSeconds(items.dt).DateTime,
                Wind = $"{items.wind.speed} m/s, {items.wind.deg}°",
                Visibility = $"{items.visibility} m",
                SkyConditions = items.weather[0].description,
                TemperatureCelsius = items.main.temp,
                TemperatureFahrenheit = items.main.temp * 9 / 5 + 32,
                DewPoint = -1,
                RelativeHumidity = $"{items.main.humidity}%",
                Pressure = $"{items.main.pressure} hPa"
            };
        }
    }
}
