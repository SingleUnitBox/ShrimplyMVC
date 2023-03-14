using ShrimplyMVC.Enums;

namespace ShrimplyMVC.Models
{
    public class Notification
    {
        public string Message { get; set; }
        public NotificationType Type { get; set; }
    }
}
