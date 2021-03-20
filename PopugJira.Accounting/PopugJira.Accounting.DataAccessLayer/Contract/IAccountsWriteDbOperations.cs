using System.Threading.Tasks;
using PopugJira.Accounting.Domain;

namespace PopugJira.Accounting.DataAccessLayer.Contract
{
    public interface IAccountsWriteDbOperations
    {
        Task Create(Account account);
        Task SetBalance(string accountId, decimal newBalance);
    }
}