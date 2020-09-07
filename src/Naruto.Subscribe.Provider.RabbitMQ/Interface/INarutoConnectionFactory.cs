using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Naruto.Subscribe.Provider.RabbitMQ.Interface
{
    /// <summary>
    /// 张海波
    /// 2020-09-07
    /// rabbitmq的连接工厂
    /// </summary>
    internal interface INarutoConnectionFactory : IDisposable
    {
        /// <summary>
        /// 获取连接
        /// </summary>
        /// <returns></returns>
        IConnection Get();

        /// <summary>
        /// 异步获取
        /// </summary>
        /// <returns></returns>
        Task<IConnection> GetAsync();
    }
}
