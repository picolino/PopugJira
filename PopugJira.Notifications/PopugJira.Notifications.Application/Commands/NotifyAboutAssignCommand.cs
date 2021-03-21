using System.Threading.Tasks;
using Serviced;

namespace PopugJira.Notifications.Application.Commands
{
    public class NotifyAboutAssignCommand : IScoped
    {
        private readonly NotifyByPhoneSmsCommand notifyByPhoneSmsCommand;
        private readonly NotifyByEmailCommand notifyByEmailCommand;
        private readonly NotifyBySlackCommand notifyBySlackCommand;

        public NotifyAboutAssignCommand(NotifyByPhoneSmsCommand notifyByPhoneSmsCommand,
                                        NotifyByEmailCommand notifyByEmailCommand,
                                        NotifyBySlackCommand notifyBySlackCommand)
        {
            this.notifyByPhoneSmsCommand = notifyByPhoneSmsCommand;
            this.notifyByEmailCommand = notifyByEmailCommand;
            this.notifyBySlackCommand = notifyBySlackCommand;
        }
        
        public async Task Execute(string assigneeId, string goal)
        {
            var message = $"You assigned to complete goal '{goal}'";

            var notifyPhoneTask = notifyByPhoneSmsCommand.Execute(assigneeId, message);
            var notifyEmailTask = notifyByEmailCommand.Execute(assigneeId, message);
            var notifySlackTask = notifyBySlackCommand.Execute(assigneeId, message);
            
            await Task.WhenAll(notifyPhoneTask, notifyEmailTask, notifySlackTask);
        }
    }
}