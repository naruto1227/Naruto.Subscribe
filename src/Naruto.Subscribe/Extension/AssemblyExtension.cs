using Naruto.Subscribe.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Naruto.Subscribe.Extension
{
    public static class AssemblyExtension
    {
        /// <summary>
        /// 从程序集中获取特性的信息
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static List<SubscribeTypeModel> GetSubscribe(this Assembly assembly)
        {
            //获取程序集中所有继承了 ISubscribe的对象
            var types = assembly.GetTypes().Where(a => a.GetInterface(typeof(ISubscribe).Name) != null && a.IsClass && !a.IsAbstract && !a.IsInterface && a.IsPublic).ToList();
            if (types == null || types.Count <= 0)
            {
                return default;
            }
            //实例化返回的信息
            var subscribeTypeModels = new List<SubscribeTypeModel>();
            foreach (var itemClassType in types)
            {
                foreach (var itemMethod in itemClassType.GetMethods().Where(a => a.IsPublic && a.IsDefined(typeof(SubscribeAttribute))))
                {
                    subscribeTypeModels.Add(new SubscribeTypeModel
                    {
                        ServiceType = itemClassType,
                        MethodName = itemMethod.Name,
                        SubscribeName = itemMethod.GetCustomAttribute<SubscribeAttribute>().Name
                    });
                }
            }
            return subscribeTypeModels;
        }
    }
}
