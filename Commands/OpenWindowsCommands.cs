using BookingCalc.Views.Interfaces;
using BookingCalc.Views.Windows;
namespace BookingCalc.Commands
{
    public class OpenWindowsCommands
    {
        public OpenWindowsCommands(IWindowManager windowManager)
        {
            _windowManager = windowManager;

        }
        private readonly IWindowManager _windowManager;

        // Окно для работы с данными Person из базы данных 
        private RelayCommand? openPriceBookingWindomCommand;
        public RelayCommand OpenPriceBookingWindomCommand
        {
            get
            {
                return openPriceBookingWindomCommand ??
                  (openPriceBookingWindomCommand = new RelayCommand((o) =>
                  {
                      // App.OpenSingleInstancePersonWindow();
                      _windowManager.ShowOrActivate<PriceBookingWindow>();
                  }));
            }
        }
    }
}
