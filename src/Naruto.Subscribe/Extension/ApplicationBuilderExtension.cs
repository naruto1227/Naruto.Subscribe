using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Naruto.Subscribe.Interface;
using Naruto.Subscribe.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naruto.Subscribe.Extension
{
    public static class ApplicationBuilderExtension
    {
        /// <summary>
        /// 启用订阅功能
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <returns></returns>
        public static async Task EnableSubscribe(this IApplicationBuilder applicationBuilder)
        {
            //获取所有需要订阅的名称
            var names = SubscribeTypeFactory.GetAllSubscribeName();

            if (names != null && names.Count() > 0)
            {
                //订阅事件接口
                var subscribeEvent = applicationBuilder.ApplicationServices.GetService<ISubscribeEvent>();
                if (subscribeEvent == null)
                {
                    throw new InvalidOperationException("请先实现ISubscribeEvent接口");
                }
                foreach (var item in names)
                {
                    //处理订阅信息
                    await subscribeEvent.SubscribeAsync(item);
                }
            }
        }
    }
}
