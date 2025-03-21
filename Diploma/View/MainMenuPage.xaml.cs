

using CommunityToolkit.Maui.Core.Primitives;

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
    async void Menu_To_LogInPage_ButtonClicked(object? sender, EventArgs e)
    {
        //await Shell.Current.GoToAsync("/LogInPage");
        await Navigation.PopAsync();
    }


    private async void Menu_To_TimeTable_ButtonClicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new TimeTablePage(notificationManager));
    }
    private async void Menu_To_CallPage_ButtonClicked(object? sender, EventArgs e)
    {

       await Shell.Current.GoToAsync("/CallPage");
        //await Navigation.PushAsync(new CallPage(notificationManager));
       
       

    }


}