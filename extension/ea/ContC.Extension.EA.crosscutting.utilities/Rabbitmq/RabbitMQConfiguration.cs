using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace ContC.Extension.EA.crosscutting.utilities.Rabbitmq
{
    public class RabbitMQConfiguration : IDisposable
    {
        private ConnectionFactory factory;
        private IConnection connection;
        private IModel channel;

        public RabbitMQConfiguration(string hostName, string userName, string password)
        {
            factory = new ConnectionFactory();
            factory.HostName = hostName;
            factory.UserName = userName;
            factory.Password = password;

            connection = factory.CreateConnection();
            channel = connection.CreateModel();
        }

        public void DeclareQueue(string queueName)
        {
            queueName = queueName.ToLower().Trim().Replace(" ", "") + ".queue";
            channel.QueueDeclare(queueName, true, false, false, null);
        }

        public void DeleteQueue(string queueName)
        {
            queueName = queueName.ToLower().Trim().Replace(" ", "") + ".queue";
            channel.QueueDelete(queueName, true, true);
        }

        public void QueueBind(string queueName, string exchangeName, string routingKey)
        {
            queueName = queueName.ToLower().Trim().Replace(" ", "") + ".queue";
            routingKey = routingKey.ToLower().Trim().Replace(" ", "");

            //Todas as mensagens que chegarem para o PublisherExchange deverão ser encaminhadas
            //para uma fila determinada pela Routing Key
            channel.QueueBind(queueName, exchangeName, routingKey);
        }

        public void QueueUnBind(string queueName, string exchangeName, string routingKey)
        {
            queueName = queueName.ToLower().Trim().Replace(" ", "") + ".queue";
            routingKey = routingKey.ToLower().Trim().Replace(" ", "");

            channel.QueueUnbind(queueName, exchangeName, routingKey, null);
        }


        public void Dispose()
        {
            if (connection != null && connection.IsOpen)
                connection.Close(); connection.Abort();
            if (channel != null && channel.IsOpen)
                channel.Close(); channel.Abort();
        }

        ~RabbitMQConfiguration()
        {
            this.Dispose();
        }
    }
}
