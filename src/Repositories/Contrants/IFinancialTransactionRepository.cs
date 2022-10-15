using System.Collections.Generic;
using System.Threading.Tasks;

namespace CostControlAPI.Repositories.Contrants
{
    public interface IFinancialTransactionRepository
    {
        public Task CreateAsync(Models.FinancialTransaction financialTransaction);
        Task<IEnumerable<Models.FinancialTransaction>> GetAsync(string month ,string year);
    }
}
