using FootballWebAPI.Models;

namespace FootballWebAPI.Data.Json
{
    public class TeamConverter : IConverter<Team>
    {
        private readonly FootballAPIContext _context;
        public TeamConverter(FootballAPIContext context)
        {
            _context = context;
        }
        public object ToJson(Team team)
        {
            return new
            {
                name = team.Name,
                baseYear = team.BaseYear,
                country = _context.Countries.First(c => c.CountryId == team.CountryId).Name
            };
        }
    }
}
