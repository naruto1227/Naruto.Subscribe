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
        Task Handler(string channel, string msg);
    }
}
