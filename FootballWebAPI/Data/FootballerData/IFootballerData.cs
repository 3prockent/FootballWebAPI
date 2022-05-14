using FootballWebAPI.Models;

namespace FootballWebAPI.Data
{
    public interface IFootballerData
    {
        public List<Footballer> GetFootballers();
        public Footballer GetFootballerById(Guid id);
        public Footballer GetFootballerByName(string name);
        public bool AddFootballer(Footballer newFootballer);
        public void DeleteFootballer(Footballer footballer);
        public bool EditFootballer(Footballer footballer, Team team);
        public bool AlreadyExist(string name);
    }
}
