using CostControlAPI.Repositories.Contrants;
using CostControlAPI.Repositories.FinancialTransaction.CreateFinancialTransactionRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CostControlAPI.Repositories.FinancialTransaction
{
    public class FinancialTransactionRepository : IFinancialTransactionRepository
    {
        private readonly ICreateFinancialTransactionRepository _createFinancialTransactionRepository;

        public FinancialTransactionRepository(ICreateFinancialTransactionRepository createFinancialTransactionRepository)
        {
            _createFinancialTransactionRepository = createFinancialTransactionRepository;
        }

        public async Task CreateAsync(Models.FinancialTransaction financialTransaction)
        {
            await _createFinancialTransactionRepository.CreateAsync(financialTransaction);
        }

        public async Task<IEnumerable<Models.FinancialTransaction>> GetAsync(string month, string year)
        {
            throw new System.NotImplementedException();
        }
    }
}
