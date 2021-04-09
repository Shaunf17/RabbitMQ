using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ExploreCalifornia.BackOffice
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Back Office ===");

            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://lwvhlrgx:6zZG9KUbnXjtyQsT66eGsw9SWLRyH0Kt@hornet.rmq.cloudamqp.com/lwvhlrgx");
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            // Fanout Queue
            channel.QueueDeclare("backOfficeQueue", true, false, false);
            channel.QueueBind("backOfficeQueue", "webappExchange", "");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, eventArgs) =>
            {
                var msg = Encoding.UTF8.GetString(eventArgs.Body);
                Console.WriteLine("\nFanout Queue");
                Console.WriteLine(msg);
            };

            #region Additional Queues
            // Direct Queue
            channel.QueueDeclare("backOfficeQueueDirect", true, false, false);
            channel.QueueBind("backOfficeQueueDirect", "webappExchangeDirect", "ExploreCalifornia.BackOffice");

            var consumerDirect = new EventingBasicConsumer(channel);
            consumerDirect.Received += (sender, eventArgs) =>
            {
                var msg = System.Text.Encoding.UTF8.GetString(eventArgs.Body.ToArray());
                Console.WriteLine("\nDirect Queue");
                Console.WriteLine($"{eventArgs.RoutingKey}: {msg}");
            };

            // Topic Queue
            channel.QueueDeclare("backOfficeQueueTopic", true, false, false);
            channel.QueueBind("backOfficeQueueTopic", "webappExchangeTopic", "tour.*");

            var consumerTopic = new EventingBasicConsumer(channel);
            consumerTopic.Received += (sender, eventArgs) =>
            {
                var msg = Encoding.UTF8.GetString(eventArgs.Body);
                Console.WriteLine("\nTopic Queue");
                Console.WriteLine($"{eventArgs.RoutingKey}: {msg}");
            };

            // Headers Queue
            channel.QueueDeclare("backOfficeQueueHeaders", true, false, false);
            var headers = new Dictionary<String, object>
            {
                { "subject", "tour" },
                { "action", "cancelled" },
                { "x-match", "all" }
            };
            channel.QueueBind("backOfficeQueueHeaders", "webappExchangeHeaders", "", headers);

            var consumerHeaders = new EventingBasicConsumer(channel);
            consumerHeaders.Received += (sender, eventArgs) =>
            {   
                var msg = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
                var subject = Encoding.UTF8.GetString(eventArgs.BasicProperties.Headers["subject"] as byte[]);
                var action = Encoding.UTF8.GetString(eventArgs.BasicProperties.Headers["action"] as byte[]);
                Console.WriteLine("\nHeaders Queue");
                Console.WriteLine($"{subject} {action} : {msg}");
            };
            #endregion

            channel.BasicConsume("backOfficeQueue", true, consumer);
            channel.BasicConsume("backOfficeQueueDirect", true, consumerDirect); 
            channel.BasicConsume("backOfficeQueueTopic", true, consumerTopic);
            channel.BasicConsume("backOfficeQueueHeaders", true, consumerHeaders);

            Console.ReadLine();

            channel.Close();
            connection.Close();
        }
    }
}
