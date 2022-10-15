using CostControlAPI.Application.Commands.FileOutlayReader;
using CostControlAPI.Application.Commands.FinancialTransaction.CreateFinancialTransaction;
using CostControlAPI.Application.Models.Enum;
using CostControlAPI.Application.Notifications.Error;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CostControlAPI.Application.Commands.FileOutlay.FileOutlayReader
{
    public class FileOutlayReaderCommandHandler : IRequestHandler<FileOutlayReaderCommand, FileOutlayReaderCommandResponse>
    {

        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        public FileOutlayReaderCommandHandler(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        public async Task<FileOutlayReaderCommandResponse> Handle(FileOutlayReaderCommand request, CancellationToken cancellationToken)
        {
            var fullPath = "";
            try
            {
                var filesPath = _configuration.GetSection("PathFile").Value;

                if (request.File == null)
                    throw new Exception("Nenhum arquivo foi anexado..");

                var extension = Path.GetExtension(request.File.FileName);

                if (extension != ".csv")
                    throw new Exception("Arquivo nao é um formato .csv");

                if (request.File.Length > 0)
                {
                    fullPath = filesPath + request.File.FileName;

                    using (var streamReader = new FileStream(fullPath, FileMode.Create))
                    {
                        await request.File.CopyToAsync(streamReader);
                    };

                    var lines = File.ReadAllLines(fullPath);
                    var financialTransactions = new List<CostControlAPI.Models.FinancialTransaction>();

                    foreach (var line in lines)
                    {
                        if (line != lines[0])
                        {
                            var values = line.Split(',');

                            CultureInfo cultureInfo = new CultureInfo("en-GB");
                            var transactionType = GetTransactionType(values[1], values[3]);
                            financialTransactions.Add(new CostControlAPI.Models.FinancialTransaction(
                                 Convert.ToDateTime(values[0]),
                                decimal.Parse(values[1], cultureInfo),
                                values[2],
                                values[3],
                                transactionType,
                                PaymentType.Debt
                                ));
                        }

                    }
                    foreach (var financialTransaction in financialTransactions)
                    {
                        await _mediator.Send(new CreateFinancialTransactionCommand(financialTransaction));
                    }

                    File.Delete(fullPath);

                }
                return await Task.FromResult(new FileOutlayReaderCommandResponse("Arquivo Lido Com Sucesso"));
            }
            catch (Exception ex)
            {
                File.Delete(fullPath);
                await _mediator.Publish(new ErrorNotification { Error = ex.Message, StackError = ex.StackTrace }, cancellationToken);
                return await Task.FromResult(new FileOutlayReaderCommandResponse("Ocorreu um erro: " + ex.Message));
            }
        }

        public TransactionType GetTransactionType(string value, string descripton)
        {
            string banks = "BANCO GENIAL";
            if (decimal.Parse(value) < 0)
            {
                return TransactionType.Outlay;
            }
            else if (descripton.Contains(banks))
            {
                return TransactionType.Investment;
            }
            else
            {
                return TransactionType.Payment;
            }
        }
    }
}
