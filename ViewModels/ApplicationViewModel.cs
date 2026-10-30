using BookingCalc.Commands;
using BookingCalc.DataBase;
using BookingCalc.ViewModels.Base;
using BookingCalc.Views.Services;


namespace BookingCalc.ViewModels
{
    public class ApplicationViewModel : ViewModelBase
    {
        public ApplicationViewModel()
        {
            new ApplicationContext();
        }

        private OpenWindowsCommands _openWindowsCommands = new OpenWindowsCommands(new WindowManager());

        public OpenWindowsCommands OpenWindowsCommands
        {
            get { return _openWindowsCommands; }
            set { _openWindowsCommands = value; }
        }
    }
}
