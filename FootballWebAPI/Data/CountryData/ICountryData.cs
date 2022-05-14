using FootballWebAPI.Models;

namespace FootballWebAPI.Data.CountryData
{
    public interface ICountryData
    {
        public List<Country> GetCountries();
        public Country GetCountry(Guid id);
        public Country GetCountry(string name);
        public Country AddCountry(string countryName);
        public void DeleteCountry(Country county);
        public bool EditCountry(Guid id, Country newCountry);
        public bool AlreadyExist(string name);

    }
}
