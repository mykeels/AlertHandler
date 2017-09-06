using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlertHandler
{
    public class AlertNotification
    {
        public string Message { get; set; }
        public NotificationType Type { get; set; }

        public enum NotificationType
        {
            Error,
            Success,
            Warning
        }

        public static AlertNotification Success(string message)
        {
            return new AlertNotification()
            {
                Type = NotificationType.Success,
                Message = message
            };
        }

        public static AlertNotification Warning(string message)
        {
            return new AlertNotification()
            {
                Type = NotificationType.Warning,
                Message = message
            };
        }

        public static AlertNotification Error(string message)
        {
            return new AlertNotification()
            {
                Type = NotificationType.Error,
                Message = message
            };
        }
    }
}
