using CostControlAPI.Application.Models.Enum;
using System;

namespace CostControlAPI.Models
{
    public class FinancialTransaction
    {
        public FinancialTransaction(DateTime date, decimal amount, string identifier, string description, TransactionType transactionType, PaymentType paymentType)
        {
            Date = date;
            Amount = amount;
            Identifier = identifier;
            Description = description;
            TransactionType = transactionType;
            PaymentType = paymentType;
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Identifier { get; set; }
        public string Description { get; set; }
        public TransactionType TransactionType { get; set; }
        public PaymentType PaymentType { get; set; }

    }
}
