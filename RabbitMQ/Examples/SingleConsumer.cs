using Messageria;

namespace Examples
{
    public static class SingleConsumer
    {
        private static void OnRecieveMessage(int id, string queue, string message, ulong tag)
        {
            Console.WriteLine($"Message '{message}' recieved to consumer '{id}' of queue '{queue}' with delivery tag '{tag}'");
            Thread.Sleep(1000);
        }

        /// <summary>
        /// A consumer and queue with a producer
        /// </summary>
        public static void Execute()
        {
            Consumer consumer = new Consumer("localhost", "guest", "guest"); // Connection

            consumer.DeclareQueue("queue"); // Create queue, this method is idempotent
            consumer.RegisterConsumer("queue", callback: OnRecieveMessage); // Register as a consumer of this queue with callback

            Producer producer = new Producer("localhost", "guest", "guest");

            producer.DeclareQueue("queue"); // Not needed but if not created, will create the queue
            producer.QueueMessage("Hello", routingKey: "queue"); // Produce a message to the queue

            Console.ReadLine(); // Block the end of the program
        }
    }
}
