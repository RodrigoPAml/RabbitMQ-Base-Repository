# RabbitMQ-Base-Repository

RabbitMQ is an open-source message broker software that implements the Advanced Message Queuing Protocol (AMQP). It provides a robust and scalable messaging system for applications to communicate and exchange data in a distributed architecture.

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

![image](https://github.com/RodrigoPAml/RabbitMQ-Base-Repository/assets/41243039/6892fb91-43e6-495f-af55-cdd3a352aed3)

## Producer and two consumer competing in queue (ManyConsumers.cs)

Can de used for load balance 

![image](https://github.com/RodrigoPAml/RabbitMQ-Base-Repository/assets/41243039/d29b8a4b-628a-4e9b-bd01-7512b0d03519)

## Produce to one or more queues (Exchange.cs)

Used to distribute messages between diferent queues based on exchange 

![1](https://github.com/RodrigoPAml/RabbitMQ-Base-Repository/assets/41243039/1dbb5f6a-d2f9-4659-b646-0ae56e875933)

## Produce to one or more queues based on routing key (Routing.cs)

Used to distribute messages between diferent queues based on routing queue 

![Sem título](https://github.com/RodrigoPAml/RabbitMQ-Base-Repository/assets/41243039/f394112e-bf05-482e-be8b-e10acd1c07a1)
