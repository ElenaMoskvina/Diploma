using Diploma.Models;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

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
    async void SignUpPage_To_MainPage_ButtonClicked(object? sender, EventArgs e)
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
            MD5 MD5Hash = MD5.Create();
            byte[] passwordbytes = Encoding.ASCII.GetBytes(password);
            byte[] passwordBytesHash = MD5Hash.ComputeHash(passwordbytes);
            string passwordHash = Convert.ToHexString(passwordBytesHash);

            string srvrdbname = "TalkingApp";
            string srvrname = 
            string srvrusername = "";
            string srvrpassword = "";

            if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(passwordHash))

            {
                await Navigation.PopAsync();
            }
            else
            {
                List<SignUpPageModel> newUser = new List<SignUpPageModel>();

                string sqlconn = $"Data Source={srvrname};Initial Catalog={srvrdbname};User ID={srvrusername};Password={srvrpassword}; TrustServerCertificate=True; Encrypt=False";
                SqlConnection sqlConnection = new SqlConnection(sqlconn);

                sqlConnection.Open();

                using (SqlCommand command = new SqlCommand("SELECT  count(*) from Users WHERE Email = @email", sqlConnection))
                {
                    command.Parameters.AddWithValue("Email", email);
                    int rowCount = (int)command.ExecuteScalar();
                    bool emailCheck = rowCount > 0;
                   
                    if (emailCheck == true)
                    {
                        await Navigation.PopAsync();
                    }

                    else
                    {
                        using (SqlCommand insertCommand = new SqlCommand("INSERT INTO dbo.Users (Email,Password) VALUES( @Email, @Password)", sqlConnection))
                        {

                            insertCommand.Parameters.Add(new SqlParameter("Email", email));
                            insertCommand.Parameters.Add(new SqlParameter("Password", passwordHash));
                            insertCommand.ExecuteNonQuery();
                        }

                        sqlConnection.Close();

                        await Navigation.PushAsync(new MainMenuPage(notificationManager));
                    }
                }
            }
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);

            throw;
        }
    } 
}


 


