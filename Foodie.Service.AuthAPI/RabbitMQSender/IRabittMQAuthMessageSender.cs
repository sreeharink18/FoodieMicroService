namespace Foodie.Service.AuthAPI.RabbitMQSender
{
    public interface IRabittMQAuthMessageSender
    {
        void SendMessage(object message,string queueName);
    }
}
