using CostControlAPI.Application.Commands.FileOutlay.FileOutlayReader;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CostControlAPI.Application.Commands.FileOutlayReader
{
    public class FileOutlayReaderCommand : IRequest<FileOutlayReaderCommandResponse>
    {
        public IFormFile File { get; set; }
        public FileOutlayReaderCommand(IFormFile file) => File = file;
    }
}
