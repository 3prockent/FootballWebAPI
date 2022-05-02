namespace FootballWebAPI.Models
{
    public class Team
    {
        public Team()
        {
            HomeMatches = new List<Match>();
            GuestMatches = new List<Match>();
        }
        public Guid TeamId { get; set; }
        public string Name { get; set; }
        public int? BaseYear { get; set; }
        public Guid CountryId { get; set; }
        public virtual Country Country { get; set; }

        public virtual ICollection<Match> HomeMatches { get; set; }
        public virtual ICollection<Match> GuestMatches { get; set; }



    }
}
