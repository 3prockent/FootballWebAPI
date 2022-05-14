using FootballWebAPI.Models;

namespace FootballWebAPI.Data
{
    public interface IMatchData
    {
        public List<Match> GetMatches();
        public Match GetMatchById(Guid id);
        public List<Match> GetNearestMatches(DateTime term);
        public bool AddMatch(Match newMatch);
        public void DeleteMatch(Match match);
        public bool EditMatch(Match match, DateTime? time, Guid? tournamentId);
    }
}
