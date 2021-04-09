using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ExploreCalifornia.EmailService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Email Service ==="); 

            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://lwvhlrgx:6zZG9KUbnXjtyQsT66eGsw9SWLRyH0Kt@hornet.rmq.cloudamqp.com/lwvhlrgx");
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            // declare resources here, handle consumed events, etc
            channel.QueueDeclare("emailServiceQueue", true, false, false);  // Name, Durable, Exclusive, Auto-delete
            channel.QueueBind("emailServiceQueue", "webappExchange", "");   // Queue name, Exchange name, Routing key

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, eventArgs) =>
            {
                var msg = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
                Console.WriteLine("\nFanout Queue");
                Console.WriteLine(msg); 
            };

            //var channelDirect = connection.CreateModel();

            #region Additional Queues
            // Direct Queue
            channel.QueueDeclare("emailServiceQueueDirect", true, false, false);
            channel.QueueBind("emailServiceQueueDirect", "webappExchangeDirect", "ExploreCalifornia.EmailService");

            var consumerDirect = new EventingBasicConsumer(channel);
            consumerDirect.Received += (sender, eventArgs) =>
            {
                var msg = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
                Console.WriteLine("\nDirect Queue");
                Console.WriteLine($"{eventArgs.RoutingKey}: {msg}");
            };

            // Topic Queue
            channel.QueueDeclare("emailServiceQueueTopic", true, false, false);
            channel.QueueBind("emailServiceQueueTopic", "webappExchangeTopic", "tour.booked");

            var consumerTopic = new EventingBasicConsumer(channel);
            consumerTopic.Received += (sender, eventArgs) =>
            {
                var msg = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
                Console.WriteLine("\nTopic Queue");
                Console.WriteLine($"{eventArgs.RoutingKey}: {msg}");
            };

            // Headers Queue
            channel.QueueDeclare("emailServiceQueueHeaders", true, false, false);
            var headers = new Dictionary<String, object>
            {
                { "subject", "tour" },
                { "action", "booked" },
                { "x-match", "all" }
            };
            channel.QueueBind("emailServiceQueueHeaders", "webappExchangeHeaders", "", headers);

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


            channel.BasicConsume("emailServiceQueue", true, consumer);
            channel.BasicConsume("emailServiceQueueDirect", true, consumerDirect);
            channel.BasicConsume("emailServiceQueueTopic", true, consumerTopic);
            channel.BasicConsume("emailServiceQueueHeaders", true, consumerHeaders);
            Console.ReadLine();

            channel.Close();
            connection.Close();

        }
    }
}
