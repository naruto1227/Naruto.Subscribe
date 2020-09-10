using Microsoft.Extensions.Logging;
using Naruto.Subscribe.Extension;
using Naruto.Subscribe.Interface;
using Naruto.Subscribe.Provider.RabbitMQ.Interface;
using Naruto.Subscribe.Provider.RabbitMQ.Object;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Naruto.Subscribe.Provider.RabbitMQ
{
    public class RabbitMQPublishProvider : INarutoPublish
    {
        private readonly ILogger logger;

        private readonly INarutoChannelFactory narutoChannel;

        public RabbitMQPublishProvider(ILogger<RabbitMQPublishProvider> _logger, INarutoChannelFactory _narutoChannel)
        {
            narutoChannel = _narutoChannel;
            logger = _logger;
        }
        public void Publish(string subscribeName, object msg = null)
        {
            //创建一个信道
            var channel = narutoChannel.Get();

            //绑定交换机
            channel.ExchangeDeclare(exchange: RabbitMQOption.ExchangeName, type: ExchangeType.Topic, durable: true, autoDelete: false, arguments: null);
            //绑定存储的消息队列
            channel.QueueDeclare(queue: RabbitMQOption.QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            // 构建byte消息数据包
            string message = msg != null ? msg.ToJsonString() : "";
            var body = Encoding.UTF8.GetBytes(message);
            // 发送数据包
            channel.BasicPublish(exchange: RabbitMQOption.ExchangeName, routingKey: subscribeName, basicProperties: null, body: body);
        }

        public async Task PublishAsync(string subscribeName, object msg = null)
        {
            //创建一个信道
            var channel = await narutoChannel.GetAsync();
            //绑定交换机
            channel.ExchangeDeclare(exchange: RabbitMQOption.ExchangeName, type: ExchangeType.Topic, durable: true, autoDelete: false, arguments: null);
            //绑定存储的消息队列
            channel.QueueDeclare(queue: RabbitMQOption.QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            // 构建byte消息数据包
            string message = msg != null ? msg.ToJsonString() : "";
            var body = Encoding.UTF8.GetBytes(message);
            // 发送数据包
            channel.BasicPublish(exchange: RabbitMQOption.ExchangeName, routingKey: subscribeName, basicProperties: null, body: body);
        }
    }
}
