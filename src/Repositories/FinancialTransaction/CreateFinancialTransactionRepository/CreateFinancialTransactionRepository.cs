using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

namespace CostControlAPI.Repositories.FinancialTransaction.CreateFinancialTransactionRepository
{
    public class CreateFinancialTransactionRepository : ICreateFinancialTransactionRepository
    {
        private readonly IConfiguration _configuration;

        public CreateFinancialTransactionRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string Connection => _configuration.GetConnectionString("DefaultConnection");

        public async Task CreateAsync(Models.FinancialTransaction financialTransaction)
        {
            try
            {
                using var connection = new MySqlConnection(Connection);
                connection.Open();

                var mySqlCommand = new MySqlCommand();

                mySqlCommand.CommandText = @"INSERT INTO financial_transaction
                                            (date,
                                            amount,
                                            identifier,
                                            description,
                                            transactionType,
                                            paymentType)
                                            VALUES
                                            (@Date,
                                             @Amount,
                                             @Identifier,
                                             @Description,
                                             @TransactionType,
                                             @PaymentType);";
                mySqlCommand.Connection = connection;
              
                mySqlCommand.Parameters.AddWithValue("@Date", financialTransaction.Date.Date);
                mySqlCommand.Parameters.AddWithValue("@Amount", financialTransaction.Amount);
                mySqlCommand.Parameters.AddWithValue("@Identifier", financialTransaction.Identifier);
                mySqlCommand.Parameters.AddWithValue("@Description", financialTransaction.Description);
                mySqlCommand.Parameters.AddWithValue("@TransactionType", financialTransaction.TransactionType);
                mySqlCommand.Parameters.AddWithValue("@PaymentType", financialTransaction.PaymentType);  
                mySqlCommand.Prepare();

                mySqlCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
    }
}
