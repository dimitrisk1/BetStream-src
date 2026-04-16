namespace BetStream.Domain.Models
{
    using System;

    public class MessageEvent
    {
        public long Id { get; set; }
        public string Topic { get; set; } = null!;
        public string? Key { get; set; }
        public string Payload { get; set; } = null!;
        public DateTime ReceivedAt { get; set; } = DateTime.UtcNow;
        public string? Partition { get; set; }
        public long? Offset { get; set; }
    }
}
