using FootballWebAPI.Models;
namespace FootballWebAPI.Data.Json
{
    public class FootballerConverter: IConverter<Footballer>
    {
        private readonly FootballAPIContext _context;
        public FootballerConverter(FootballAPIContext context)
        {
            _context = context; 
        }
        public object ToJson(Footballer footballer)
        {
            return new
            {
                name = footballer.Name,
                country = _context.Countries.First(c=>c.CountryId==footballer.CountryId)?.Name,
                team = _context.Teams.First(t => t.TeamId == footballer.TeamId)?.Name
            };

        }
    }
}
