namespace FootballWebAPI.Models
{
    public class Team
    {
        public Guid TeamId { get; set; }
        public string Name { get; set; }
        public int? BaseYear { get; set; }
        public Guid CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}
