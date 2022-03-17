using System.Collections.Generic;
using TruckControl.Business.Shared.Validator.Interface;

namespace TruckControl.Business.Shared.Validator
{
    public class Notifiable : INotifiable
    {
        private List<string> _notifications = new List<string>();

        public bool Valid => _notifications.Count == 0;

        public List<string> Notifications => _notifications;

        protected void AddNotifications(List<string> notifications)
        {
            if (notifications is null)
                return;

            _notifications.AddRange(notifications);
        }

        protected void AddNotification(string notification)
        {
            if (notification is null)
                return;

            _notifications.Add(notification);
        }
    }
}
