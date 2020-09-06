using Microsoft.Extensions.Logging;
using Naruto.Redis;
using Naruto.Subscribe.Extension;
using Naruto.Subscribe.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Naruto.Subscribe.Provider.Redis
{
    /// <summary>
    /// redis订阅的实现
    /// </summary>
    public class RedisSubscribeEvent : ISubscribeEvent
    {
        /// <summary>
        /// redis仓储
        /// </summary>
        private readonly IRedisRepository redis;

        private readonly ILogger logger;

        /// <summary>
        /// 订阅处理接口
        /// </summary>
        private readonly ISubscribeHandler subscribeHandler;


        public RedisSubscribeEvent(IRedisRepository _redis, ILogger<RedisSubscribeEvent> _logger,
            ISubscribeHandler _subscribeHandler)
        {
            redis = _redis;
            logger = _logger;
            subscribeHandler = _subscribeHandler;
        }

        /// <summary>
        /// 处理订阅信息
        /// </summary>
        /// <param name="subscribeName"></param>
        /// <returns></returns>
        public async Task SubscribeAsync(string subscribeName)
        {
            subscribeName.CheckNullOrEmpty();
            logger.LogInformation("开始订阅[{subscribeName}]信息", subscribeName);

            //订阅消息
            await redis.Subscribe.SubscribeAsync(subscribeName, async (subscribeName, msg) =>
            {
                //处理消息
                await subscribeHandler.HandlerAsync(subscribeName, msg);
            });
            logger.LogInformation("订阅成功[{subscribeName}]", subscribeName);
        }
    }
}
