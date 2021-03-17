using System;
using System.Threading.Tasks;
using EasyNetQ;

namespace PopugJira.EventBus
{
    public class RabbitMqMessageBus : IMessageBus
    {
        public IBus Bus { get; }

        public RabbitMqMessageBus(IBus bus)
        {
            Bus = bus;
        }

        public async Task Publish<TMessage>(TMessage message)
        {
            await Bus.PubSub.PublishAsync(message);
        }

        public async Task Subscribe<TMessage>(string subscriptionId, Func<TMessage, Task> onMessage)
        {
            await Bus.PubSub.SubscribeAsync(subscriptionId, onMessage);
        }
    }
}