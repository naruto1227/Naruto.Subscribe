using Naruto.Subscribe.Extension;
using Naruto.Subscribe.Interface;
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
    public class MethodCache : IMethodCache
    {
        /// <summary>
        /// 缓存方法的信息
        /// </summary>
        private readonly ConcurrentDictionary<string, (MethodInfo method, ParameterInfo[] parameterInfos)> methods;

        public MethodCache()
        {
            methods = new ConcurrentDictionary<string, (MethodInfo method, ParameterInfo[] parameterInfos)>();
        }

        public void Dispose()
        {
            methods?.Clear();
        }

        /// <summary>
        /// 获取方法信息
        /// </summary>
        /// <param name="service"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public (MethodInfo method, ParameterInfo[] parameterInfos) Get(Type service, string action)
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
                return default;
            }
            methods.TryAdd(key, (methodInfo, methodInfo.GetParameters()));
            return (methodInfo, methodInfo.GetParameters());
        }
    }
}
