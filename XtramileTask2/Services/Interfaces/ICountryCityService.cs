using System.Collections.Generic;
using System.Threading.Tasks;
using XtramileTask2.Models;

namespace XtramileTask2.Services.Interfaces
{
    public interface ICountryCityService
    {
        List<Country> GetCountriesAsync();
        List<City> GetCitiesAsync(string countryCode);
    }
}
