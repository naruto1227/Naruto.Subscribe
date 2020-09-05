using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Naruto.Redis;
using Naruto.Subscribe.Interface;
using Naruto.Subscribe.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naruto.Subscribe.Extension
{
    public static class ApplicationBuilderExtension
    {
        /// <summary>
        /// 启用 订阅
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <returns></returns>
        public static async Task EnableSubscribe(this IApplicationBuilder applicationBuilder)
        {
            //获取订阅类型的工厂
            //var typeFactory = applicationBuilder.ApplicationServices.GetRequiredService<ISubscribeTypeFactory>();
            //获取所有需要订阅的名称
            var names = SubscribeTypeFactory.GetAllSubscribeName();

            if (names != null && names.Count() > 0)
            {
                //获取redis
                var redis = applicationBuilder.ApplicationServices.GetRequiredService<IRedisRepository>();
                //订阅处理接口
                var subscribeHandler = applicationBuilder.ApplicationServices.GetRequiredService<ISubscribeHandler>();
                foreach (var item in names)
                {
                    await redis.Subscribe.SubscribeAsync(item, async (channel, msg) =>
                     {
                         await subscribeHandler.Handler(channel, msg);
                     });
                }
            }
        }
    }

}
