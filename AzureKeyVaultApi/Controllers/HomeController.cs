using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AzureKeyVaultApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpGet]
        public async Task<string> Get()
        {
            var key1 = _configuration.GetValue<string>("somesetting");

            var key2 = _configuration.GetValue<string>("MyKey");

            return _configuration.GetValue<string>("somesetting");
        }

        [HttpGet(nameof(Send))]
        public async Task<string> Send(string message)
        {
            var factory = new ConnectionFactory() { 
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "samplequeue",
                                     basicProperties: null,
                                     body: body);
            }

            return $"Message Sent: {message}";
        }

        [HttpGet(nameof(Read))]
        public async Task<string> Read()
        {
            string message = string.Empty;
            var factory = new ConnectionFactory() { };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    message = Encoding.UTF8.GetString(body);
                };
                channel.BasicConsume(queue: "samplequeue",
                                     autoAck: true,
                                     consumer: consumer);
            }

            return $"Message: {message}";
        }
    }
}
