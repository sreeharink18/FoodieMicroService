namespace Foodie.Service.ShoppingCartAPI.RabbitMQSender
{
    public interface IRabittMQCartMessageSender
    {
        void SendMessage(object message,string queueName);
    }
}
