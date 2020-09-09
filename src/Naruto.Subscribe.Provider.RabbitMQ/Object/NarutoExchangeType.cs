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
        /// 把所有发送到该Exchange的消息路由到所有与它绑定的Queue中。
        /// </summary>

        public const string Fanout = "fanout";
        /// <summary>
        /// 它会把消息路由到那些binding key与routing key完全匹配的Queue中
        /// </summary>
        public const string Direct = "direct";
        /// <summary>
        /// 可以通过通配符满足一部分规则就可以传送
        /// </summary>

        public const string Topic = "topic";

        public const string Headers = "headers";
    }
}
