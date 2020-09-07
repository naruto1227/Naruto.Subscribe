using System;
using System.Collections.Generic;
using System.Text;

namespace Naruto.Subscribe.Provider.RabbitMQ.Object
{
    /// <summary>
    /// rbbitmq的配置信息
    /// </summary>
    public class NarutoRabbitMQOption
    {
        /// <summary>
        /// 主机信息
        /// </summary>
        public List<string> HostNames { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; } = "guest";
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = "guest";
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; } = 5672;
    }
}
