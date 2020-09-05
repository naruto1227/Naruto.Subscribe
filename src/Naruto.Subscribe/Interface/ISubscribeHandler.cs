using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Naruto.Subscribe.Interface
{
    /// <summary>
    /// 订阅的处理接口
    /// </summary>
    public interface ISubscribeHandler
    {
        /// <summary>
        /// 处理订阅消息
        /// </summary>
        /// <param name="subscribeName">渠道 订阅名称</param>
        /// <param name="msg">消息内容</param>
        /// <returns></returns>
        Task Handler(string subscribeName, string msg);
    }
}
