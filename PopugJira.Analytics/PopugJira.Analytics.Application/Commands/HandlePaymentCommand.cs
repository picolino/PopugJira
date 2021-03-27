using System.Threading.Tasks;
using PopugJira.Analytics.Application.Dtos;
using PopugJira.Analytics.DataAccessLayer.Contract;
using Serviced;

namespace PopugJira.Analytics.Application.Commands
{
    public class HandlePaymentCommand : IScoped
    {
        private readonly ITopManagementEarnedEntryWriteDbOperations topManagementEarnedEntryWriteDbOperations;

        public HandlePaymentCommand(ITopManagementEarnedEntryWriteDbOperations topManagementEarnedEntryWriteDbOperations)
        {
            this.topManagementEarnedEntryWriteDbOperations = topManagementEarnedEntryWriteDbOperations;
        }
        
        public async Task Execute(PaymentInfoDto paymentInfoDto)
        {
            if (paymentInfoDto.Earned < 0)
            {
                var positiveEarnedForManagement = -paymentInfoDto.Earned;
                await topManagementEarnedEntryWriteDbOperations
                    .AddEarnedWithNegativeEmployeesCountIncrementOrCreateNew(paymentInfoDto.PaymentDateTime,
                                                                             positiveEarnedForManagement);
            }
        }
    }
}