using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace ConsoleConsumers2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("监听队列消息");

            var factory = new ConnectionFactory()
            {
                HostName = ""
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {

                    //扇形交换机
                    //channel.ExchangeDeclare("logs", ExchangeType.Fanout, durable: true, autoDelete: false, arguments: null);
                    //channel.QueueDeclare(queue: "hello", durable: true, exclusive: false, autoDelete: false, arguments: null);

                    //直连交换机
                    channel.ExchangeDeclare("topic_logs", ExchangeType.Topic, durable: true, autoDelete: false, arguments: null);
                    //创建一个非持久化，的临时队列，并且自动删除
                    var queueName = channel.QueueDeclare().QueueName;

                    channel.QueueBind(queue: queueName, exchange: "topic_logs", routingKey: "testLog.*");
                    var Consumer = new EventingBasicConsumer(channel);
                    Consumer.Received += (obj, ea) =>
                    {
                        Console.WriteLine($"收到消息:{Encoding.UTF8.GetString(ea.Body.ToArray())}");
                        Thread.Sleep(5 * 1000);
                        channel.BasicAck(ea.DeliveryTag, false);
                    };
                    //每次允许一个操作
                    channel.BasicQos(0, 1, false);
                    channel.BasicConsume(queue: queueName, autoAck: false, Consumer);
                    Console.WriteLine("回车退出");

                    Console.ReadKey();
                }
            }
        }
    }
}
