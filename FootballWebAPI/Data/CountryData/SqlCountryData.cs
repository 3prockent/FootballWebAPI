using FootballWebAPI.Models;

namespace FootballWebAPI.Data.CountryData
{

    public class SqlCountryData : ICountryData
    {
        private readonly FootballAPIContext _context;

        public SqlCountryData(FootballAPIContext context)
        {
            _context = context;
        }
        public List<Country> GetCountries()
        {
            return _context.Countries.ToList();
        }

        public Country GetCountry(Guid id)
        {
            var country = _context.Countries.Find(id);
            return country;
        }

        public Country GetCountry(string name)
        {
            var country = _context.Countries.First(c => c.Name.ToLower() == name.ToLower());
            return country;
        }


        public Country? AddCountry(string countryName)
        {
            var newCountry = new Country() {CountryId=Guid.NewGuid(),
                                            Name=countryName};
            if(_context.Countries.Add(newCountry)!=null)
            {
                _context.SaveChanges();
                return newCountry;
            }    
            return null;

        }

        public void DeleteCountry(Country country)
        {
            _context.Countries.Remove(country);
            _context.SaveChanges();
        }

        public bool EditCountry(Guid id, Country newCountry)
        {
            var editableCountry = _context.Countries.Find(id);
            if (editableCountry != null)
            {
                editableCountry.Name = newCountry.Name;
                _context.Countries.Update(editableCountry);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool AlreadyExist(string name)
        {
            return _context.Countries.Any(c => c.Name.ToLower() == name.ToLower());
        }

    }
}
