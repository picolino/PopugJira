using System.Threading.Tasks;
using FluentScheduler;
using PopugJira.Accounting.Application.Commands;
using Serviced;

namespace PopugJira.Accounting.Jobs
{
    public class PayEarnedToEmployeesJob : IAsyncJob, IScoped
    {
        private readonly PayEarnedToEmployeesForTodayCommand payEarnedToEmployeesForTodayCommand;

        public PayEarnedToEmployeesJob(PayEarnedToEmployeesForTodayCommand payEarnedToEmployeesForTodayCommand)
        {
            this.payEarnedToEmployeesForTodayCommand = payEarnedToEmployeesForTodayCommand;
        }

        public async Task ExecuteAsync()
        {
            await payEarnedToEmployeesForTodayCommand.Execute();
        }
    }
}