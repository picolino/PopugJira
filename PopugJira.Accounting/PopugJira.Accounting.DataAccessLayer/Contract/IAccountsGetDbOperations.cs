using System.Threading.Tasks;
using PopugJira.Accounting.Domain;

namespace PopugJira.Accounting.DataAccessLayer.Contract
{
    public interface IAccountsGetDbOperations
    {
        Task<Account> Get(string id);
    }
}