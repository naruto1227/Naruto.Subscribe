using Microsoft.Extensions.Logging;
using Naruto.Subscribe.Extension;
using Naruto.Subscribe.Interface;
using Naruto.Subscribe.Provider.RabbitMQ.Interface;
using Naruto.Subscribe.Provider.RabbitMQ.Object;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Naruto.Subscribe.Provider.RabbitMQ
{
    /// <summary>
    /// rabbitmq消费者实现
    /// </summary>
    public class RabbitMQSubscribeEvent : ISubscribeEvent
    {
        private readonly ILogger logger;

        private readonly INarutoChannelFactory narutoChannelFactory;


        /// <summary>
        /// 订阅处理接口
        /// </summary>
        private readonly ISubscribeHandler subscribeHandler;

        /// <summary>
        /// 信道
        /// </summary>
        private IModel channel;

        public RabbitMQSubscribeEvent(INarutoChannelFactory _narutoChannelFactory, ILogger<RabbitMQSubscribeEvent> _logger, ISubscribeHandler _subscribeHandler)
        {
            narutoChannelFactory = _narutoChannelFactory;
            logger = _logger;
            subscribeHandler = _subscribeHandler;
        }
        public async Task SubscribeAsync(List<string> subscribeNames)
        {
            subscribeNames.CheckNull();
            //创建一个信道
            channel = await narutoChannelFactory.GetAsync();
            //绑定交换机
            channel.ExchangeDeclare(exchange: RabbitMQOption.ExchangeName, type: NarutoExchangeType.Topic, durable: true, autoDelete: false, arguments: null);
            //绑定队列
            channel.QueueDeclare(queue: RabbitMQOption.QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            //设置绑定关系
            foreach (var subscribeName in subscribeNames)
            {
                logger.LogInformation("开始订阅[{subscribeName}]信息", subscribeName);
                //将队列和交换机还有路由key绑定关系
                channel.QueueBind(queue: RabbitMQOption.QueueName, exchange: RabbitMQOption.ExchangeName, routingKey: subscribeName);

                logger.LogInformation("订阅完成[{subscribeName}]", subscribeName);
            }
            // 构造消费者实例
            var consumer = new EventingBasicConsumer(channel);
            // 绑定消息接收后的事件委托
            consumer.Received += ReceivedMessage;
            //消费取消
            consumer.ConsumerCancelled += ConsumerCancelledEvent;
            //服务关闭的时候
            consumer.Shutdown += ShutdownEvent;
            // 启动消费者
            channel.BasicConsume(queue: RabbitMQOption.QueueName, autoAck: false, consumer: consumer);
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ea"></param>
        private async void ReceivedMessage(object? model, BasicDeliverEventArgs ea)
        {
            await subscribeHandler.HandlerAsync(ea.RoutingKey, Encoding.UTF8.GetString(ea.Body.ToArray()));
            //确认消费完此消息
            channel.BasicAck(ea.DeliveryTag, false);
        }

        private void ConsumerCancelledEvent(object? model, ConsumerEventArgs consumerEventArgs)
        {
        }

        private void ShutdownEvent(object? model, ShutdownEventArgs shutdownEventArgs)
        {
            logger.LogWarning("ShutdownEvent:{shutdownEventArgs}", shutdownEventArgs.ToString());
        }
    }
}
