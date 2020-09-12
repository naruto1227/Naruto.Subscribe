using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Naruto.Subscribe.Interface
{
    /// <summary>
    /// 缓存订阅的方法
    /// </summary>
    public interface IMethodCache : IDisposable
    {
        /// <summary>
        /// 获取方法
        /// </summary>
        /// <param name="service"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        (MethodInfo method, ParameterInfo[] parameterInfos) Get(Type service, string action);
    }
}
