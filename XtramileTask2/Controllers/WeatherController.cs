using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using XtramileTask2.Services.Interfaces;

namespace XtramileTask2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly ICountryCityService _countryCityService;
        private readonly IWeatherService _weatherService;

        public WeatherController(ICountryCityService countryCityService, IWeatherService weatherService)
        {
            _countryCityService = countryCityService;
            _weatherService = weatherService;
        }

        [HttpGet("countries")]
        public IActionResult GetCountries()
        {
            var countries =  _countryCityService.GetCountriesAsync();
            return Ok(countries);
        }

        [HttpGet("cities")]
        public IActionResult GetCities([FromQuery] string countryCode)
        {
            var cities =  _countryCityService.GetCitiesAsync(countryCode);
            return Ok(cities);
        }

        [HttpGet("weather")]
        public async Task<IActionResult> GetWeather([FromQuery] string cityName, [FromQuery] string countryCode)
        {
            var weatherInfo = await _weatherService.GetWeatherAsync(cityName, countryCode);
            return Ok(weatherInfo);
        }
    }
}
