using System.Threading.Tasks;
using XtramileTask2.Models;

namespace XtramileTask2.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherInfo> GetWeatherAsync(string cityName, string countryCode);
    }
}
