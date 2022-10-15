using System.Threading.Tasks;

namespace CostControlAPI.Repositories.FinancialTransaction.CreateFinancialTransactionRepository
{
    public interface ICreateFinancialTransactionRepository
    {
        public Task CreateAsync(Models.FinancialTransaction financialTransaction);
    }
}
