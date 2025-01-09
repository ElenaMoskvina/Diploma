#if ANDROID
using Diploma.Platforms.Android;
#endif


using Diploma.View;
using Microsoft.Maui.Controls;
using static Diploma.INotificationManagerService;


namespace Diploma.View;

public partial class TimeTablePage : ContentPage
{

    INotificationManagerService notificationManager;
    int notificationNumber = 0;

    

    public TimeTablePage(INotificationManagerService manager)

    {
        InitializeComponent();
        CurrentTimeLabel.Text = DateTime.Now.ToString("HH/mm/ss");
        notificationManager = manager;

    }

    


#if ANDROID
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        PermissionStatus status = await Permissions.RequestAsync<NotificationPermission>();
    }
#endif


    public void OnTimerClicked(object? sender, EventArgs e) 
    {
        notificationNumber++;
        string title = $" Notification #{notificationNumber}";
        string message = $"You have now received {notificationNumber} notifications!";
        notificationManager.SendNotification(title, message, DateTime.Now.AddSeconds(10));

    }


    async void OnBackToMainMenuClicked(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
        //await Shell.Current.GoToAsync("/MainMenuPage");

    }

}