
using Microsoft.Data.SqlClient;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui;
using CommunityToolkit.Maui.Core.Primitives;




namespace Diploma.View;

public partial class CallPage : ContentPage
{
    
    public CallPage()
	{
		InitializeComponent();
        
    }



    async void CallPage_ToMainMenuPage(object? sender, EventArgs e)
    {

        mediaElement.Stop();

        await Shell.Current.GoToAsync("/MainPage");
        // await Navigation.PopModalAsync();

           
       

    }

    private void AcceptCallButton(object? sender, EventArgs e)
    {
        try
        {
            string srvrdbname = "Diploma";
            string srvrname = " 192.168.1.58";
            string srvrusername = "diplomauser";
            string srvrpassword = "12345";
            string CurrentExercise;

            string sqlconn = $"Data Source={srvrname};Initial Catalog={srvrdbname};User ID={srvrusername};Password={srvrpassword}; TrustServerCertificate=True; Encrypt=False";
            SqlConnection sqlConnection = new SqlConnection(sqlconn);

            sqlConnection.Open();
            string queryString = "Select ExerciseId, Audio from dbo.Exercise where ExerciseId = 1";
            SqlCommand command = new SqlCommand(queryString, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CurrentExercise = reader["Audio"].ToString();
                
            }
            reader.Close();
            sqlConnection.Close();
        
            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

            throw;
        
        }




        if (mediaElement.CurrentState == MediaElementState.Stopped ||
             mediaElement.CurrentState == MediaElementState.Paused)
        {
            mediaElement.Play();
        }
             else if (mediaElement.CurrentState == MediaElementState.Playing)
        {
            mediaElement.Pause();
        }


    }


}