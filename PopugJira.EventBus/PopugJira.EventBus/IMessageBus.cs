using System;
using System.Threading.Tasks;
using Serviced;

namespace PopugJira.EventBus
{
    public interface IMessageBus : IScoped
    {
        Task Publish<TMessage>(TMessage message);
        Task Subscribe<TMessage>(string subscriptionId, Func<TMessage, Task> onMessage);
    }
}