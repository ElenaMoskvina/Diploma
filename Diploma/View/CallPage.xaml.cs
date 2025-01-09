



namespace Diploma.View;

public partial class CallPage : ContentPage
{
    
    public CallPage()
	{
		InitializeComponent();
        
    }



    async void CallPage_ToMainMenuPage(object? sender, EventArgs e)
    {

        await Shell.Current.GoToAsync("/MainPage");
       // await Navigation.PopModalAsync();

    }
}