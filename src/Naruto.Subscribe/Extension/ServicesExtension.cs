using Naruto.Subscribe.Extension;
using Naruto.Subscribe.Interface;
using Naruto.Subscribe.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServicesExtension
    {
        /// <summary>
        /// 添加 订阅的服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSubscribeServices(this IServiceCollection services, params Assembly[] assemblies)
        {
            //注入服务
            // services.AddSingleton<ISubscribeTypeFactory, SubscribeTypeFactory>();
            services.AddSingleton(typeof(DynamicMethodExpression<>));
            services.AddSingleton<ISubscribeHandler, SubscribeHandler>();
            //设置订阅信息
            services.SetSubscribe(assemblies);
            return services;
        }

        /// <summary>
        /// 设置订阅信息
        /// </summary>
        private static void SetSubscribe(this IServiceCollection services, params Assembly[] assemblies)
        {
            //ISubscribeTypeFactory subscribeTypeFactory = services.BuildServiceProvider().GetRequiredService<ISubscribeTypeFactory>();
            if (assemblies != null && assemblies.Count() > 0)
            {
                foreach (var item in assemblies)
                {
                    //获取需要订阅的信息
                    var list = item.GetSubscribe();
                    if (list != null && list.Count() > 0)
                    {
                        foreach (var subscribeType in list)
                        {
                            //将订阅的信息 存储到工厂
                            SubscribeTypeFactory.Set(subscribeType);
                            //注入服务
                            services.AddSingleton(subscribeType.ServiceType);
                        }
                    }
                }
            }
        }
    }
}
