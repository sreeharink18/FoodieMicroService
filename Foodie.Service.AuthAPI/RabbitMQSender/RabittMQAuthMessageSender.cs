

using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;


namespace Foodie.Service.AuthAPI.RabbitMQSender
{
    public class RabittMQAuthMessageSender : IRabittMQAuthMessageSender
    {
        private string _hostName;
        private string _userName;
        private string _password;
        public IConnection _connection;
        public RabittMQAuthMessageSender()
        {
            _hostName = "localhost";
            _userName = "guest";
            _password = "guest";
        }
        public async void SendMessage(object message, string queueName)
        {
            if (ConnectionExists())
            {
                using var channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();
                channel.QueueDeclareAsync(queueName, false, false, false, null).GetAwaiter().GetResult();
                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);
                await channel.BasicPublishAsync(exchange: "", routingKey: queueName, body: body);
            }
        }
        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostName,
                    UserName = _userName,
                    Password = _password
                };
                _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex) { }
        }
        private bool ConnectionExists()
        {
            if(_connection != null)
            {
                return true;
            }
            CreateConnection();
            return true;
        }
    }
}
