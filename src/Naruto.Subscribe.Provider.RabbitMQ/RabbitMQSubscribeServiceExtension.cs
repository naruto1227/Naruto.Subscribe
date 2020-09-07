using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Naruto.Subscribe.Interface;
using Naruto.Subscribe.Provider.RabbitMQ.Interface;
using Naruto.Subscribe.Provider.RabbitMQ.Internal;
using Naruto.Subscribe.Provider.RabbitMQ.Object;
using System;

namespace Naruto.Subscribe.Provider.RabbitMQ
{
    /// <summary>
    /// rabbitmq的服务扩展
    /// </summary>
    public static class RabbitMQSubscribeServiceExtension
    {
        /// <summary>
        /// 注入RabbitMQ订阅服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRabbitMQSubscribe(this IServiceCollection services, IConfiguration option)
        {
            services.TryAddSingleton<ISubscribeEvent, RabbitMQSubscribeEvent>();
            services.TryAddSingleton<INarutoPublish, RabbitMQPublishProvider>();
            services.AddSingleton<INarutoConnectionFactory, NarutoConnectionFactory>();
            services.AddSingleton<INarutoChannelFactory, NarutoChannelFactory>();
            services.Configure<NarutoRabbitMQOption>(option);
            return services;
        }
    }
}
