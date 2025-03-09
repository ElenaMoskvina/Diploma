
using Microsoft.Data.SqlClient;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui;
using CommunityToolkit.Maui.Core.Primitives;
using System.Data;
using System.Text;
using Diploma.Models;





namespace Diploma.View;

public partial class CallPage : ContentPage
{
    
       
    
    
    public CallPage()
	{
		InitializeComponent();


        


    }



    async void CallPage_To_MainPage_ButtonClicked(object? sender, EventArgs e)
    {

        
        incomingCall.Stop();
       

        mediaElement.Stop();

        await Shell.Current.GoToAsync("/MainPage");
        // await Navigation.PopModalAsync();

        



    }

    private async void AcceptCallButton(object? sender, EventArgs e)
    {
        try
        {
            string srvrdbname = "TalkingApp";
            string srvrname = "192.168.56.1"; //"46.48.55.13,80"; //"192.168.1.58";//"192.168.56.1"; //
            string srvrusername = "diplomauser";
            string srvrpassword = "12345";
            List<CallPageModel> callPageModel = new List<CallPageModel>();

            string sqlconn = $"Data Source={srvrname};Initial Catalog={srvrdbname};User ID={srvrusername};Password={srvrpassword}; TrustServerCertificate=True; Encrypt=False";
            SqlConnection sqlConnection = new SqlConnection(sqlconn);

            sqlConnection.Open();
            string queryString = "Select ExerciseId, Audio from dbo.Exercise where ExerciseId = 3";
            SqlCommand command = new SqlCommand(queryString, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();



            while (reader.Read())
            {
                callPageModel.Add(new CallPageModel
                {
                    ExerciseId = Convert.ToInt32(reader["ExerciseID"]), //["Audio"].ToString();
                    Audio = reader["Audio"].ToString(),

                }
            );
            }
            
            reader.Close();
            sqlConnection.Close();

            
           
            var audios = from c in callPageModel select c.Audio;

            string trackUrl = callPageModel[0].Audio.ToString();
                
                

           // Console.WriteLine( frist );

            mediaElement.Source = trackUrl;


            //MyCallPageView.ItemsSource = callPageModel;

            incomingCall.Stop();

            if (mediaElement.CurrentState != MediaElementState.Playing)
            {
                mediaElement.Play();
            }

           
        }

        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

            throw;
        
        }

       


    }


}