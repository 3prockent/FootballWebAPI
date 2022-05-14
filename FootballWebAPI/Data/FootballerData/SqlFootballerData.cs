using FootballWebAPI.Models;

namespace FootballWebAPI.Data
{
    public class SqlFootballerData : IFootballerData
    {
        private readonly FootballAPIContext _context;

        public SqlFootballerData(FootballAPIContext context)
        {
            _context = context;
        }


        public List<Footballer> GetFootballers()
        {
            return _context.Footballers.ToList();
        }

        public Footballer GetFootballerById(Guid id)
        {
            return _context.Footballers.Find(id);
        }

        public Footballer GetFootballerByName(string name)
        {
            Footballer footballer = _context.Footballers.First(f => f.Name.ToLower() == name.ToLower());
            return footballer;
        }

        public bool AddFootballer(Footballer newFootballer)
        {
            try
            {
                _context.Footballers.Add(newFootballer);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool EditFootballer(Footballer footballer, Team team)
        {
            if (footballer == null||team==null)
                return false;
            
            footballer.TeamId = team.TeamId;
            _context.Update(footballer);
            _context.SaveChanges();
            return true;
        }


        public void DeleteFootballer(Footballer footballer)
        {
            _context.Footballers.Remove(footballer);
            _context.SaveChanges();
        }

        public bool AlreadyExist(string name)
        {
            return _context.Footballers.Any(f => f.Name.ToLower() == name.ToLower()); 
        }

    }
}
