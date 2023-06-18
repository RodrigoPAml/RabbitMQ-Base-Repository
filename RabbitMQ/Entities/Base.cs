using RabbitMQ.Client;

namespace Messageria
{
    /// <summary>
    /// Base class for rabbit mq operations
    /// </summary>
    public class Base
    {
        protected IConnection _connection;

        protected IModel _channel;

        public Base(string host, string user, string password)
        {
            var factory = new ConnectionFactory()
            {
                HostName = host,
                UserName = user,
                Password = password
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void DeclareExchange(string name, ExchangeTypeEnum type)
        {
            _channel.ExchangeDeclare(name, type.GetDescription(), autoDelete: true);
        }

        public void DeclareQueue(
            string name,
            bool brokerPersistent = false,
            bool singleConsumer = false,
            bool autoDelete = true,
            int consumerTimeout = 2_000_000,
            int priority = 10
        )
        {
            Dictionary<string, object> args = new Dictionary<string, object>();
         
            args["x-max-priority"] = priority;
            args["x-consumer-timeout"] = consumerTimeout;

            _channel.QueueDeclare(
                queue: name,
                durable: brokerPersistent,
                exclusive: singleConsumer,
                autoDelete: autoDelete,
                arguments: args
            );
        }

        public void BindQueue(string queue, string exchange = "", string routingKey = "")
        {
            _channel.QueueBind(queue, exchange, routingKey);
        }

        public void Close()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
