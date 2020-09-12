using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Naruto.Subscribe.Interface;
using Naruto.Subscribe.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Naruto.Subscribe
{
    /// <summary>
    /// 启用订阅
    /// </summary>
    public class EnableSubscribeHostServices : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;

        private readonly ILogger logger;

        public EnableSubscribeHostServices(IServiceProvider _serviceProvider, ILogger<EnableSubscribeHostServices> _logger)
        {
            serviceProvider = _serviceProvider;
            logger = _logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.Register(() =>
            {
                logger.LogInformation("stop");
            });
            //获取所有需要订阅的名称
            var subscribeNames = SubscribeTypeFactory.GetAllSubscribeName();

            if (subscribeNames != null && subscribeNames.Count() > 0)
            {
                var subscribeEvent = serviceProvider.GetService<ISubscribeEvent>();
                if (subscribeEvent != null)
                {
                    //处理订阅信息
                    await subscribeEvent.SubscribeAsync(subscribeNames);
                }
                else
                {
                    logger.LogWarning("当前未实现订阅服务");
                }
            }
        }
    }
}
