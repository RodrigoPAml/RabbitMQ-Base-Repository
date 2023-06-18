using System.Text;
using RabbitMQ.Client;

namespace Messageria
{
    /// <summary>
    /// Base class for producer
    /// </summary>
    class Producer : Base
    {
        public Producer(string host, string user, string password) : base(host, user, password) 
        {
        }

        public void QueueMessage(string message, string routingKey = "", string exchange = "")
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange, routingKey, null, body);
        } 
    }
}