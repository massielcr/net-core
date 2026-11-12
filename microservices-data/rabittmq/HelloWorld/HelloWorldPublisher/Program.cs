using RabbitMQ.AMQP.Client;
using RabbitMQ.AMQP.Client.Impl;
using System.Text;

Console.WriteLine("Sending...");

const string brokerUri = "amqp://guest:guest@localhost:5672/%2f";

ConnectionSettings settings = ConnectionSettingsBuilder.Create()
    .Uri(new Uri(brokerUri))
    .ContainerId("tutorial-send")
    .Build();

IEnvironment environment = AmqpEnvironment.Create(settings);
IConnection connection = await environment.CreateConnectionAsync();

try
{
    IManagement management = connection.Management();
    IQueueSpecification queueSpecification = management.Queue("hello").Type(QueueType.QUORUM);
    await queueSpecification.DeclareAsync();

    IPublisher publisher = await connection.PublisherBuilder().Queue("hello").BuildAsync();

    try
    {
        const string body = "Hello World!";
        var message = new AmqpMessage(Encoding.UTF8.GetBytes(body));
        PublishResult publishResult = await publisher.PublishAsync(message);
        if (publishResult.Outcome.State != OutcomeState.Accepted)
        {
            Console.Error.WriteLine($"Unexpected publish outcome: {publishResult.Outcome.State}");
        }

        Console.WriteLine($"[x] Sent {body}");
    }
    finally
    {
        await publisher.CloseAsync();
    }
}
finally
{
    await connection.CloseAsync();
    await environment.CloseAsync();
}

Console.ReadLine();