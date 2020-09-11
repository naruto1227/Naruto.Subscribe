using Naruto.Subscribe.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Naruto.Subscribe
{
    /// <summary>
    /// 订阅属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]

    public class SubscribeAttribute : Attribute
    {
        /// <summary>
        /// 订阅的名称
        /// </summary>
        public string Name { get; }

        public SubscribeAttribute(string name)
        {
            Name = name;
        }
    }
}
