using Domains.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CountryService : ICountry
    {
        private readonly List<string> _countries;

        public CountryService()
        {
                _countries = new List<string>
                {
                    "United States",
                    "Canada",
                    "Mexico",
                    "United Kingdom",
                    "Germany",
                    "France",
                    "Italy",
                    "Spain",
                    "Australia",
                    "Jordan",
                    "Japan"
                };
        }
        public List<string> GetAllCountries()
        {
            return _countries;
        }
    }
}
