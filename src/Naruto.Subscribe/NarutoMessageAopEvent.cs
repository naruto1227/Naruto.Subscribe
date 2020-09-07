using System;
using System.Collections.Generic;
using System.Text;

namespace Naruto.Subscribe
{
    /// <summary>
    /// 张海波
    /// 2020-09-06
    /// 消息处理的aop事件
    /// </summary>
    public class NarutoMessageAopEvent
    {

        /// <summary>
        /// 消息处理前
        /// </summary>
        internal static Action<string, string> PreHandler;

        /// <summary>
        /// 消息处理后
        /// </summary>
        internal static Action<string, string> AfterHandler;

        /// <summary>
        /// 订阅消息处理前的事件 第一个参数是订阅的名称，第二个参数是 接收的消息
        /// </summary>
        /// <param name="messageEvent"></param>
        public static void RegisterPreHandlerEvent(Action<string, string> messageEvent)
        {
            PreHandler = messageEvent;
        }

        /// <summary>
        /// 订阅消息处理后的事件 第一个参数是订阅的名称，第二个参数是 接收的消息
        /// </summary>
        /// <param name="messageEvent"></param>
        public static void RegisterAfterHandlerEvent(Action<string, string> messageEvent)
        {
            AfterHandler = messageEvent;
        }
    }
}
