using Microsoft.EntityFrameworkCore;

namespace FootballWebAPI.Models
{
    public partial class FootballAPIContext:DbContext
    {
        public FootballAPIContext()
        {
            Database.EnsureCreated(); 
        }
        public FootballAPIContext(DbContextOptions<FootballAPIContext> options)
            : base(options)
        {
          
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
                .WithMany(t=>t.HomeMatches)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Match>()
               .HasOne<Team>(m => m.GuestTeam)
               .WithMany(t => t.GuestMatches)
               .OnDelete(DeleteBehavior.ClientCascade);



            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
