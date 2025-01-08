using Android.App;
using Android.Content;
using Diploma.View;
using Diploma;
using Microsoft.Maui.Controls;
using System.Security.Cryptography.X509Certificates;


namespace Diploma.Platforms.Android;

[BroadcastReceiver(Exported = true, Enabled = true)]

public class AlarmReceiver : BroadcastReceiver
{
    public override async void OnReceive(Context context, Intent intent)
    {
        if (intent?.Extras != null)
        {
            string title = intent.GetStringExtra(NotificationManagerService.TitleKey);
            string message = intent.GetStringExtra(NotificationManagerService.MessageKey);

            NotificationManagerService manager = NotificationManagerService.Instance ?? new NotificationManagerService();
            //manager.ShowAsync(title, message);


            //  var nextPage = new NewPage();
            //  var navigationPage = new NavigationPage();
            //  await navigationPage.Navigation.PushModalAsync(nextPage);

            //await App.Current.MainPage.DisplayActionSheet("Выбрать язык", "Отмена", "Удалить", "C#", "JavaScript", "Java");

            await Shell.Current.GoToAsync("/CallPage");

        }
    }
}
