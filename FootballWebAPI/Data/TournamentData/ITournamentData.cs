using FootballWebAPI.Models;

namespace FootballWebAPI.Data.TournamentData
{
    public interface ITournamentData
    {
        public List<Tournament> GetTournaments();
        public Tournament GetTournament(Guid id);
        public Tournament AddTournament(string enttournamName);
        public void DeleteTournament(Tournament tournament);
        public bool EditTournament(Guid id, Tournament newTournament);
        public bool AlreadyExist(string name);
    }
}
