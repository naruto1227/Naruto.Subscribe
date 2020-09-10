using Naruto.Subscribe.Extension;
using Naruto.Subscribe.Provider.RabbitMQ.Object;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Naruto.Subscribe.Provider.RabbitMQ
{
    public static class PublishMessageExtension
    {

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="msg">消息内容</param>
        /// <param name="subscribeName">路由key</param>
        /// <param name="channel"></param>
        public static void PublishMessage(this IModel channel, object msg, string subscribeName)
        {
            // 构建byte消息数据包
            string message = msg != null ? msg.ToJsonString() : "";
            var body = Encoding.UTF8.GetBytes(message);
            // 发送数据包
            channel.BasicPublish(exchange: RabbitMQOption.ExchangeName, routingKey: subscribeName, basicProperties: null, body: body);
        }
    }
}
