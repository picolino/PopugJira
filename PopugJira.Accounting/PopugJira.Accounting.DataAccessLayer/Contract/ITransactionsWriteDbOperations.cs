using System.Threading.Tasks;
using PopugJira.Accounting.Domain;

namespace PopugJira.Accounting.DataAccessLayer.Contract
{
    public interface ITransactionsWriteDbOperations
    {
        Task Create(Transaction transaction);
    }
}