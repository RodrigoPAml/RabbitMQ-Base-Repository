using Messageria;

namespace Examples
{
    public static class ManyConsumers
    {
        private static void OnRecieveMessage(int id, string queue, string message, ulong tag)
        {
            Console.WriteLine($"Message '{message}' recieved to consumer '{id}' of queue '{queue}' with delivery tag '{tag}'");
            Thread.Sleep(1000);
        }

        /// <summary>
        /// A producer and a queue with two consumers competing
        /// </summary>
        public static void Execute()
        {
            // Create two consumers
            Consumer consumer1 = new Consumer("localhost", "guest", "guest");

            consumer1.DeclareQueue("queue");
            consumer1.RegisterConsumer("queue", callback: OnRecieveMessage);

            Consumer consumer2 = new Consumer("localhost", "guest", "guest");

            consumer2.DeclareQueue("queue");
            consumer2.RegisterConsumer("queue", callback: OnRecieveMessage);

            Producer producer = new Producer("localhost", "guest", "guest");

            producer.DeclareQueue("queue"); // If not created, will create the queue, not needed

            foreach(var i in Enumerable.Range(0, 10))
            {
                producer.QueueMessage(i.ToString(), routingKey: "queue"); // Produce a message to the queue
            }

            Console.ReadLine(); // Block the end of the program
        }
    }
}
