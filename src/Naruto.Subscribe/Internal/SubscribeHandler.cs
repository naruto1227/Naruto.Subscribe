using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Naruto.Subscribe.Interface;
using Naruto.Subscribe.Object;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Naruto.Subscribe.Internal
{
    public class SubscribeHandler : ISubscribeHandler
    {
      //  private readonly ISubscribeTypeFactory subscribeTypeFactory;

        private readonly IServiceProvider serviceProvider;

        private readonly ILogger logger;
        public SubscribeHandler( IServiceProvider _serviceProvider, ILogger<SubscribeHandler> _logger)
        {
         //   subscribeTypeFactory = _subscribeTypeFactory;
            serviceProvider = _serviceProvider;
            logger = _logger;
        }
        public async Task Handler(string channel, string msg)
        {
            logger.LogInformation("开始处理接收到的订阅信息,Channel:{channel},msg:{msg}", channel, msg);
            //查找当前订阅信息对应的 所处的对象信息
            BaseSubscribeTypeModel baseSubscribeType = SubscribeTypeFactory.Get(channel);
            if (baseSubscribeType == null)
            {
                logger.LogWarning("查找不到指定的订阅信息:channel:{channel}", channel);
                return;
            }

            //根据类型获取泛型
            var dynamicMethod = serviceProvider.GetRequiredService(typeof(DynamicMethodExpression<>).MakeGenericType(baseSubscribeType.ServiceType)) as IDynamicMethodExpression;
            //操作动态执行方法接口
            ;
            await dynamicMethod.ExecAsync(serviceProvider.GetRequiredService(baseSubscribeType.ServiceType), baseSubscribeType.MethodName, null);

            logger.LogInformation("处理完毕接受到的订阅信息");
        }
    }
}
