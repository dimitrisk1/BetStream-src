namespace BetStream.Application.Data
{
    using BetStream.Domain.Models;
    using Microsoft.EntityFrameworkCore;

    public class BetDbContext : DbContext
    {
        public BetDbContext(DbContextOptions<BetDbContext> options) : base(options) { }

        public DbSet<Match> Matches { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<MessageEvent> MessageEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>(b =>
            {
                b.HasKey(t => t.Id);
                b.Property(t => t.Name).IsRequired().HasMaxLength(200);
                b.HasIndex(t => t.Name).IsUnique();
            });

            modelBuilder.Entity<Match>(b =>
            {
                b.HasKey(m => m.Id);
                b.Property(m => m.HomeOdds).HasColumnType("numeric(18,4)");
                b.Property(m => m.AwayOdds).HasColumnType("numeric(18,4)");
                b.HasOne(m => m.HomeTeam).WithMany(t => t.HomeMatches).HasForeignKey(m => m.HomeTeamId).OnDelete(DeleteBehavior.Restrict);
                b.HasOne(m => m.AwayTeam).WithMany(t => t.AwayMatches).HasForeignKey(m => m.AwayTeamId).OnDelete(DeleteBehavior.Restrict);
                b.Property(m => m.StartTime).IsRequired();
            });

            modelBuilder.Entity<MessageEvent>(b =>
            {
                b.HasKey(e => e.Id);
                b.Property(e => e.Topic).IsRequired().HasMaxLength(200);
                b.Property(e => e.Key).HasMaxLength(200);
                b.Property(e => e.Payload).IsRequired();
                b.HasIndex(e => e.ReceivedAt);
            });
        }
    }

}
