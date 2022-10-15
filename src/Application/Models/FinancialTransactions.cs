using CostControlAPI.Application.Models.Enum;
using System;

namespace CostControlAPI.Application.Models
{
    public class FinancialTransactions
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Identifier { get; set; }
        public string Description { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
