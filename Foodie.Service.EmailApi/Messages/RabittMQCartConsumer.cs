
using Foodie.Service.EmailApi.ExternalServices;
using Foodie.Service.EmailApi.Utility;
using Foodie.Service.ShoppingCartAPI.Model.DTO;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Foodie.Service.EmailApi.Messages
{
    public class RabittMQCartConsumer : BackgroundService
    {
        private readonly EmailService _emailService;
        private IConnection _connection;
        private readonly IChannel _channel;
        public RabittMQCartConsumer(EmailService emailService) { 
            _emailService = emailService;
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
            _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();
            _channel.QueueDeclareAsync(SD.CartRabbitMQQueueName, false, false, false, null).GetAwaiter().GetResult();
         
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += (model, ea) =>
            {
                var context = Encoding.UTF8.GetString(ea.Body.ToArray());
                CartDto cartDto = JsonConvert.DeserializeObject<CartDto>(context);
                HandleMessage(cartDto).GetAwaiter().GetResult();

                _channel.BasicAckAsync(ea.DeliveryTag, false).GetAwaiter().GetResult();
                
                return Task.CompletedTask;
            };

            _channel.BasicConsumeAsync(SD.CartRabbitMQQueueName, false, consumer);
            return Task.CompletedTask;
        }
        private async Task HandleMessage(CartDto cartDto)
        {
            _emailService.EmailCartAndLog(cartDto).GetAwaiter().GetResult();   
        }
    }
}
