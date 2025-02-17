#if ANDROID
using Diploma.Platforms.Android;
#endif


using Diploma.View;
using Microsoft.Maui.Controls;
using static Diploma.INotificationManagerService;



namespace Diploma

{
    public partial class MainPage : ContentPage
    {
        INotificationManagerService notificationManager;
        int notificationNumber = 0;

        public MainPage(INotificationManagerService manager)
        {
            InitializeComponent();
            notificationManager = manager;
         
        }

#if ANDROID
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        PermissionStatus status = await Permissions.RequestAsync<NotificationPermission>();
    }
#endif

       

        //void OnScheduleClick(object sender, EventArgs e)
        //{
        //    notificationNumber++;
        //    string title = $" Notification #{notificationNumber}";
        //    string message = $"You have now received {notificationNumber} notifications!";
        //    notificationManager.SendNotification(title, message, DateTime.Now.AddSeconds(10));
        //}





        private async void LogInButtonClicked(object? sender, EventArgs e)
        {
          
            
          await Navigation.PushAsync(new LogInPage(notificationManager));
         // await Shell.Current.GoToAsync("/LogInPage(notificatoinManager)");
            
        }

        private async void SingUpButtonClicked(object? sender, EventArgs e)
        {


            await Navigation.PushAsync(new SingUpPage(notificationManager));
            // await Shell.Current.GoToAsync("/LogInPage(notificatoinManager)");

        }

       



    }

}
