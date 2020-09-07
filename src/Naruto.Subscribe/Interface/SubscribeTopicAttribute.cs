using System;
using System.Collections.Generic;
using System.Text;

namespace Naruto.Subscribe.Interface
{
    /// <summary>
    /// 订阅主题模式
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class SubscribeTopicAttribute : Attribute
    {
        protected SubscribeTopicAttribute(string name)
        {
            Name = name;
        }
        /// <summary>
        /// 订阅的名称
        /// </summary>
        public string Name { get; }
    }
}
