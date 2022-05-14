using FootballWebAPI.Models;

namespace FootballWebAPI.Data.TeamData
{ 
    public interface ITeamData
    {
        public List<Team> GetTeams();
        public Team GetTeamById(Guid id);
        public Team GetTeamByName(string name);
        public bool AddTeam(Team newTeam);
        public void DeleteTeam(Team team);
        public bool EditTeam(Team team, int year);
        public bool AlreadyExist(string name);
    }
}
