using Naruto.Subscribe.Interface;
using Naruto.Subscribe.Object;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naruto.Subscribe.Internal
{
    public static class SubscribeTypeFactory
    {
        private static readonly ConcurrentDictionary<string, BaseSubscribeTypeModel> data;

        static SubscribeTypeFactory()
        {
            data = new ConcurrentDictionary<string, BaseSubscribeTypeModel>();
        }

        /// <summary>
        /// 获取订阅的名称 所在的 对象的类型
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static BaseSubscribeTypeModel Get(string name)
        {
            var result = data.TryGetValue(name, out var type);
            if (!result)
            {
                throw new InvalidOperationException("无法匹配当前订阅的信息");
            }
            return type;
        }

        /// <summary>
        /// 获取所有的订阅名称
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllSubscribeName()
        {
            return data.Select(a => a.Key).ToList();
        }
        /// <summary>
        /// 设置存储的订阅类型对应的订阅名称
        /// </summary>
        /// <param name="name"></param>
        /// 
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public static bool Set(SubscribeTypeModel subscribe)
        {
            if (data.Any(a => a.Key == subscribe.SubscribeName))
            {
                throw new InvalidOperationException("存在重复的订阅信息");
            }
            return data.TryAdd(subscribe.SubscribeName, new BaseSubscribeTypeModel
            {
                MethodName = subscribe.MethodName,
                ServiceType = subscribe.ServiceType
            });
        }
    }
}
