using System.Threading;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using PopugJira.EventBus.Tasks;
using PopugJira.Notifications.Application.Commands;
using Serviced;

namespace PopugJira.Notifications.Consumers
{
    public class NotifyTaskConsumer : IConsumeAsync<NotifyTaskV1>, IScoped
    {
        private readonly NotifyByPhoneSmsCommand notifyByPhoneSmsCommand;
        private readonly NotifyByEmailCommand notifyByEmailCommand;
        private readonly NotifyBySlackCommand notifyBySlackCommand;

        public NotifyTaskConsumer(NotifyByPhoneSmsCommand notifyByPhoneSmsCommand,
                                  NotifyByEmailCommand notifyByEmailCommand,
                                  NotifyBySlackCommand notifyBySlackCommand)
        {
            this.notifyByPhoneSmsCommand = notifyByPhoneSmsCommand;
            this.notifyByEmailCommand = notifyByEmailCommand;
            this.notifyBySlackCommand = notifyBySlackCommand;
        }
        
        public async Task ConsumeAsync(NotifyTaskV1 message, CancellationToken cancellationToken = new CancellationToken())
        {
            if (message.NotifyBy.HasFlag(NotifyBy.Email))
            {
                await notifyByEmailCommand.Execute(message.UserId, message.Message);
            }
            
            if (message.NotifyBy.HasFlag(NotifyBy.Sms))
            {
                await notifyByPhoneSmsCommand.Execute(message.UserId, message.Message);
            }
            
            if (message.NotifyBy.HasFlag(NotifyBy.Slack))
            {
                await notifyBySlackCommand.Execute(message.UserId, message.Message);
            }
        }
    }
}