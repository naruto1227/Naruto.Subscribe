using Microsoft.Extensions.Logging;
using Naruto.Subscribe.Interface;
using Naruto.Subscribe.Provider.RabbitMQ.Interface;
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
            throw new NotImplementedException();
        }

        public async Task PublishAsync(string subscribeName, object msg = null)
        {
            //创建一个信道
            var channel = await narutoChannel.GetAsync();

            channel.QueueDeclare(queue: subscribeName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            // 构建byte消息数据包
            string message = msg != null ? msg.ToString() : "";
            var body = Encoding.UTF8.GetBytes(message);
            logger.LogInformation("发送消息");
            // 发送数据包
            channel.BasicPublish(exchange: "fanout", routingKey: subscribeName, basicProperties: null, body: body);
        }
    }
}
