using FootballWebAPI.Models;

namespace FootballWebAPI.Data.TournamentData
{

    public class SqlTournamentData : ITournamentData
    {
        private readonly FootballAPIContext _context;

        public SqlTournamentData(FootballAPIContext context)
        {
            _context = context;
        }
        public List<Tournament> GetTournaments()
        {
            return _context.Tournaments.ToList();
        }

        public Tournament GetTournament(Guid id)
        {
            var tournament = _context.Tournaments.Find(id);
            return tournament;
        }
        public Tournament? AddTournament(string tournamentName)
        {
            var newTournament = new Tournament() {TournamentId=Guid.NewGuid(),
                                            Name=tournamentName};
            if(_context.Tournaments.Add(newTournament)!=null)
            {
                _context.SaveChanges();
                return newTournament;
            }    
            return null;
        }

        public void DeleteTournament(Tournament tournament)
        {
            _context.Tournaments.Remove(tournament);
            _context.SaveChanges();
        }

        public bool EditTournament(Guid id, Tournament newTournament)
        {
            var editableTournament = _context.Tournaments.Find(id);
            if (editableTournament != null)
            {
                editableTournament.Name = newTournament.Name;
                _context.Tournaments.Update(editableTournament);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool AlreadyExist(string name)
        {
            return _context.Tournaments.Any(c => c.Name.ToLower() == name.ToLower());
        }

        public Tournament GetTournamentByName(string name)
        {
            Tournament tournament = _context.Tournaments.FirstOrDefault(c => c.Name.ToLower() == name.ToLower());
            return tournament;
        }
    }
}
