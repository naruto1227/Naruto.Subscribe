using System;
using System.Collections.Generic;
using System.Text;

namespace Naruto.Subscribe
{
    /// <summary>
    /// 订阅的名称
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class SubscribeAttribute : Attribute
    {
        public string Name { get; set; }
    }
}
