using CostControlAPI.Repositories.Contrants;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CostControlAPI.Repositories.FinancialTransaction
{
    public class FinancialTransactionRepository : IFinancialTransactionRepository
    {
        private readonly IConfiguration _configuration;
        private string Connection => _configuration.GetConnectionString("DefaultConnection");

        public FinancialTransactionRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task CreateAsync(Models.FinancialTransaction financialTransaction)
        {
            try
            {
                using var connection = new MySqlConnection(Connection);
                connection.Open();

                var cmd = new MySqlCommand();

                cmd.CommandText = @"INSERT INTO cost_control.financial_transaction
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
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@Date", financialTransaction.Date.Date);
                cmd.Parameters.AddWithValue("@Amount", financialTransaction.Amount);
                cmd.Parameters.AddWithValue("@Identifier", financialTransaction.Identifier);
                cmd.Parameters.AddWithValue("@Description", financialTransaction.Description);
                cmd.Parameters.AddWithValue("@TransactionType", financialTransaction.TransactionType);
                cmd.Parameters.AddWithValue("@PaymentType", financialTransaction.PaymentType);
                cmd.Prepare();

                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IList<Models.FinancialTransaction>> GetAsyncByDate(string month, string year)
        {
            using var connection = new MySqlConnection(Connection);

            try
            {
                connection.Open();

                var cmd = new MySqlCommand();

                cmd.CommandText = @"SELECT * FROM cost_control.financial_transaction
                                    WHERE Month(date) = @month AND YEAR(date) = @year";
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@month", month);
                cmd.Parameters.AddWithValue("@year", year);
                cmd.Prepare();

                MySqlDataReader dtreader = cmd.ExecuteReader();

                var financialTransactions = new List<Models.FinancialTransaction>();

                while (dtreader.Read())
                {
                    var financialTransaction = new Models.FinancialTransaction();

                    financialTransaction.Id = (int)dtreader[0];
                    financialTransaction.Date = (DateTime)dtreader[1];
                    financialTransaction.Amount = (decimal)dtreader[2];
                    financialTransaction.Identifier = dtreader[3].ToString();
                    financialTransaction.Description = dtreader[4].ToString();
                    financialTransaction.TransactionType = (Application.Models.Enum.TransactionType)dtreader[5];
                    financialTransaction.PaymentType = (Application.Models.Enum.PaymentType)dtreader[6];

                    financialTransactions.Add(financialTransaction);
                }

                return financialTransactions;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Models.FinancialTransaction> GetAsyncByIdentifier(string identifier)
        {
            try
            {
                using var connection = new MySqlConnection(Connection);
                connection.Open();

                var cmd = new MySqlCommand();

                cmd.CommandText = @"SELECT * FROM cost_control.financial_transaction
                                    WHERE identifier = @identifier;";
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@Identifier", identifier);
                cmd.Prepare();

                MySqlDataReader dtreader = cmd.ExecuteReader();

                var financialTransaction = new Models.FinancialTransaction();

                while (dtreader.Read())
                {
                    financialTransaction.Id = (int)dtreader[0];
                    financialTransaction.Date = (DateTime)dtreader[1];
                    financialTransaction.Amount = (decimal)dtreader[2];
                    financialTransaction.Identifier = dtreader[3].ToString();
                    financialTransaction.Description = dtreader[4].ToString();
                    financialTransaction.TransactionType = (Application.Models.Enum.TransactionType)dtreader[5];
                    financialTransaction.PaymentType = (Application.Models.Enum.PaymentType)dtreader[6];
                }

                return financialTransaction;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
