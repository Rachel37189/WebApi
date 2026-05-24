using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var config = new ConsumerConfig
{
    BootstrapServers =
        configuration["Kafka:BootstrapServers"],

    GroupId = "orders-consumer-group",

    AutoOffsetReset = AutoOffsetReset.Earliest
};

var topic = configuration["Kafka:Topic"];

using var consumer =
    new ConsumerBuilder<Ignore, string>(config).Build();

consumer.Subscribe(topic);

Console.WriteLine($"Listening to topic: {topic}");

while (true)
{
    try
    {
        var consumeResult = consumer.Consume();

        Console.WriteLine(
            $"Message received: {consumeResult.Message.Value}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}