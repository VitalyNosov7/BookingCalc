using BookingCalc.Views.Interfaces;
using BookingCalc.Views.Windows;

namespace BookingCalc.Commands
{
    // В этом классе хранятся команды для открытия дополнительных окон в единственном экземпляре.
    public class OpenWindowsCommands
    {
        public OpenWindowsCommands(IWindowManager windowManager)
        {
            _windowManager = windowManager;
        }

        // Взаимодействие с дополнительными окнами
        private readonly IWindowManager _windowManager;

        // Окно для работы с данными PriceBooking из базы данных 
        private RelayCommand? openPriceBookingWindomCommand;
        public RelayCommand OpenPriceBookingWindomCommand
        {
            get
            {
                return openPriceBookingWindomCommand ??
                  (openPriceBookingWindomCommand = new RelayCommand((o) =>
                  {
                      _windowManager.ShowOrActivate<PriceBookingWindow>();
                  }));
            }
        }

        // Окно для работы с данными Sanatorium из базы данных 
        private RelayCommand? openSanatoriumWindomCommand;
        public RelayCommand OpenSanatoriumWindomCommand
        {
            get
            {
                return openSanatoriumWindomCommand ??
                  (openSanatoriumWindomCommand = new RelayCommand((o) =>
                  {
                      // App.OpenSingleInstancePersonWindow();
                      _windowManager.ShowOrActivate<SanatoriumWindow>();
                  }));
            }
        }
    }
}
