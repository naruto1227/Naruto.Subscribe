using Naruto.Subscribe.Extension;
using Naruto.Subscribe.Object;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Naruto.Subscribe.Internal
{
    /// <summary>
    /// 方法的缓存
    /// </summary>
    public class MethodCache
    {
        /// <summary>
        /// 缓存方法的信息
        /// </summary>
        private static readonly ConcurrentDictionary<string, (MethodInfo method, ParameterInfo[] parameterInfos)> methods = new ConcurrentDictionary<string, (MethodInfo method, ParameterInfo[] parameterInfos)>();

        /// <summary>
        /// 获取方法信息
        /// </summary>
        /// <param name="service"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static (MethodInfo method, ParameterInfo[] parameterInfos) Get(Type service, string action)
        {
            service.CheckNull();
            action.CheckNullOrEmpty();
            var key = service.Name + action;
            if (methods.TryGetValue(key, out var method))
            {
                return method;
            }
            //获取方法
            var methodInfo = service.GetMethod(action, BindingFlags.Public | BindingFlags.Instance);
            if (methodInfo == null)
            {
                throw new NotMethodException($"查找不到服务{service.Name}中的{action}方法");
            }
            methods.TryAdd(key, (methodInfo, methodInfo.GetParameters()));
            return (methodInfo, methodInfo.GetParameters());
        }
    }
}
