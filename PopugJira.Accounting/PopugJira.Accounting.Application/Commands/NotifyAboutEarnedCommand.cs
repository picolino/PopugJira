using System.Threading.Tasks;
using PopugJira.EventBus;
using PopugJira.EventBus.Tasks;
using Serviced;

namespace PopugJira.Accounting.Application.Commands
{
    public class NotifyAboutEarnedCommand : IScoped
    {
        private readonly IMessageBus messageBus;

        public NotifyAboutEarnedCommand(IMessageBus messageBus)
        {
            this.messageBus = messageBus;
        }
        
        public async Task Execute(string accountId, decimal earned)
        {
            var message = $"You earned: {earned:C2}";

            await messageBus.Publish(new NotifyTaskV1
                                     {
                                         UserId = accountId,
                                         Message = message,
                                         NotifyBy = NotifyBy.Email
                                     });
        }
    }
}