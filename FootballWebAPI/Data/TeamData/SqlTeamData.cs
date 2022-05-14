using FootballWebAPI.Models;

namespace FootballWebAPI.Data.TeamData
{
    public class SqlTeamData : ITeamData
    {
        private readonly FootballAPIContext _context;

        public SqlTeamData(FootballAPIContext context)
        {
            _context = context;
        }

        public List<Team> GetTeams()
        {
            return _context.Teams.ToList();
        }
        public Team GetTeamById(Guid id)
        {
            var team = _context.Teams.Find(id);
            return team;
        }

        public Team GetTeamByName(string name)
        {
            Team team = _context.Teams.First(c => c.Name.ToLower() == name.ToLower());
            return team;
        }

        public bool AddTeam(Team newTeam)
        {
            try
            {
                _context.Teams.Add(newTeam);
                _context.SaveChanges();
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }

        public bool EditTeam(Team team, int year)
        {
            if (team == null)
                return false;
            team.BaseYear = year;
            _context.Update(team);
            _context.SaveChanges();
            return true;
        }


        public void DeleteTeam(Team team)
        {
            _context.Teams.Remove(team);
            _context.SaveChanges();
        }

        public bool AlreadyExist(string name)
        {
            return _context.Teams.Any(t => t.Name.ToLower() == name.ToLower());
        }
    }
}
