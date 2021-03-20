using System.Linq;
using System.Threading.Tasks;
using PopugJira.Accounting.Application.Dtos;
using PopugJira.Accounting.Application.Queries;
using PopugJira.Common;
using PopugJira.EventBus;
using PopugJira.EventBus.Events.BusinessEvents;
using Serviced;

namespace PopugJira.Accounting.Application.Commands
{
    public class PayEarnedToEmployeesForTodayCommand : IScoped
    {
        private readonly GetAllAccountsQuery getAllAccountsQuery;
        private readonly CreateTransactionCommand createTransactionCommand;
        private readonly IDateTimeService dateTimeService;
        private readonly IMessageBus messageBus;

        public PayEarnedToEmployeesForTodayCommand(GetAllAccountsQuery getAllAccountsQuery,
                                                   CreateTransactionCommand createTransactionCommand,
                                                   IDateTimeService dateTimeService,
                                                   IMessageBus messageBus)
        {
            this.getAllAccountsQuery = getAllAccountsQuery;
            this.createTransactionCommand = createTransactionCommand;
            this.dateTimeService = dateTimeService;
            this.messageBus = messageBus;
        }

        public async Task Execute()
        {
            var accounts = await getAllAccountsQuery.Query();
            
            foreach (var account in accounts)
            {
                var paymentDateTime = dateTimeService.UtcNow;

                if (account.Balance > 0)
                {
                    await createTransactionCommand.Execute(new CreateTransactionDto
                                                           {
                                                               Debit = 0,
                                                               Credit = -account.Balance,
                                                               DateTime = paymentDateTime,
                                                               AccountId = account.Id,
                                                               Reason = "Daily payment"
                                                           });
                }

                await messageBus.Publish(new PayEarnedToEmployeesEventV1
                                         {
                                             AccountId = account.Id,
                                             Earned = account.Balance,
                                             PaymentDateTime = paymentDateTime
                                         });
            }
        }
    }
}