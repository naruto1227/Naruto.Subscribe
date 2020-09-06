using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Naruto.Subscribe.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Naruto.Subscribe.Provider.Redis
{
    public static class RedisSubscribeServiceExtension
    {

        /// <summary>
        /// 注入redis订阅服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRedisSubscribe(this IServiceCollection services)
        {
            services.TryAddSingleton<ISubscribeEvent, RedisSubscribeEvent>();
            services.TryAddSingleton<INarutoPublish, RedisPublishProvider>();
            return services;
        }
    }
}
