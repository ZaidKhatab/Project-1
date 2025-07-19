using Domains.Interfaces;

namespace Services;

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
