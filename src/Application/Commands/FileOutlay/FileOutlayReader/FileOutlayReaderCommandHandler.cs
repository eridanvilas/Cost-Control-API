using CostControlAPI.Application.Commands.FileOutlayReader;
using CostControlAPI.Application.Notifications.Error;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
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

        public  async Task<FileOutlayReaderCommandResponse> Handle(FileOutlayReaderCommand request, CancellationToken cancellationToken)
        {
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
                    var fullPath = filesPath + request.File.FileName;

                    using (var streamReader = new FileStream(fullPath, FileMode.Create))
                    {
                        await request.File.CopyToAsync(streamReader);
                    };
                
                    var lines = File.ReadAllLines(fullPath);

                    foreach (var line in lines)
                    {

                    }

                    File.Delete(fullPath);
                    
                }
                    return await Task.FromResult(new FileOutlayReaderCommandResponse("Arquivo Lido Com Sucesso"));
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ErrorNotification { Error = ex.Message, StackError = ex.StackTrace }, cancellationToken);
                return await Task.FromResult(new FileOutlayReaderCommandResponse("Ocorreu um erro: " + ex.Message));
            }
        }
    }
}
