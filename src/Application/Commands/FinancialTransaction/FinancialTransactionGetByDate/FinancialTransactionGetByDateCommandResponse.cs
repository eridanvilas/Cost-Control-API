using System.Collections.Generic;

namespace CostControlAPI.Application.Commands.FinancialTransaction.FinancialTransactionGetByDate
{
    public class FinancialTransactionGetByDateCommandResponse
    {
        public FinancialTransactionGetByDateCommandResponse(IList<CostControlAPI.Models.FinancialTransaction> rinancialTransactions)
        {
            FinancialTransactions = rinancialTransactions;
        }

        public IList<CostControlAPI.Models.FinancialTransaction>  FinancialTransactions{ get; set; }
    }
}
