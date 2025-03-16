#if ANDROID
using Diploma.Models;
using Diploma.Platforms.Android;
#endif



using Diploma.Models;
using Diploma.View;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Platform;
using System.Globalization;
using static Diploma.INotificationManagerService;



namespace Diploma.View;

public partial class TimeTablePage : ContentPage
{

    INotificationManagerService notificationManager;

    
    int notificationNumber = 0;
  
    static string srvrdbname = "TalkingApp";
    static string srvrname = 
    static string srvrusername = "diplomauser";
    static string srvrpassword = "12345";
    static string sqlconn = $"Data Source={srvrname};Initial Catalog={srvrdbname};User ID={srvrusername};Password={srvrpassword}; TrustServerCertificate=True; Encrypt=False";

   
    SqlConnection sqlConnection = new SqlConnection(sqlconn);

    void OnDateChanged(object sender, DateChangedEventArgs e)
    {
        int year = e.NewDate.Year;
        int month = e.NewDate.Month;
        int day = e.NewDate.Day;
        sqlConnection.Open();
        using (SqlCommand updateCommand = new SqlCommand("update DefaultTimeTable set Year = @year, Month = @month, Day = @day", sqlConnection))
        {

            updateCommand.Parameters.Add(new SqlParameter("Year", year));
            updateCommand.Parameters.Add(new SqlParameter("Month", month));
            updateCommand.Parameters.Add(new SqlParameter("Day", day));
            updateCommand.ExecuteNonQuery();
        }

        sqlConnection.Close();

    }

    void OnTimeChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "Time")
        {
            TimeSpan selectedTime = new TimeSpan();
            selectedTime = timePicker.Time;
            int hour = selectedTime.Hours;
            int minute = selectedTime.Minutes;
            int second = 0;
            sqlConnection.Open();
            using (SqlCommand updateCommand = new SqlCommand("update DefaultTimeTable set Hour = @hour, Minute = @minute, Second  = @second", sqlConnection))
            {

                updateCommand.Parameters.Add(new SqlParameter("Hour", hour));
                updateCommand.Parameters.Add(new SqlParameter("Minute", minute));
                updateCommand.Parameters.Add(new SqlParameter("Second", second));
                updateCommand.ExecuteNonQuery();
            }

            sqlConnection.Close();

        }

    }

    public TimeTablePage(INotificationManagerService manager)

    {
        InitializeComponent();
        string cultureName = "ru-RU";
        var culture = new CultureInfo(cultureName);
        DateTime localDate = DateTime.Now;
             
        //CurrentTimeLabel.Text = $"Текущее время:\n  {localDate.ToString(culture)}";

        List<TimeTablePageModel> fromDBTime = new List<TimeTablePageModel>();
        try
        {
            sqlConnection.Open();

            using (SqlCommand command = new SqlCommand("SELECT * from DefaultTimeTable", sqlConnection))
            {

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        fromDBTime.Add(new TimeTablePageModel
                        {
                            year = Convert.ToInt16(reader["Year"]),
                            month = Convert.ToInt16(reader["Month"]),
                            day = Convert.ToInt16(reader["Day"]),
                            hour = Convert.ToInt16(reader["Hour"]),
                            minute = Convert.ToInt16(reader["Minute"]),
                            second = Convert.ToInt16(reader["Second"]),

                        }
                        );
                    }

                    reader.Close();
                    sqlConnection.Close();
                }

            }
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);

            throw;
        }

        DateTime fullDBTime = new DateTime
                  (fromDBTime[0].year,
                  fromDBTime[0].month,
                  fromDBTime[0].day,
                  fromDBTime[0].hour,
                  fromDBTime[0].minute,
                  fromDBTime[0].second);


        ExerciseTimeLabel.Text = fullDBTime.ToString(culture);

       

       
        notificationManager = manager;
        
        }


#if ANDROID
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        PermissionStatus status = await Permissions.RequestAsync<NotificationPermission>();
    }
#endif



    async public void OnTimerClicked(object? sender, EventArgs e) 
    {
        notificationNumber++;
        string title = $" Notification #{notificationNumber}";
        string message = $"You have now received {notificationNumber} notifications!";
        List<TimeTablePageModel> exerciseTime = new List<TimeTablePageModel>();
        try
        {
            sqlConnection.Open();

            using (SqlCommand command = new SqlCommand("SELECT * from DefaultTimeTable", sqlConnection))
            {

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        exerciseTime.Add(new TimeTablePageModel
                        {
                            year = Convert.ToInt16(reader["Year"]),
                            month = Convert.ToInt16(reader["Month"]),
                            day = Convert.ToInt16(reader["Day"]),
                            hour = Convert.ToInt16(reader["Hour"]),
                            minute = Convert.ToInt16(reader["Minute"]),
                            second = Convert.ToInt16(reader["Second"]),

                        }
                        );
                    }

                    reader.Close();
                    sqlConnection.Close();
                }

            }
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);

            throw;
        }

        DateTime fullExersiceTime = new DateTime
                   (exerciseTime[0].year,
                   exerciseTime[0].month, 
                   exerciseTime[0].day, 
                   exerciseTime[0].hour, 
                   exerciseTime[0].minute, 
                   exerciseTime[0].second);

        string cultureName = "ru-RU";
        var culture = new CultureInfo(cultureName);
        ExerciseTimeLabel.Text = fullExersiceTime.ToString(culture);
        
       notificationManager.SendNotification(title, message, fullExersiceTime);
        


    }
    async void TimeTable_To_Menu_ButtonCliked(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
        //await Shell.Current.GoToAsync("/MainMenuPage");

    }

}