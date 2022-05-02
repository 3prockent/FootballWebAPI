namespace FootballWebAPI.Models
{
    public class Match
    {
        public Guid MatchId { get; set; }
        public DateTime StartTime { get; set; }
        public Guid TournamentId { get; set; }
        public Guid HomeTeamId { get; set; }
        public Guid GuestTeamId { get; set; }

        public virtual Tournament Tournament { get; set; }
        public virtual Team HomeTeam { get; set; }
        public virtual Team GuestTeam { get; set; }
        
    }
}
