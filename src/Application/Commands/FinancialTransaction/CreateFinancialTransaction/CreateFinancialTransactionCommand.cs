using MediatR;

namespace CostControlAPI.Application.Commands.FinancialTransaction.CreateFinancialTransaction
{
    public class CreateFinancialTransactionCommand : IRequest<Unit>
    {
        public CreateFinancialTransactionCommand(CostControlAPI.Models.FinancialTransaction financialTransaction)
        {
            FinancialTransaction = financialTransaction;
        }

        public CostControlAPI.Models.FinancialTransaction FinancialTransaction { get; set; }
    }
}
