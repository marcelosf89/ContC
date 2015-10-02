using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace ContC.Extension.EA.crosscutting.utilities.Rabbitmq
{
    public class RabbitMQProducer : IDisposable
    {
        #region Properties

        public string ExchangeName { get; set; }
        public string RoutingKey { get; set; }
        public IConnection Connection { get; set; }
        public IModel Channel { get; set; }

        #endregion Properties

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
    }
}
