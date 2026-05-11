namespace BetStream.Infrastucture.Services
{
    using BetStream.Infrastucture.Options;
    using Confluent.Kafka;
    using Microsoft.Extensions.Options;

    public class KafkaProducer
    {
        private readonly IProducer<Null, string> _producer;
        private readonly KafkaOptions _options;

        public KafkaProducer(IOptions<KafkaOptions> options)
        {
            _options = options.Value;

            var config = new ProducerConfig
            {
                BootstrapServers = _options.BootstrapServers
            };

            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task SendMessage(string message)
        {
            await _producer.ProduceAsync(_options.Topic, new Message<Null, string>
            {
                Value = message
            });
        }
    }
}
