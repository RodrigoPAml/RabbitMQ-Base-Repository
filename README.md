# RabbitMQ-Base-Repository

RabbitMQ is an open-source message broker software that implements the Advanced Message Queuing Protocol (AMQP). It provides a robust and scalable messaging system for applications to communicate and exchange data in a distributed architecture.

Reference: https://www.rabbitmq.com/getstarted.html

# Exchange types

RabbitMQ supports several types of exchanges that determine how messages are routed to queues. Here's an overview of the different exchange types:

- Direct Exchange: A direct exchange routes messages to queues based on an exact match between the routing key of the message and the binding key of the queue. It is a simple, one-to-one routing mechanism. When a message is published with a specific routing key, RabbitMQ delivers it to the queue(s) that are bound with the same routing key.

- Fanout Exchange: A fanout exchange broadcasts messages to all the queues that are bound to it. It ignores the routing key and delivers the message to every bound queue. Fanout exchanges are useful for scenarios where the same message needs to be sent to multiple consumers or where message broadcasting is required.

- Topic Exchange: A topic exchange routes messages based on patterns in the routing keys. The routing key is a string with multiple words separated by dots. Queues are bound to the topic exchange with binding keys that define patterns. The topic exchange uses wildcard characters (* and #) in the binding keys for matching. It allows for flexible message routing based on specific patterns and criteria.

  Just like routing keys but work with patterns
  The topic exchange supports two types of pattern characters:

  - "." (star): Matches exactly one word in the routing key. For example, "user." would match "user.create" or "user.update".
  - "#" (hash): Matches zero or more words in the routing key. It must be placed at the end of the routing key. For example, "order.#" would match "order" or "order.update.status".

- Headers Exchange: A headers exchange uses message headers as the routing criteria instead of routing keys. The exchange examines the headers of the message and matches them against the headers specified in the bindings. It allows for complex matching based on multiple header attributes. Headers exchanges are less commonly used compared to the other exchange types.

- Default Exchange: RabbitMQ has a default exchange, which is a direct exchange with no name. When a queue is declared with no explicit binding to any exchange, it automatically binds to the default exchange using the queue name as the routing key. Messages published to the default exchange with the corresponding routing key will be routed to the bound queue.

# Some Code Examples
## Single Consumer and Producer (SingleConsumer.cs)

Can be used for in serie processing

![image](https://github.com/RodrigoPAml/RabbitMQ-Base-Repository/assets/41243039/c36d3e58-ccd5-4150-848d-0ba76f1913ff)

## Producer and two consumer competing in queue (ManyConsumers.cs)

Can be used for load balance 

![image](https://github.com/RodrigoPAml/RabbitMQ-Base-Repository/assets/41243039/7006238a-aeb3-48b7-8bca-a701268297b2)

## Produce to one or more queues (Exchange.cs)

Used to distribute messages between diferent queues based on exchange 

![image](https://github.com/RodrigoPAml/RabbitMQ-Base-Repository/assets/41243039/3b7bfff8-dfb0-4222-89f2-6d6a1cd4d810)

## Produce to one or more queues based on routing key (Routing.cs)

Used to distribute messages between diferent queues based on routing queue 

![image](https://github.com/RodrigoPAml/RabbitMQ-Base-Repository/assets/41243039/8bd24d9b-aca0-4fe5-8713-edb2ef457a2a)

