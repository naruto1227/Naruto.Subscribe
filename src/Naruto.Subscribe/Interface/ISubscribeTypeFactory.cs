using Naruto.Subscribe.Object;
using System;
using System.Collections.Generic;
using System.Text;

namespace Naruto.Subscribe.Interface
{
    /// <summary>
    /// 张海波
    /// 2020-09-05
    /// 订阅类型的 工厂类
    /// </summary>
    public interface ISubscribeTypeFactory
    {
        /// <summary>
        /// 存储订阅的类型
        /// </summary>
        /// <param name="name">订阅的名称</param>
        /// <param name="serviceType">服务类型</param>
        bool Set(SubscribeTypeModel subscribe);

        /// <summary>
        /// 获取所有的订阅名称
        /// </summary>
        /// <returns></returns>
        List<string> GetAllSubscribeName();
        /// <summary>
        /// 订阅的名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        BaseSubscribeTypeModel Get(string name);
    }
}
