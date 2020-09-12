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

namespace Naruto.Subscribe
{
    /// <summary>
    /// 启用订阅
    /// </summary>
    public class EnableSubscribeHostServices : BackgroundService
    {
        private readonly ISubscribeEvent subscribeEvent;

        private readonly ILogger logger;

        public EnableSubscribeHostServices(ISubscribeEvent _subscribeEvent, ILogger<EnableSubscribeHostServices> _logger)
        {
            subscribeEvent = _subscribeEvent;
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
                //订阅事件接口
                if (subscribeEvent != null)
                {
                    //处理订阅信息
                    await subscribeEvent.SubscribeAsync(subscribeNames);
                }
                else
                {
                    logger.LogWarning("当前未实现订阅接口，无法订阅消息");
                }
            }
        }
    }
}
