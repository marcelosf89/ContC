using System;

namespace ContC.crosscutting.utilities.RabbitMq
{
    public class RabbitMQProducer : IDisposable
    {
        public RabbitMQProducer(string hostName, string userName, string password, string exchangeName, string routingKey)
        {
            ExchangeName = exchangeName;
            RoutingKey = routingKey;
            var connectionFactory = new ConnectionFactory
            {
                HostName = hostName,
                UserName = userName,
                Password = password
            };
            Connection = connectionFactory.CreateConnection();
            Channel = Connection.CreateModel();
        }

        public void Produce(byte[] messageBytes)
        {
            IBasicProperties basicProperties = Channel.CreateBasicProperties();
            Channel.BasicPublish(ExchangeName, RoutingKey, basicProperties, messageBytes);
        }


        public void Dispose()
        {
            if (Connection != null && Connection.IsOpen)
                Connection.Close(); Connection.Abort();
            if (Channel != null && Channel.IsOpen)
                Channel.Close(); Channel.Abort();
        }

        ~RabbitMQProducer()
        {
            this.Dispose();
        }

        public string ExchangeName { get; set; }

        public string RoutingKey { get; set; }

        public IConnection Connection { get; set; }

        public IModel Channel { get; set; }

    }
}
