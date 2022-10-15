using CostControlAPI.Application.Commands.FinancialTransaction.FinancialTransactionGetByDate;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CostControlAPI.Controllers.v1.FinancialTransaction
{
    [ApiController]
    [Route("api/v1/financialtransaction")]
    [EnableCors]
    public class FinancialTransactionController : ControllerBase
    {
        private readonly ILogger<FinancialTransactionController> _logger;
        private readonly IMediator _mediator;

        public FinancialTransactionController(ILogger<FinancialTransactionController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

         
        [HttpPost("GetByDate")]
        public async Task<IActionResult> GetByDate(string month,string year)
        {
            try
            {
                var response = await _mediator.Send(new FinancialTransactionGetByDateCommand(month, year));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
