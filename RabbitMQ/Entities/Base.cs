using RabbitMQ.Client;

namespace Messageria
{
    /// <summary>
    /// Base class for RabbitMQ operations.
    /// </summary>
    public class Base
    {
        protected IConnection _connection;
        protected IModel _channel;

        /// <summary>
        /// Initializes a new instance of the <see cref="Base"/> class.
        /// </summary>
        /// <param name="host">The hostname of the RabbitMQ server.</param>
        /// <param name="user">The username for RabbitMQ authentication.</param>
        /// <param name="password">The password for RabbitMQ authentication.</param>
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

        /// <summary>
        /// Declares an exchange with the specified name and type.
        /// </summary>
        /// <param name="name">The name of the exchange to declare.</param>
        /// <param name="type">The type of the exchange, represented by the <see cref="ExchangeTypeEnum"/>.</param>
        public void DeclareExchange(string name, ExchangeTypeEnum type)
        {
            _channel.ExchangeDeclare(name, type.GetDescription(), autoDelete: true);
        }

        /// <summary>
        /// Declares a queue with the specified parameters in RabbitMQ.
        /// </summary>
        /// <param name="name">The name of the queue to declare.</param>
        /// <param name="brokerPersistent">Determines whether the queue should be durable. If true, the queue will survive broker restarts. Default is false.</param>
        /// <param name="singleConsumer">Specifies whether the queue should be exclusive to a single consumer. If true, the queue is used by only one consumer and is deleted when the consumer disconnects. Default is false.</param>
        /// <param name="autoDelete">Indicates whether the queue should be automatically deleted when no longer in use. Default is true.</param>
        /// <param name="consumerTimeout">Sets the timeout (in milliseconds) for consumers to be automatically disconnected if they do not send a heartbeat. Default is 2,000,000 milliseconds (or 2,000 seconds).</param>
        /// <param name="priority">Sets the maximum priority of the messages that can be sent to the queue. Valid values are from 0 to 255. Default is 10.</param>
        /// <remarks>
        /// The method sets additional arguments for the queue:
        /// - "x-max-priority": Defines the maximum priority level for the messages in the queue. This allows message prioritization based on the value of this argument.
        /// - "x-consumer-timeout": Defines the consumer timeout, after which a consumer is considered inactive if no heartbeat is received. This argument is useful for managing consumer lifecycles.
        /// The method uses the RabbitMQ client library's QueueDeclare method to create or modify a queue with the specified settings.
        /// </remarks>
        public void DeclareQueue(
            string name,
            bool brokerPersistent = false,
            bool singleConsumer = false,
            bool autoDelete = true,
            int consumerTimeout = 2_000_000,
            int priority = 10
        )
        {
            // Create a dictionary to hold additional arguments for the queue
            Dictionary<string, object> args = new Dictionary<string, object>();

            // Add the maximum priority for messages to the arguments
            args["x-max-priority"] = priority;

            // Add the consumer timeout to the arguments
            args["x-consumer-timeout"] = consumerTimeout;

            // Declare the queue with the specified parameters
            _channel.QueueDeclare(
                queue: name,
                durable: brokerPersistent,
                exclusive: singleConsumer,
                autoDelete: autoDelete,
                arguments: args
            );
        }

        /// <summary>
        /// Binds a queue to an exchange with the specified routing key.
        /// </summary>
        /// <param name="queue">The name of the queue to bind.</param>
        /// <param name="exchange">The name of the exchange to bind the queue to. Default is an empty string, which means no exchange.</param>
        /// <param name="routingKey">The routing key to use for binding. Default is an empty string.</param>
        public void BindQueue(string queue, string exchange = "", string routingKey = "")
        {
            _channel.QueueBind(queue, exchange, routingKey);
        }

        /// <summary>
        /// Closes the channel and the connection to RabbitMQ.
        /// </summary>
        public void Close()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
