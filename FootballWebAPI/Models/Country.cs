namespace FootballWebAPI.Models
{
    public class Country
    {
        public Country()
        {
            Teams = new List<Team>();
            Footers=new List<Footballer>(); 
        }
        public Guid CountryId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
        public virtual ICollection<Footballer> Footers { get; set; }
    }
}
