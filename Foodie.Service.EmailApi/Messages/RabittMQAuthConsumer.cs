
using Foodie.Service.EmailApi.ExternalServices;
using Foodie.Service.EmailApi.Utility;

using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Foodie.Service.EmailApi.Messages
{
    public class RabittMQAuthConsumer : BackgroundService
    {
        private readonly EmailService _emailService;
        private IConnection _connection;
        private readonly IChannel _channel;
        public RabittMQAuthConsumer(EmailService emailService) { 
            _emailService = emailService;
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
            _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();
            _channel.QueueDeclareAsync(SD.RabbitMQQueueName, false, false, false, null).GetAwaiter().GetResult();
         
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += (model, ea) =>
            {
                var context = Encoding.UTF8.GetString(ea.Body.ToArray());
                string email = JsonConvert.DeserializeObject<string>(context);
                HandleMessage(email).GetAwaiter().GetResult();

                _channel.BasicAckAsync(ea.DeliveryTag, false).GetAwaiter().GetResult();
                
                return Task.CompletedTask;
            };

            _channel.BasicConsumeAsync(SD.RabbitMQQueueName, false, consumer);
            return Task.CompletedTask;
        }
        private async Task HandleMessage(string email)
        {
            _emailService.RegisterUserEmailAndLog(email).GetAwaiter().GetResult();   
        }
    }
}
