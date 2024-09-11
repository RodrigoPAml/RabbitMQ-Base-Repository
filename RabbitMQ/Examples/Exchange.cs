using Messageria;

namespace Examples
{
    public static class Exchange
    {
        private static void OnRecieveMessage(int id, string queue, string message, ulong tag)
        {
            Console.WriteLine($"Message '{message}' recieved to consumer '{id}' of queue '{queue}' with delivery tag '{tag}'");
            Thread.Sleep(1000);
        }

        /// <summary>
        /// A producer that can send between three diferent queues based on exchange
        /// </summary>
        public static void Execute()
        {
            Consumer consumer1 = new Consumer("localhost", "guest", "guest");

            // Declare a exchange (default is "")
            consumer1.DeclareExchange("exchange", ExchangeTypeEnum.Direct);
            consumer1.DeclareQueue("queue1");
            consumer1.BindQueue("queue1", "exchange");
            consumer1.RegisterConsumer("queue1", callback: OnRecieveMessage);

            Consumer consumer2 = new Consumer("localhost", "guest", "guest");

            // Declare a exchange (default is "")
            consumer2.DeclareExchange("exchange", ExchangeTypeEnum.Direct);
            consumer2.DeclareQueue("queue2");
            consumer2.BindQueue("queue2", "exchange");
            consumer2.RegisterConsumer("queue2", callback: OnRecieveMessage);

            Consumer consumer3 = new Consumer("localhost", "guest", "guest");

            // Declare a exchange (default is "")
            consumer3.DeclareExchange("exchange2", ExchangeTypeEnum.Direct);
            consumer3.DeclareQueue("queue3");
            consumer3.BindQueue("queue3", "exchange2");
            consumer3.RegisterConsumer("queue3", callback: OnRecieveMessage);

            Producer producer = new Producer("localhost", "guest", "guest");
            producer.QueueMessage("message", exchange: "exchange");
            producer.QueueMessage("message2", exchange: "exchange2");

            Console.ReadLine();
        }
    }
}
