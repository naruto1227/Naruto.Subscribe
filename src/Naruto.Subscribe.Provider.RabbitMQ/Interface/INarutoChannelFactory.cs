using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Naruto.Subscribe.Provider.RabbitMQ.Interface
{
    /// <summary>
    /// mq的通道工厂
    /// </summary>
    public interface INarutoChannelFactory : IDisposable
    {
        IModel Get();

        Task<IModel> GetAsync();
    }
}
