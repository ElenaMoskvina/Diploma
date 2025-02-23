using Diploma.Models;
using Microsoft.Data.SqlClient;

namespace Diploma.View;

public partial class SingUpPage : ContentPage
{
    public string newUserEmail;
    public string newUserPassword;



    INotificationManagerService notificationManager;
    public SingUpPage(INotificationManagerService manager)
    {
        InitializeComponent();
        notificationManager = manager;
    }
    public SingUpPage()
    {
        InitializeComponent();
    }
    async void SignUpPage_ToMainPage(object? sender, EventArgs e)
    {
        // await Shell.Current.GoToAsync("/MainPage");
        await Navigation.PopAsync();
    }





    private async void EmailTextChanged(object? sender, TextChangedEventArgs e)
    {
        newUserEmail = e.NewTextValue;

    }


    private async void PasswordTextChanged(object? sender, TextChangedEventArgs e)
    {

        newUserPassword = e.NewTextValue;

    }



    private async void SignUp_To_MainMenu_Button_Clicked(object? sender, EventArgs e)
    {

        try
        {
            string email = newUserEmail.ToString();
            string password = newUserPassword.ToString();

            string srvrdbname = "TalkingApp";
            string srvrname = "192.168.56.1"; //"192.168.1.58";//
            string srvrusername = "diplomauser";
            string srvrpassword = "12345";

            if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password))

            {
                await Navigation.PopAsync();
            }
            else
            {

                List<SignUpPageModel> newUser = new List<SignUpPageModel>();
                string sqlconn = $"Data Source={srvrname};Initial Catalog={srvrdbname};User ID={srvrusername};Password={srvrpassword}; TrustServerCertificate=True; Encrypt=False";
                SqlConnection sqlConnection = new SqlConnection(sqlconn);




                sqlConnection.Open();
                using (SqlCommand insertCommand = new SqlCommand("INSERT INTO dbo.Users (Email,Password) VALUES( @Email, @Password)", sqlConnection))
                {

                    insertCommand.Parameters.Add(new SqlParameter("Email", email));
                    insertCommand.Parameters.Add(new SqlParameter("Password", password));
                    insertCommand.ExecuteNonQuery();
                }


                sqlConnection.Close();

                await Navigation.PushAsync(new MainMenuPage(notificationManager));

            }

        }



        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);

            throw;
        }
        } 
    }


 


