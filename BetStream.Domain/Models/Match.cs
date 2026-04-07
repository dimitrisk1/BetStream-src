namespace BetStream.Domain.Models
{
    using System;

    public class Match
    {
        public Guid Id { get; set; }

        public int HomeTeamId { get; set; }
        public Team HomeTeam { get; set; } = null!;

        public int AwayTeamId { get; set; }
        public Team AwayTeam { get; set; } = null!;

        public decimal HomeOdds { get; set; }
        public decimal AwayOdds { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
