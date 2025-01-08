
using Diploma.View;

namespace Diploma
{
    public partial class AppShell : Shell
    {
        public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
            BindingContext = this;
        }

        void RegisterRoutes()
        {
            Routes.Add("MainPage", typeof(MainPage));
            Routes.Add("CallPage", typeof(CallPage));
            Routes.Add("MainMenuPage", typeof(MainMenuPage));
            Routes.Add("LogInPage", typeof(LogInPage));
            Routes.Add("TimeTablePage", typeof(TimeTablePage));

            foreach (var item in Routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }
}
}