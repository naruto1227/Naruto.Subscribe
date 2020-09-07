using Microsoft.Extensions.Logging;
using Naruto.Subscribe.Interface;
using Naruto.Subscribe.Provider.RabbitMQ.Interface;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Naruto.Subscribe.Provider.RabbitMQ
{
    public class RabbitMQSubscribeEvent : ISubscribeEvent
    {
        private readonly ILogger logger;

        private readonly INarutoChannelFactory narutoChannelFactory;

        public RabbitMQSubscribeEvent(INarutoChannelFactory _narutoChannelFactory, ILogger<RabbitMQSubscribeEvent> _logger)
        {
            narutoChannelFactory = _narutoChannelFactory;
            logger = _logger;
        }
        public async Task SubscribeAsync(string subscribeName)
        {
            //创建一个信道
            var channel = await narutoChannelFactory.GetAsync();

            channel.QueueDeclare(queue: subscribeName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            // 构造消费者实例
            var consumer = new EventingBasicConsumer(channel);
            // 绑定消息接收后的事件委托
            consumer.Received += (model, ea) =>
            {
                logger.LogInformation("开始接收消息");
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                Console.WriteLine(" [x] Received {0}", message);
                Console.WriteLine(" [x] Done");
            };
            // 启动消费者
            channel.BasicConsume(queue: subscribeName, autoAck: true, consumer: consumer);
        }
    }
}
