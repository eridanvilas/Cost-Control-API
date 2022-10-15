using CostControlAPI.Repositories.Contrants;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CostControlAPI.Application.Commands.FinancialTransaction.FinancialTransactionGetByDate
{
    public class FinancialTransactionGetByDateCommandHandler : IRequestHandler<FinancialTransactionGetByDateCommand, FinancialTransactionGetByDateCommandResponse>
    {

        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        private readonly IFinancialTransactionRepository _financialTransactionRepository;
        public FinancialTransactionGetByDateCommandHandler(IMediator mediator, IConfiguration configuration, IFinancialTransactionRepository financialTransactionRepository)
        {
            _mediator = mediator;
            _configuration = configuration;
            _financialTransactionRepository = financialTransactionRepository;
        }

        public async Task<FinancialTransactionGetByDateCommandResponse> Handle(FinancialTransactionGetByDateCommand request, CancellationToken cancellationToken)
        {
            var result = new FinancialTransactionGetByDateCommandResponse (await _financialTransactionRepository.GetAsyncByDate(request.Month, request.Year));
            return result;
        }
    }
}
