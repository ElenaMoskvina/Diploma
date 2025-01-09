

namespace Diploma.View;

public partial class MainMenuPage : ContentPage
{
    INotificationManagerService notificationManager;
    public MainMenuPage(INotificationManagerService manager)
    {
        InitializeComponent();
        notificationManager = manager;
    }
    public MainMenuPage()
    {
        InitializeComponent();

    }
    Си
    {
        //await Shell.Current.GoToAsync("/LogInPage");
        await Navigation.PopAsync();
    }


    private async void OnTimeTableClicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new TimeTablePage(notificationManager));
    }
    private async void Menu_ToCallPage(object? sender, EventArgs e)
    {

       await Shell.Current.GoToAsync("/CallPage");
        //await Navigation.PushAsync(new CallPage(notificationManager));
    }


}