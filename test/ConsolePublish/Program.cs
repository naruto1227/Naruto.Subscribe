using RabbitMQ.Client;
using System;
using System.Text;

namespace ConsolePublish
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("开始发送消息");

            var factory = new ConnectionFactory()
            {
                HostName = ""
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.ExchangeDeclare("topic_logs", ExchangeType.Topic, durable: true, autoDelete: false, arguments: null);

            // channel.ExchangeDeclare("direct_logs", ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
            //channel.QueueDeclare(queue: "hello", durable: true, exclusive: false, autoDelete: false, arguments: null);
            for (int i = 0; i < 10; i++)
            {
                channel.BasicPublish(exchange: "topic_logs", routingKey: "testLog.asd", null, Encoding.UTF8.GetBytes($"第{i + 1}条消息"));
            }

            Console.WriteLine("回车退出");

            Console.ReadKey();
        }
    }
}
