using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Naruto.Subscribe.Interface
{
    /// <summary>
    /// 张海波
    /// 2020-09-06
    /// 发布消息接口
    /// </summary>
    public interface INarutoPublish
    {
        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="subscribeName">订阅名</param>
        /// <param name="msg">消息内容</param>
        /// <returns></returns>
        Task PublishAsync(string subscribeName, object msg = default);
        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="subscribeName">订阅名</param>
        /// <param name="msg">消息内容</param>
        /// <returns></returns>
        void Publish(string subscribeName, object msg = default);
    }
}
