using System;
using System.Collections.Generic;
using System.Text;

namespace Naruto.Subscribe.Provider.RabbitMQ.Object
{
    public class RabbitMQOption
    {
        /// <summary>
        /// 使用的交换机名称
        /// </summary>
        public const string ExchangeName = "exchange.naruto.direct";

        /// <summary>
        /// 使用的队列名称
        /// </summary>
        public const string QueueName = "queue.naruto";
    }
}
