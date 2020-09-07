using Naruto.Subscribe.Provider.RabbitMQ.Interface;
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
           return connectionFactory.Get()?.CreateModel();
        }

        public async Task<IModel> GetAsync()
        {
            return (await connectionFactory.GetAsync())?.CreateModel();
        }



        public void Dispose()
        {
           // model?.Dispose();
        }
    }
}
