using CostControlAPI.Repositories.Contrants;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CostControlAPI.Application.Commands.FinancialTransaction.CreateFinancialTransaction
{
    public class CreateFinancialTransactionCommandHandler : IRequestHandler<CreateFinancialTransactionCommand, Unit>
    {
        private readonly IFinancialTransactionRepository _financialTransactionRepository;

        public CreateFinancialTransactionCommandHandler(IFinancialTransactionRepository financialTransactionRepository)
        {
            _financialTransactionRepository = financialTransactionRepository;
        }

        public async Task<Unit> Handle(CreateFinancialTransactionCommand request, CancellationToken cancellationToken)
        {
            await _financialTransactionRepository.CreateAsync(request.FinancialTransaction);
            return Unit.Value;
        }
    }
}
