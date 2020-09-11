using System;
using System.Collections.Generic;
using System.Text;

namespace Naruto.Subscribe.Provider.RabbitMQ.Object
{
    /// <summary>
    /// 交换机类型
    /// </summary>
    public class NarutoExchangeType
    {
        /// <summary>
        /// 所有bind到此exchange的queue都可以接收消息， 无需匹配routing key
        /// </summary>

        public const string Fanout = "fanout";
        /// <summary>
        /// 它会把消息路由到那些exchange与routing key完全匹配的Queue中
        /// </summary>
        public const string Direct = "direct";
        /// <summary>
        /// 所有符合routingKey(此时可以是一个表达式)的routingKey所bind的queue可以接收消息
        /// </summary>

        public const string Topic = "topic";

        /// <summary>
        /// 通过headers 来决定把消息发给哪些queue
        /// </summary>
        public const string Headers = "headers";
    }
}
