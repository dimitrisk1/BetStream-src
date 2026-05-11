using BetStream.Infrastucture.Options;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BetStream.Infrastucture.Services
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly KafkaOptions _options;
        private readonly ILogger<KafkaConsumerService> _logger;

        public KafkaConsumerService(IOptions<KafkaOptions> options, ILogger<KafkaConsumerService> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _options.BootstrapServers,
                GroupId = _options.ConsumerGroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe(_options.Topic);

            while (!stoppingToken.IsCancellationRequested)
            {
                var result = consumer.Consume(stoppingToken);
                _logger.LogInformation("Received message from Kafka topic {Topic}: {Message}", _options.Topic, result.Message.Value);
            }

            return Task.CompletedTask;
        }
    }
}
