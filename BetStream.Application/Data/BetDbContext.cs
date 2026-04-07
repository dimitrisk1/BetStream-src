namespace BetStream.Application.Data
{
    using BetStream.Domain.Models;
    using Microsoft.EntityFrameworkCore;

    public class BetDbContext : DbContext
    {
        public BetDbContext(DbContextOptions<BetDbContext> options) : base(options) { }

        public DbSet<Match> Matches { get; set; }
    }

}
