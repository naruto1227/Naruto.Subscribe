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
            services.AddMQServices();
            services.Configure<NarutoRabbitMQOption>(option);
            return services;
        }

        /// <summary>
        /// 注入RabbitMQ订阅服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRabbitMQSubscribe(this IServiceCollection services, Action<NarutoRabbitMQOption> option)
        {
            services.TryAddSingleton<ISubscribeEvent, RabbitMQSubscribeEvent>();
            services.AddMQServices();
            services.Configure(option);
            return services;
        }

        /// 注入RabbitMQ发布服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRabbitMQPublishServices(this IServiceCollection services)
        {
            services.TryAddSingleton<INarutoPublish, RabbitMQPublishProvider>();
            services.AddMQServices();
            return services;
        }
        /// <summary>
        /// 注入RabbitMQ发布服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRabbitMQPublishServices(this IServiceCollection services, IConfiguration option)
        {
            services.TryAddSingleton<INarutoPublish, RabbitMQPublishProvider>();
            services.AddMQServices();
            services.Configure<NarutoRabbitMQOption>(option);
            return services;
        }

        /// <summary>
        /// 注入RabbitMQ发布服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRabbitMQPublishServices(this IServiceCollection services, Action<NarutoRabbitMQOption> option)
        {
            services.TryAddSingleton<INarutoPublish, RabbitMQPublishProvider>();
            services.AddMQServices();
            services.Configure(option);
            return services;
        }
        private static void AddMQServices(this IServiceCollection services)
        {
            services.TryAddSingleton<INarutoConnectionFactory, NarutoConnectionFactory>();
            services.TryAddSingleton<INarutoChannelFactory, NarutoChannelFactory>();
        }
    }
}
