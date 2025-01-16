using DataAccess.Services;

namespace Week6MAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //databaseService.InitializeDatabaseAsync().Wait();
            MainPage = new MainPage();
        }
    }
}
