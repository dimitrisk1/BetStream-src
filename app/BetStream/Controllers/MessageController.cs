using BetStream.Infrastucture.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BetStream.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly KafkaProducer _producer;

        public MessageController(KafkaProducer producer)
        {
            _producer = producer;
        }

        [HttpPost]
        public async Task<IActionResult> Send(string message)
        {
            await _producer.SendMessage("test-topic", message);
            return Ok("Message sent to Kafka");
        }
    }
}
