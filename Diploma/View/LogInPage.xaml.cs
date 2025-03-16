
using Diploma.Models;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace Diploma.View;

public partial class LogInPage : ContentPage
{
    INotificationManagerService notificationManager;
    public LogInPage(INotificationManagerService manager)
    {
        InitializeComponent();
        notificationManager = manager;
    }
  
    public string currentUserEmail;
    public string currentUserPassword;
    List<LogInPageModel> CurrentUser = new List<LogInPageModel>();

    public LogInPage()
    {
        InitializeComponent();
       
    }
    async void LogIn_To_MainPage_ButtonClicked (object? sender, EventArgs e)
    {
       // await Shell.Current.GoToAsync("/MainPage");
        await Navigation.PopAsync();
    }

    private async void EmailTextChanged(object? sender, TextChangedEventArgs e )
    {
        currentUserEmail = e.NewTextValue;
    }
    private async void PasswordTextChanged(object? sender, TextChangedEventArgs e)
    {
        currentUserPassword = e.NewTextValue; 
    }

    private async void LogIn_To_MainMenu_ButtonClicked(object? sender, EventArgs e)
    {

        try
        {
            string email = currentUserEmail.ToString();

            string srvrdbname = "TalkingApp";
            string srvrname = 

            string srvrusername = "";
            string srvrpassword = "";
            List<LogInPageModel> currentUser = new List<LogInPageModel>();

            currentUser.Add(new LogInPageModel { Email = "Admin", Password = "Admin" });

            string sqlconn = $"Data Source={srvrname};Initial Catalog={srvrdbname};User ID={srvrusername};Password={srvrpassword}; TrustServerCertificate=True; Encrypt=False";
           
            SqlConnection sqlConnection = new SqlConnection(sqlconn);

            using (SqlCommand command = new SqlCommand("SELECT Email, Password from Users WHERE Email = @email", sqlConnection))
            {
               // command.Parameters.AddWithValue("Email", email);
                if (email == null)
                {
                    command.Parameters.AddWithValue("Email", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("Email", email);
                }
                sqlConnection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        currentUser.Add(new LogInPageModel
                        {
                            Email = reader["Email"].ToString(),
                            Password = reader["Password"].ToString(),
                        }
                        );
                    }
                    reader.Close();
                    sqlConnection.Close();
                }
            }
               int i = currentUser.Count-1;

            if (currentUserPassword == null)
            {
                await Navigation.PopAsync();

            }
            else
            {
                string foundCurrentUserPassword = currentUser[i].Password.ToString();
                MD5 MD5Hash = MD5.Create();
                byte[] passwordbytes = Encoding.ASCII.GetBytes(currentUserPassword);
                byte[] passwordBytesHash = MD5Hash.ComputeHash(passwordbytes);
                string passwordHash = Convert.ToHexString(passwordBytesHash);
                if (foundCurrentUserPassword == passwordHash)
                {
                    await Navigation.PushAsync(new MainMenuPage(notificationManager));
                }
                else
                {
                    await Navigation.PopAsync();
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