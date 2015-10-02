using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace ContC.Extension.EA.crosscutting.utilities.Rabbitmq
{
    public class RabbitMQConsumer : IDisposable
    {
        #region Properties

        public string QueueName { get; set; }
        public IConnection Connection { get; set; }
        public IModel Channel { get; set; }

        #endregion Properties

        private bool isConsuming;
        private BasicGetResult result = null;

        // used to pass messages back to UI for processing
        public delegate void OnReceiveMessage(byte[] message);
        public event OnReceiveMessage OnMessageReceived;

        public RabbitMQConsumer(string hostName, string userName, string password, string queueName, int port)
        {
            QueueName = !queueName.Contains(".queue") ? queueName + ".queue" : queueName;
            ConnectionFactory connectionFactory = new ConnectionFactory
            {
                HostName = hostName,
                UserName = userName,
                Password = password
            };
           Connection = connectionFactory.CreateConnection();
            Channel = Connection.CreateModel();
        }

        public byte[] Pop()
        {
            result = Channel.BasicGet(QueueName, noAck: false);
            if (result != null)
            {
                IBasicProperties props = result.BasicProperties;
                return result.Body;
            }
            return null;
        }

        public void AcknowledgeMsg(bool isMessageDelivered)
        {
            if (isMessageDelivered)
                Channel.BasicAck(result.DeliveryTag, false);
            else
                Channel.BasicRecover(true);
        }

        public void Dispose()
        {
            if (Connection != null && Connection.IsOpen)
                Connection.Close(); Connection.Abort();
            if (Channel != null && Channel.IsOpen)
                Channel.Close(); Channel.Abort();
        }

        ~RabbitMQConsumer()
        {
            this.Dispose();
        }
    }
}
