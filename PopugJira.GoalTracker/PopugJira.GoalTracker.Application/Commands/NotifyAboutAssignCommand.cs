using System.Threading.Tasks;
using PopugJira.EventBus;
using PopugJira.EventBus.Tasks;
using Serviced;

namespace PopugJira.GoalTracker.Application.Commands
{
    public class NotifyAboutAssignCommand : IScoped
    {
        private readonly IMessageBus messageBus;

        public NotifyAboutAssignCommand(IMessageBus messageBus)
        {
            this.messageBus = messageBus;
        }
        
        public async Task Execute(string assigneeId, string goal)
        {
            var message = $"You assigned to complete goal '{goal}'";

            await messageBus.Publish(new NotifyTaskV1
                                     {
                                         UserId = assigneeId,
                                         Message = message,
                                         NotifyBy = NotifyBy.Email | NotifyBy.Sms | NotifyBy.Slack
                                     });
        }
    }
}