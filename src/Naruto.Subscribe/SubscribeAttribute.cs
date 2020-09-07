using Naruto.Subscribe.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Naruto.Subscribe
{
    /// <summary>
    /// 订阅
    /// </summary>

    public class SubscribeAttribute : SubscribeTopicAttribute
    {
        public SubscribeAttribute(string name) : base(name)
        {

        }
    }
}
