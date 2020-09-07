using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Naruto.Subscribe.Extension;
using Naruto.Subscribe.Interface;
using Naruto.Subscribe.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Naruto.Subscribe.Internal
{
    public class SubscribeHandler : ISubscribeHandler
    {
        private readonly IServiceProvider serviceProvider;

        private readonly ILogger logger;
        public SubscribeHandler(IServiceProvider _serviceProvider, ILogger<SubscribeHandler> _logger)
        {
            serviceProvider = _serviceProvider;
            logger = _logger;
        }
        /// <summary>
        /// 开始处理订阅所对应的方法
        /// </summary>
        /// <param name="subscribeName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public async Task HandlerAsync(string subscribeName, string msg)
        {
            subscribeName.CheckNullOrEmpty();

            logger.LogInformation("开始处理接收到的订阅信息,subscribeName:{subscribeName},msg:{msg}", subscribeName, msg);
            //查找当前订阅信息对应的 所处的对象信息
            BaseSubscribeTypeModel baseSubscribeType = SubscribeTypeFactory.Get(subscribeName);
            if (baseSubscribeType == null)
            {
                logger.LogWarning("查找不到指定的订阅信息:channel:{channel}", subscribeName);
                return;
            }

            //根据类型获取泛型
            var dynamicMethod = serviceProvider.GetRequiredService(typeof(DynamicMethodExpression<>).MakeGenericType(baseSubscribeType.ServiceType)) as IDynamicMethodExpression;

            //获取当前执行方法的参数
            var paramters = MethodCache.Get(baseSubscribeType.ServiceType, baseSubscribeType.MethodName).parameterInfos;
            //获取当前方法是否含有参数
            var isParamter = paramters != null && paramters.Count() > 0;
            //验证是否是有参的方法，当前默认只支持一个参数
            var paramtersObject = isParamter ? msg.ToDeserialized(paramters[0].ParameterType) : null;

            //消息处理前的aop事件
            NarutoMessageAopEvent.PreHandler?.Invoke(subscribeName, msg);

            //操作动态执行方法接口
            await dynamicMethod.ExecAsync(serviceProvider.GetRequiredService(baseSubscribeType.ServiceType), baseSubscribeType.MethodName, isParamter, isParamter ? paramters[0].ParameterType : null, paramtersObject);

            //消息处理后的aop事件
            NarutoMessageAopEvent.AfterHandler?.Invoke(subscribeName, msg);

            logger.LogInformation("处理完毕接受到的订阅信息");
        }
    }
}
