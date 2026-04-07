namespace BetStream.Domain.Models
{
    public class Match
    {
        public Guid Id { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public decimal HomeOdds { get; set; }
        public decimal AwayOdds { get; set; }
    }
}
