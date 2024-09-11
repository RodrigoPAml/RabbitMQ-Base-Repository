using System.Security.Cryptography;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Messageria
{
    /// <summary>
    /// Base class for consumer
    /// </summary>
    class Consumer : Base
    {
        private int _debugId { get; }

        public Consumer(string host, string user, string password) : base(host, user, password) 
        {
            _debugId = RandomNumberGenerator.GetInt32(100);
        }

        public void RegisterConsumer(
            string queue, 
            bool exclusive = false,
            ErrorBehaviourEnum errorBehaviour = ErrorBehaviourEnum.NackWithRequeue,
            OnRecieveMessage? callback = null
        )
        {
            var consumer = new EventingBasicConsumer(_channel);
            
            consumer.Received += (model, arg) =>
            {
                var body = arg.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                try
                {
                    if (callback != null)
                        callback(_debugId, queue, message, arg.DeliveryTag);
                    
                    _channel.BasicAck(arg.DeliveryTag, false);
                }
                catch (Exception)
                {
                    switch (errorBehaviour)
                    {
                        case ErrorBehaviourEnum.NackWithRequeue:
                            _channel.BasicNack(arg.DeliveryTag, false, true);
                            break;
                        case ErrorBehaviourEnum.Nack:
                            _channel.BasicNack(arg.DeliveryTag, false, true);
                            break;
                        case ErrorBehaviourEnum.Ack:
                            break;
                    }

                }
            };

            _channel.BasicConsume(
                queue: queue,
                autoAck: false,
                consumer: consumer,
                exclusive: exclusive
            );
        }

        public void SetConsumerQos(uint prefetchSize, ushort prefetchCount)
        {
            _channel.BasicQos(prefetchSize, prefetchCount, false);
        }
    }
}