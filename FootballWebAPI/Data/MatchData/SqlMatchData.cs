using FootballWebAPI.Models;

namespace FootballWebAPI.Data
{
    public class SqlMatchData : IMatchData
    {

        private readonly FootballAPIContext _context;

        public SqlMatchData(FootballAPIContext context)
        {
            _context = context;
        }
        public List<Match> GetMatches()
        {
            return _context.Matches.ToList();
        }

        public Match GetMatchById(Guid id)
        {
            var match = _context.Matches.Find(id);
            return match;
        }
        public List<Match> GetNearestMatches(DateTime term)
        {
            var nearestMatches = _context.Matches.Where(m => m.StartTime<=term).ToList();
            return nearestMatches;
        }

        public bool AddMatch(Match newMatch)
        {
            try
            {
                _context.Matches.Add(newMatch);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public void DeleteMatch(Match match)
        {
            _context.Matches.Remove(match);
            _context.SaveChanges();
        }

        public bool EditMatch(Match match, DateTime? time, Guid? tournamentId)
        {
            if (match == null)
                return false;
            match.TournamentId = tournamentId;
            match.StartTime = time;
            _context.Update(match);
            _context.SaveChanges();
            return true;
        }

    }
}
