using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diploma;


public interface INotificationManagerService
{
    event EventHandler NotificationReceived;
    void SendNotification(string title, string message, DateTime? notifyTime = null);
    
    

}