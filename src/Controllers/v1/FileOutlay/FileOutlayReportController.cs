using CostControlAPI.Application.Commands.FileOutlayReader;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CostControlAPI.Controllers.v1
{
    [ApiController]
    [Route("api/v1/fileoutlay")]
    [EnableCors]
    public class FileOutlayReportController : ControllerBase
    {
        private readonly ILogger<FileOutlayReportController> _logger;
        private readonly IMediator _mediator;

        public FileOutlayReportController(ILogger<FileOutlayReportController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> UploadFile(IFormFile formFile)
        {
            try
            {
                var response = await _mediator.Send(new FileOutlayReaderCommand(formFile));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
