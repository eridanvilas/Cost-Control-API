using System.Collections.Generic;
using System.Threading.Tasks;

namespace CostControlAPI.Repositories.Contrants
{
    public interface IFinancialTransactionRepository
    {
        Task CreateAsync(Models.FinancialTransaction financialTransaction);
        Task<IList<Models.FinancialTransaction>> GetAsyncByDate(string month ,string year);
        Task<Models.FinancialTransaction> GetAsyncByIdentifier(string identifier);
    }
}
