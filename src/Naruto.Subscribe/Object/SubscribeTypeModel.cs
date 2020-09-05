using System;
using System.Collections.Generic;
using System.Text;

namespace Naruto.Subscribe.Object
{
    /// <summary>
    /// 订阅类型的模型
    /// </summary>
    public class SubscribeTypeModel : BaseSubscribeTypeModel
    {
        /// <summary>
        /// 订阅的名称
        /// </summary>
        public string SubscribeName { get; set; }

    }

    public class BaseSubscribeTypeModel
    {
        /// <summary>
        /// 订阅特性标记的方法名
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// 当前对象的类型
        /// </summary>
        public Type ServiceType { get; set; }
    }
}
