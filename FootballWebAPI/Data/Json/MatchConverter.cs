using FootballWebAPI.Models;

namespace FootballWebAPI.Data.Json
{
    public class MatchConverter : IConverter<Match>
    {
        FootballAPIContext _context;
        public MatchConverter(FootballAPIContext context)
        {
            _context = context;
        }
        public object ToJson(Match match)
        {
            string tournamentName;
            if (match.TournamentId == null)
                tournamentName = null;
            else
                tournamentName = _context.Tournaments.First(t => t.TournamentId == match.TournamentId).Name;
            return new
            {
                startTime = match.StartTime,
                tournament = tournamentName,
                homeTeam = _context.Teams.First(t => t.TeamId == match.HomeTeamId).Name,
                guestTeam = _context.Teams.First(t => t.TeamId == match.GuestTeamId).Name
            };
        }
    }
}
