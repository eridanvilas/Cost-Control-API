using MediatR;

namespace CostControlAPI.Application.Notifications.Error
{
    public class ErrorNotification : INotification
    {
        public string Error { get; set; }
        public string StackError { get; set; }
    }
}
