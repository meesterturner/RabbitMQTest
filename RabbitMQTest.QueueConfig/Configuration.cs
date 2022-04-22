using System;
using RabbitMQ.Client;
namespace RabbitMQTest.QueueConfig
{
    public class Configuration
    {
        public string QueueName { get; set; } = "eseries.search";
        public bool PersistentQueue { get; set; } = true;

        public IModel Queue()
        {
            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();

            channel.QueueDeclare(queue: QueueName,
                                    durable: PersistentQueue,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

            channel.BasicQos(0, 1, false);

            return channel;
        }
    }
}
