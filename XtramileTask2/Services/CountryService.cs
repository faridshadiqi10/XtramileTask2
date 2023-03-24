using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XtramileTask2.Models;
using XtramileTask2.Services.Interfaces;

namespace XtramileTask2.Services
{
    public class CountryService : ICountryCityService
    {
        public List<City> GetCitiesAsync(string countryCode)
        {
            return _countryCities.TryGetValue(countryCode, out var cities) ? cities : new List<City>();
        }

        public List<Country> GetCountriesAsync()
        {
            return _countryCities.Keys.Select(code => new Country { Code = code, Name = _countryNames[code] }).ToList();
        }

        #region RawCountriesAndNames
        private readonly Dictionary<string, List<City>> _countryCities = new Dictionary<string, List<City>>
        {
            {
                "US", new List<City>
                {
                    new City { Name = "New York", CountryCode = "US" },
                    new City { Name = "Los Angeles", CountryCode = "US" },
                    new City { Name = "Chicago", CountryCode = "US" }
                }
            },
            {
                "UK", new List<City>
                {
                    new City { Name = "London", CountryCode = "UK" },
                    new City { Name = "Manchester", CountryCode = "UK" },
                    new City { Name = "Birmingham", CountryCode = "UK" }
                }
            }
        };

        private readonly Dictionary<string, string> _countryNames = new Dictionary<string, string>
        {
            { "US", "United States" },
            { "UK", "United Kingdom" }
        };
        #endregion
    }
}
