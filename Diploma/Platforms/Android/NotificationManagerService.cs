using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.Core.App;
using Diploma.View;

namespace Diploma.Platforms.Android;

public class NotificationManagerService : INotificationManagerService
{

    public const string TitleKey = "title";
    public const string MessageKey = "message";

    bool channelInitialized = false;
    int messageId = 0;
    int pendingIntentId = 0;

    NotificationManagerCompat compatManager;

    public event EventHandler NotificationReceived;

    public static NotificationManagerService Instance { get; private set; }

    public NotificationManagerService()
    {
        if (Instance == null)
        {
            compatManager = NotificationManagerCompat.From(Platform.AppContext);
            Instance = this;
        }
    }

    public void SendNotification(string title, string message, DateTime? notifyTime = null)
    {
        if (notifyTime != null)
        {
            Intent intent = new Intent(Platform.AppContext, typeof(AlarmReceiver));
            intent.PutExtra(TitleKey, title);
            intent.PutExtra(MessageKey, message);
            intent.SetFlags(ActivityFlags.SingleTop | ActivityFlags.ClearTop);

            var pendingIntentFlags = (Build.VERSION.SdkInt >= BuildVersionCodes.S)
                ? PendingIntentFlags.CancelCurrent | PendingIntentFlags.Immutable
                : PendingIntentFlags.CancelCurrent;

            PendingIntent pendingIntent = PendingIntent.GetBroadcast(Platform.AppContext, pendingIntentId++, intent, pendingIntentFlags);
            long interval = 10 * 1000;
            AlarmManager alarmManager = Platform.AppContext.GetSystemService(Context.AlarmService) as AlarmManager;
            alarmManager.Set(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime()+interval, pendingIntent);
        }
        else
        {
           
        }
    }

      

    //long GetNotifyTime(DateTime notifyTime)
    //{
    //    DateTime utcTime = TimeZoneInfo.ConvertTimeToUtc(notifyTime);
    //    double epochDiff = (new DateTime(1970, 1, 1) - DateTime.MinValue).TotalSeconds;
    //    long utcAlarmTime = utcTime.AddSeconds(-epochDiff).Ticks / 10000;
    //    return utcAlarmTime; // milliseconds
    //}
}
