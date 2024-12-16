namespace Diploma.View;

public partial class LogInPage : ContentPage
{
    public LogInPage()
    {
        InitializeComponent();

    }

    
    async void ToMainPage (object? sender, EventArgs e)    
    {
             await Navigation.PopModalAsync();

       }

}