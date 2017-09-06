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

        private static List<AlertNotification> Notifications
        {
            get
            {
                if (Session[storageKey] == null)
                {
                    Session[storageKey] = new List<AlertNotification>();
                }
                return (List<AlertNotification>)Session[storageKey];
            }
        }

        public static void AddNotification(AlertNotification notification)
        {
            Notifications.Add(notification);
        }

        public static string RenderNotifications()
        {
            string ret = "<script>\n" +
                            "$(document).ready(function () {\n" +
                            String.Join("\n", Notifications.Select(notification => "app.notifyMe({ message: '" + notification.Message + "', type: '" + notification.Type.ToString().ToLower() + "' });")) +
                            "});\n" +
                        "</script>";
            Notifications.Clear();
            return ret;
        }

        public static string RenderNotifications(Func<List<AlertNotification>, string> templateFn)
        {
            string ret = templateFn(Notifications);
            Notifications.Clear();
            return ret;
        }
    }
}
