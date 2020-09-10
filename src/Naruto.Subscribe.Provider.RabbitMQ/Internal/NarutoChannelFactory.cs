using Naruto.Subscribe.Provider.RabbitMQ.Interface;
using Naruto.Subscribe.Provider.RabbitMQ.Object;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Naruto.Subscribe.Provider.RabbitMQ.Internal
{
    internal class NarutoChannelFactory : INarutoChannelFactory
    {
        private readonly INarutoConnectionFactory connectionFactory;

        public NarutoChannelFactory(INarutoConnectionFactory _connectionFactory)
        {
            connectionFactory = _connectionFactory;

        }

        public IModel Get()
        {
            var channel = connectionFactory.Get()?.CreateModel();
            SetChannel(channel);
            return channel;
        }

        public async Task<IModel> GetAsync()
        {
            var channel = (await connectionFactory.GetAsync())?.CreateModel();
            SetChannel(channel);
            return channel;
        }

        /// <summary>
        /// 设置信道的信息
        /// </summary>
        private void SetChannel(IModel channel)
        {
            //绑定交换机
            channel.ExchangeDeclare(exchange: RabbitMQOption.ExchangeName, type: ExchangeType.Topic, durable: true, autoDelete: false, arguments: null);
            //绑定存储的消息队列
            channel.QueueDeclare(queue: RabbitMQOption.QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        }
    }
}
