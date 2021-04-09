using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace ExploreCalifornia.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        [HttpPost]
        [Route("Book")]
        public IActionResult Book()
        {
            #region Headers
            var headers = new Dictionary<String, object>()
            {
                { "subject", "tour" },
                { "action", "booked" }
            };
            #endregion

            var tourname = Request.Form["tourname"];
            var name = Request.Form["name"];
            var email = Request.Form["email"];
            var needsTransport = Request.Form["transport"] == "on";

            var exchangeType = Request.Form["exchangeSelect"];

            // compare name to demonstrate each type of exchange
            if (exchangeType.Equals("fanout"))
            {
                Fanout(tourname, name, email);
            }
            else if (exchangeType.Equals("direct"))
            {
                Direct(tourname, name, email);
            }
            else if (exchangeType.Equals("topic"))
            {
                Topic(tourname, name, email, "", "tour.booked");
            }
            else if (exchangeType.Equals("headers"))
            {
                Headers(tourname, name, email, headers);
            }

            return Redirect($"/BookingConfirmed?tourname={tourname}&name={name}&email={email}");
        }

        [HttpPost]
        [Route("Cancel")]
        public IActionResult Cancel()
        {
            #region
            var headers = new Dictionary<String, object>()
            {
                { "subject", "tour" },
                { "action", "cancelled" }
            };
            #endregion

            var tourname = Request.Form["tourname"];
            var name = Request.Form["name"];
            var email = Request.Form["email"];
            var cancelReason = Request.Form["reason"];

            var exchangeType = Request.Form["exchangeSelect"];

            // Send cancel message here
            if (exchangeType.Equals("topic"))
            {
                Topic(tourname, name, email, cancelReason, "tour.cancelled");
            }
            else if (exchangeType.Equals("headers"))
            {
                Headers(tourname, name, email, headers);
            }

            return Redirect($"/BookingCanceled?tourname={tourname}&name={name}");
        }

        public void Fanout(string tourname, string name, string email)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://lwvhlrgx:6zZG9KUbnXjtyQsT66eGsw9SWLRyH0Kt@hornet.rmq.cloudamqp.com/lwvhlrgx");

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.ExchangeDeclare("webappExchange", ExchangeType.Fanout, true);

            var message = $"{tourname};{name};{email}";
            var bytes = System.Text.Encoding.UTF8.GetBytes(message);
            channel.BasicPublish("webappExchange", "", null, bytes);

            channel.Close();
            connection.Close();
        }

        public void Direct(string tourname, string name, string email)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://lwvhlrgx:6zZG9KUbnXjtyQsT66eGsw9SWLRyH0Kt@hornet.rmq.cloudamqp.com/lwvhlrgx");

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.ExchangeDeclare("webappExchangeDirect", ExchangeType.Direct, true);

            var message = $"{tourname};{name};{email}";
            var bytes = System.Text.Encoding.UTF8.GetBytes(message);
            channel.BasicPublish("webappExchangeDirect", "ExploreCalifornia.EmailService", null, bytes);

            channel.Close();
            connection.Close();
        }

        public void Topic(string tourname, string name, string email, string reason, string key)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://lwvhlrgx:6zZG9KUbnXjtyQsT66eGsw9SWLRyH0Kt@hornet.rmq.cloudamqp.com/lwvhlrgx");

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.ExchangeDeclare("webappExchangeTopic", ExchangeType.Topic, true);

            var message = $"{tourname};{name};{email};{reason}";
            var bytes = System.Text.Encoding.UTF8.GetBytes(message);
            channel.BasicPublish("webappExchangeTopic", key, null, bytes);

            channel.Close();
            connection.Close();
        }

        public void Headers(string tourname, string name, string email, IDictionary<String, object> headers)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://lwvhlrgx:6zZG9KUbnXjtyQsT66eGsw9SWLRyH0Kt@hornet.rmq.cloudamqp.com/lwvhlrgx");

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.ExchangeDeclare("webappExchangeHeaders", ExchangeType.Headers, true);

            var message = $"{tourname};{name};{email}";
            var bytes = System.Text.Encoding.UTF8.GetBytes(message);
            
            var properties = channel.CreateBasicProperties();
            properties.Headers = headers;

            channel.BasicPublish("webappExchangeHeaders", "", properties, bytes);

            channel.Close();
            connection.Close();
        }
    }
}