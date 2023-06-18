using Messageria;

namespace Examples
{
    public static class Routing
    {
        private static void OnRecieveMessage(int id, string queue, string message, ulong tag)
        {
            Console.WriteLine($"Message '{message}' recieved to consumer '{id}' of queue '{queue}' with delivery tag '{tag}'");
            Thread.Sleep(1000);
        }

        /// <summary>
        /// A producer that send can send to three diferent queues based on routing keys
        /// </summary>
        public static void Execute()
        {
            Producer producer = new Producer("localhost", "guest", "guest");

            producer.DeclareExchange("default_exchange", ExchangeTypeEnum.Direct);

            // A queue for the speakers of portugueses
            producer.DeclareQueue("queue_speak_portuguese");
            producer.BindQueue("queue_speak_portuguese", "default_exchange", "brazilians");
            producer.BindQueue("queue_speak_portuguese", "default_exchange", "portugueses");

            // A queue for the speakers of english
            producer.DeclareQueue("queue_speak_english");
            producer.BindQueue("queue_speak_english", "default_exchange", "english");

            Consumer consumer1 = new Consumer("localhost", "guest", "guest");
            consumer1.RegisterConsumer("queue_speak_portuguese", callback: OnRecieveMessage);

            Consumer consumer2 = new Consumer("localhost", "guest", "guest");
            consumer2.RegisterConsumer("queue_speak_english", callback: OnRecieveMessage);

            producer.QueueMessage("i am speak portuguese", exchange: "default_exchange", routingKey: "portugueses");
            producer.QueueMessage("i am speak brazilian portuguese", exchange: "default_exchange", routingKey: "portugueses");
            producer.QueueMessage("i am speak english", exchange: "default_exchange", routingKey: "english");

            Console.ReadLine();
        }
    }
}
