using MediatR;

namespace CostControlAPI.Application.Commands.FinancialTransaction.FinancialTransactionGetByDate
{
    public class FinancialTransactionGetByDateCommand : IRequest<FinancialTransactionGetByDateCommandResponse>
    {
        public string Month { get; set; }
        public string Year { get; set; }

        public FinancialTransactionGetByDateCommand(string month, string year)
        {
            Month = month;
            Year = year;
        }
    }
}
