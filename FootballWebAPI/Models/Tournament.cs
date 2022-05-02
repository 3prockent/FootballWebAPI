namespace FootballWebAPI.Models
{
    public class Tournament
    {
        public Tournament()
        {
            Matches = new List<Match>();
        }
        public Guid TournamentId { get; set; }
        public string Name { get; set; }
        public ICollection<Match> Matches { get; set; }
    }
}
