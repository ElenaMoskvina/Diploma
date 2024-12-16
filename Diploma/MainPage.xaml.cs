using Diploma.View;

namespace Diploma

{
    public partial class MainPage : ContentPage
    {
        

        public MainPage()
        {
            InitializeComponent();
            

        }
     

        private async void LogInButtonClicked(object? sender, EventArgs e)
        {

            await Navigation.PushModalAsync(new LogInPage());

        }


    }

}
