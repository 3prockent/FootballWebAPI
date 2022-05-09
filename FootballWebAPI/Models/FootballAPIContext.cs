using Microsoft.EntityFrameworkCore;

namespace FootballWebAPI.Models
{
    public partial class FootballAPIContext:DbContext
    {

        public FootballAPIContext(DbContextOptions<FootballAPIContext> options)
            : base(options)
        { 
            if (Database.EnsureCreated())
            {
                #region Init Countries
                var Spain = new Country() { CountryId = Guid.NewGuid(), Name = "Spain"};
                var Brazil = new Country() { CountryId = Guid.NewGuid(), Name = "Brazil"};
                var Argentina = new Country() { CountryId = Guid.NewGuid(), Name = "Argentina"};
                var Germany = new Country() { CountryId = Guid.NewGuid(), Name = "Germany"};
                var France = new Country() { CountryId = Guid.NewGuid(), Name = "France"};

                Countries.AddRange(Spain, Brazil, Argentina, Germany);
                #endregion
                #region Init Teams
                var Barcelona = new Team() { TeamId = Guid.NewGuid(), Name = "Barcelona", Country = Spain, BaseYear = 1899};
                var RealMadrid = new Team() { TeamId = Guid.NewGuid(), Name = "Barcelona", Country = Spain, BaseYear = 1899};

                Teams.AddRange(Barcelona, RealMadrid);
                #endregion
                #region Init Tournaments
                var LaLiga =         new Tournament() { TournamentId = Guid.NewGuid(), Name = "La Liga"};
                var ChampionLeague = new Tournament() { TournamentId = Guid.NewGuid(), Name = "Champion League"};

                Tournaments.AddRange(LaLiga, ChampionLeague);
                #endregion
                #region Init Footballers
                //Barcelona
                var Ferran =    new Footballer() { FootballerId = Guid.NewGuid(), Name = "Ferran", Country = Spain, Team=Barcelona};
                var Fati =      new Footballer() { FootballerId = Guid.NewGuid(), Name = "Fati", Country = Spain, Team = Barcelona };
                var Traore =    new Footballer() { FootballerId = Guid.NewGuid(), Name = "Traore", Country = Spain, Team = Barcelona };
                var Pedri =     new Footballer() { FootballerId = Guid.NewGuid(), Name = "Ferran", Country = Spain, Team = Barcelona };
                var Busquets =  new Footballer() { FootballerId = Guid.NewGuid(), Name = "Busquets", Country = Spain, Team = Barcelona };
                var Gavi =      new Footballer() { FootballerId = Guid.NewGuid(), Name = "Gavi", Country = Spain, Team = Barcelona };

                //Real Madrid
                var Vinicius = new Footballer() { FootballerId = Guid.NewGuid(), Name = "Vinicius", Country = Brazil, Team = RealMadrid };
                var Benzema = new Footballer() { FootballerId = Guid.NewGuid(), Name = "Benzema", Country = France, Team = RealMadrid };
                var Rodrygo = new Footballer() { FootballerId = Guid.NewGuid(), Name ="Rodrygo", Country = Brazil, Team = RealMadrid };
                var Casemiro = new Footballer() { FootballerId = Guid.NewGuid(), Name ="Casemiro", Country = Brazil, Team = RealMadrid };
                var Kroos = new Footballer() { FootballerId = Guid.NewGuid(), Name ="Kroos", Country = Germany, Team = RealMadrid };
                var Mendy = new Footballer() { FootballerId = Guid.NewGuid(), Name ="Mendy", Country = France, Team = RealMadrid };
                
                Footballers.AddRange(Ferran,Fati,Traore, Pedri, Busquets, Gavi, Vinicius, Benzema, Rodrygo,Casemiro, Kroos,Mendy);
                #endregion
                #region Init Matches
                var Match1=new Match() { MatchId=Guid.NewGuid(), Tournament=LaLiga, HomeTeam=RealMadrid, GuestTeam=Barcelona,StartTime=new DateTime(2000,10,03)};
                var Match2=new Match() { MatchId=Guid.NewGuid(), Tournament=ChampionLeague, HomeTeam=Barcelona, GuestTeam=RealMadrid,StartTime=new DateTime(2003,11,25)};

                Matches.AddRange(Match1, Match2);
                #endregion
                SaveChanges();
            }



            SaveChanges();
        }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Footballer> Footballers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            if(!optionBuilder.IsConfigured)
                optionBuilder.UseSqlServer("Server= DESKTOP-1GRC7IR\\SQLEXPRESS;Database=FootballAPI;Integrated Security=true;TrustServerCertificate=True;MultipleActiveResultSets=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>()
                .HasOne<Team>(m=>m.HomeTeam)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Match>()
               .HasOne<Team>(m => m.GuestTeam)
               .WithMany()
               .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Footballer>()
                .HasOne<Team>(f => f.Team)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Footballer>()
                .HasOne<Country>(f => f.Country)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientCascade);



            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
