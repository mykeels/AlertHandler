using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace AlertHandler
{
    public class AlertHandler
    {
        private const string storageKey = "alert-notifications";
        private const string storageKeyMultiple = "alert-notifications-Multi";

        private static HttpContext Context
        {
            get
            {
                return HttpContext.Current;
            }
        }
        private static HttpSessionState Session
        {
            get
            {
                return Context.Session;
            }
        }

        private static Dictionary<string, AlertNotification> MultipleNotifications
        {
            get
            {
                if (Session[storageKeyMultiple] == null)
                {
                    Session[storageKeyMultiple] = new Dictionary<string, AlertNotification>();
                }
                return (Dictionary<string, AlertNotification>)Session[storageKeyMultiple];

            }
            set
            {
                Session[storageKeyMultiple] = value;
            }
        }
        private static AlertNotification SingleNotification
        {
            get
            {
                if (Session[storageKey] == null)
                {
                    Session[storageKey] = new AlertNotification();
                }
                return (AlertNotification)Session[storageKey];
            }
            set
            {

                Session[storageKey] = value;

            }
        }

        public static void AddNotification(AlertNotification notification)
        {
            SingleNotification = notification;
        }

        public static void AddMultipleNotifications(Dictionary<string, AlertNotification> notifications)
        {
            foreach (var notification in notifications)
            {
                MultipleNotifications.Add(notification.Key, notification.Value);
            }
        }
        public static string RenderNotification()
        {
            string toastrNotificationScript = "";
            if (SingleNotification.Type == NotificationType.Success)
            {

                toastrNotificationScript = $"<script>toastr.success('{SingleNotification.Message}')</script>";
            }
            else
            {
                toastrNotificationScript = $"<script>toastr.error('{SingleNotification.Message}')</script>";
            }

            ClearSingleNotification();
            return toastrNotificationScript;
        }
        public static string RenderMultipleNotifications(string key)
        {
            string toastrNotificationScript = "";
            AlertNotification selectedNotification = MultipleNotifications.FirstOrDefault(k => k.Key == key).Value;
            if (selectedNotification.Type == NotificationType.Success)
            {

                toastrNotificationScript = $"<script>toastr.success('{selectedNotification.Message}')</script>";
            }
            else
            {
                toastrNotificationScript = $"<script>toastr.error('{selectedNotification.Message}')</script>";
            }

            ClearMultipleNotifcations();
            return toastrNotificationScript;
        }
        private static void ClearSingleNotification()
        {
            SingleNotification = new AlertNotification() { Message = "", Type = NotificationType.Empty };
        }
        private static void ClearMultipleNotifcations()
        {
            MultipleNotifications = null;
        }
    }
}
