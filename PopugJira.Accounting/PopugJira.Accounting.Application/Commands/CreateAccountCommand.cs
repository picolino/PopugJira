using System.Threading.Tasks;
using PopugJira.Accounting.Application.Dtos;
using PopugJira.Accounting.DataAccessLayer.Contract;
using PopugJira.Accounting.Domain;
using Serviced;

namespace PopugJira.Accounting.Application.Commands
{
    public class CreateAccountCommand : IScoped
    {
        private readonly IAccountsWriteDbOperations accountsWriteDbOperations;

        public CreateAccountCommand(IAccountsWriteDbOperations accountsWriteDbOperations)
        {
            this.accountsWriteDbOperations = accountsWriteDbOperations;
        }

        public async Task Execute(CreateAccountDto createAccountDto)
        {
            var account = new Account(null, createAccountDto.Name, 0);
            await accountsWriteDbOperations.Create(account);
        }
    }
}