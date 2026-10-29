using BookingCalc.ViewModels;
using BookingCalc.Views.Windows;
using System.Windows;

namespace BookingCalc
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private new MainWindow MainWindow = new MainWindow();

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var applicationViewModel = new ApplicationViewModel(/*new WindowManager()*/);
            MainWindow.DataContext = applicationViewModel;
            MainWindow.Show();
        }
    }

}
