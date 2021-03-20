using System.Threading.Tasks;
using PopugJira.Accounting.Application.Dtos;
using PopugJira.Accounting.DataAccessLayer.Contract;
using PopugJira.Accounting.Domain;
using Serviced;

namespace PopugJira.Accounting.Application.Commands
{
    public class CreateTransactionCommand : IScoped
    {
        private readonly IAccountsGetDbOperations accountsGetDbOperations;
        private readonly IAccountsWriteDbOperations accountsWriteDbOperations;
        private readonly ITransactionsWriteDbOperations transactionsWriteDbOperations;

        public CreateTransactionCommand(IAccountsGetDbOperations accountsGetDbOperations,
                                        IAccountsWriteDbOperations accountsWriteDbOperations,
                                        ITransactionsWriteDbOperations transactionsWriteDbOperations)
        {
            this.accountsGetDbOperations = accountsGetDbOperations;
            this.accountsWriteDbOperations = accountsWriteDbOperations;
            this.transactionsWriteDbOperations = transactionsWriteDbOperations;
        }
        
        public async Task Execute(CreateTransactionDto createDto)
        {
            var account = await accountsGetDbOperations.Get(createDto.AccountId);
            var transaction = new Transaction(null, account, createDto.DateTime, createDto.Debit, createDto.Credit, createDto.Reason);
            await transactionsWriteDbOperations.Create(transaction);
            await accountsWriteDbOperations.SetBalance(account.Id, account.Balance + createDto.Debit + createDto.Credit);
        }
    }
}