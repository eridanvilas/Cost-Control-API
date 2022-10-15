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
            var dataDB = await _financialTransactionRepository.GetAsyncByIdentifier(request.FinancialTransaction.Identifier);
            
            if(dataDB.Id == 0)
            {
                await _financialTransactionRepository.CreateAsync(request.FinancialTransaction);
            }
           
            return Unit.Value;
        }
    }
}
