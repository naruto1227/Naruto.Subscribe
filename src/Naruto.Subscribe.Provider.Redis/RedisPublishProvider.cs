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
    /// redis发布 提供者
    /// </summary>
    public class RedisPublishProvider : INarutoPublish
    {
        private readonly ILogger logger;

        private readonly IRedisRepository redis;

        public RedisPublishProvider(ILogger<RedisPublishProvider> _logger, IRedisRepository _redis)
        {
            logger = _logger;
            redis = _redis;
        }

        /// <summary>
        /// 同步发布
        /// </summary>
        /// <param name="subscribeName"></param>
        /// <param name="msg"></param>
        public void Publish(string subscribeName, object msg = null)
        {
            subscribeName.CheckNullOrEmpty();
            logger.LogInformation("Publish:开始发布消息，subscribeName={subscribeName},msg={msg}", subscribeName, msg);
            var res = redis.Subscribe.Publish(subscribeName, msg.ToJsonString());
            logger.LogInformation("Publish:发布完成，subscribeName={subscribeName},msg={msg},result={res}", subscribeName, msg, res);
        }

        /// <summary>
        /// 异步发布
        /// </summary>
        /// <param name="subscribeName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public async Task PublishAsync(string subscribeName, object msg = null)
        {
            subscribeName.CheckNullOrEmpty();
            logger.LogInformation("PublishAsync:开始发布消息，subscribeName={subscribeName},msg={msg}", subscribeName, msg);
            var res = await redis.Subscribe.PublishAsync(subscribeName, msg.ToJsonString());
            logger.LogInformation("PublishAsync:发布完成，subscribeName={subscribeName},msg={msg},result={res}", subscribeName, msg, res);
        }
    }
}
