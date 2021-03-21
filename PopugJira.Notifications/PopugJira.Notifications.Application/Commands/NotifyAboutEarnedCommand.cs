using System.Threading.Tasks;
using Serviced;

namespace PopugJira.Notifications.Application.Commands
{
    public class NotifyAboutEarnedCommand : IScoped
    {
        private readonly NotifyByEmailCommand notifyByEmailCommand;

        public NotifyAboutEarnedCommand(NotifyByEmailCommand notifyByEmailCommand)
        {
            this.notifyByEmailCommand = notifyByEmailCommand;
        }
        
        public async Task Execute(string accountId, decimal earned)
        {
            var message = $"You earned: {earned:C2}";

            await notifyByEmailCommand.Execute(accountId, message);
        }
    }
}