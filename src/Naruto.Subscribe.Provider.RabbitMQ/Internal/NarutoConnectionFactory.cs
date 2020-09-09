using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Naruto.Subscribe.Provider.RabbitMQ.Interface;
using Naruto.Subscribe.Provider.RabbitMQ.Object;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Naruto.Subscribe.Provider.RabbitMQ.Internal
{
    /// <summary>
    /// 连接工厂的实现
    /// </summary>
    internal class NarutoConnectionFactory : INarutoConnectionFactory
    {
        private readonly ILogger logger;
        /// <summary>
        /// 连接工厂对象
        /// </summary>

        public readonly ConnectionFactory connectionFactory;

        /// <summary>
        /// 连接信息
        /// </summary>
        private IConnection connection;

        /// <summary>
        /// 配置信息
        /// </summary>
        private readonly IOptionsMonitor<NarutoRabbitMQOption> optionsMonitor;

        public NarutoConnectionFactory(ILogger<NarutoConnectionFactory> _logger, IOptionsMonitor<NarutoRabbitMQOption> _optionsMonitor)
        {
            logger = _logger;
            optionsMonitor = _optionsMonitor;
            //配置工厂信息
            connectionFactory = new ConnectionFactory()
            {
                Password = optionsMonitor.CurrentValue.Password, //密码
                UserName = optionsMonitor.CurrentValue.UserName,//用户名
                Port = optionsMonitor.CurrentValue.Port,
                VirtualHost = "/",//虚拟主机
            };
            if (optionsMonitor.CurrentValue.HostNames.Count == 1)
            {
                connectionFactory.HostName = optionsMonitor.CurrentValue.HostNames.FirstOrDefault();
            }
        }

        public IConnection Get()
        {
            return CreateConnection();
        }

        public Task<IConnection> GetAsync()
        {
            TaskCompletionSource<IConnection> taskCompletionSource = new TaskCompletionSource<IConnection>();
            try
            {
                CreateConnection();
                taskCompletionSource.SetResult(connection);
            }
            catch (Exception ex)
            {
                taskCompletionSource.SetException(ex);
            }
            return taskCompletionSource.Task;
        }
        /// <summary>
        /// 创建连接
        /// </summary>
        private IConnection CreateConnection()
        {
            logger.LogInformation("获取连接");
            if (connection == null)
            {
                lock (this)
                {
                    var serviceName = Assembly.GetEntryAssembly()?.GetName().Name.ToLower();
                    if (optionsMonitor.CurrentValue.HostNames.Count > 1)
                    {
                        connection = connectionFactory.CreateConnection(optionsMonitor.CurrentValue.HostNames, serviceName);
                    }
                    else
                    {
                        connection = connectionFactory.CreateConnection(serviceName);
                    }
                }
            }
            logger.LogInformation($"{(connection == null ? "获取连接失败" : "获取连接成功")}");
            return connection;
        }

        public void Dispose()
        {
            connection?.Close();
            connection?.Dispose();
        }
    }
}
