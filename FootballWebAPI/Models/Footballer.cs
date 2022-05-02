namespace FootballWebAPI.Models
{
    public class Footballer
    {
        public Guid FootballerId { get; set; }
        public string Name { get; set; }
        public Guid CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}
