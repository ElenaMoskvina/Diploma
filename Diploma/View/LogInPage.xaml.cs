
namespace Diploma.View;


public partial class LogInPage : ContentPage
{
    INotificationManagerService notificationManager;
    public LogInPage(INotificationManagerService manager)
    {
        InitializeComponent();
        notificationManager = manager;
    }

    public LogInPage()
    {
        InitializeComponent();
       
    }
    async void ToMainPage (object? sender, EventArgs e)
    {
       // await Shell.Current.GoToAsync("/MainPage");
        await Navigation.PopAsync();
    }


    private async void LogIn_LogInButton_Clicked(object? sender, EventArgs e)
    {


        await Navigation.PushAsync(new MainMenuPage(notificationManager));

    }



}