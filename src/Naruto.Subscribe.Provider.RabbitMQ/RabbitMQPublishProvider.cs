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
    /// <summary>
    /// rabbitmq 生产者
    /// </summary>
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
            using var channel = narutoChannel.Get();
            channel.PublishMessage(msg, subscribeName);
        }

        public async Task PublishAsync(string subscribeName, object msg = null)
        {
            //创建一个信道
            using var channel = await narutoChannel.GetAsync();
            channel.PublishMessage(msg, subscribeName);
        }
    }
}
