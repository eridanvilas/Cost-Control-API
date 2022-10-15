namespace CostControlAPI.Application.Commands.FileOutlay.FileOutlayReader
{
    public class FileOutlayReaderCommandResponse
    {
        public string Result { get; set; }
        public FileOutlayReaderCommandResponse(string result) => Result = result;
    }
}
