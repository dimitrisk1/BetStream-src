namespace BetStream.Infrastucture.Options
{
    public class KafkaOptions
    {
        public const string SectionName = "Kafka";

        public string BootstrapServers { get; set; } = "localhost:9092";

        public string Topic { get; set; } = "betstream.messages";

        public string ConsumerGroupId { get; set; } = "betstream-consumers";
    }
}
