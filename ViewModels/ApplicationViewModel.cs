using BookingCalc.Commands;
using BookingCalc.DataBase;
using BookingCalc.Models;
using BookingCalc.ViewModels.Base;
using BookingCalc.Views.Services;
using System.Collections.ObjectModel;

namespace BookingCalc.ViewModels
{
      /// <summary>Модель-Представление главного окна</summary>
    public class ApplicationViewModel : ViewModelBase
    {
        public ApplicationViewModel()
        {
            new ApplicationContext(); // TODO: Убрать после добавления миграции БД. 
        }

        // Доступ к командам, которые открывают дополнительные окна в единственном экземпляре
        private OpenWindowsCommands _openWindowsCommands = new OpenWindowsCommands(new WindowManager());
        // Доступ к командам, которые открывают дополнительные окна в единственном экземпляре
        public OpenWindowsCommands OpenWindowsCommands
        {
            get { return _openWindowsCommands; }
            set { _openWindowsCommands = value; }
        }

        /// <summary>Список расчетов бронирования</summary>
        private ObservableCollection<CalcBookingData> _calcBookings = new ObservableCollection<CalcBookingData>();
        /// <summary>Список расчетов бронирования</summary>
        public ObservableCollection<CalcBookingData> CalcBookings
        {
            get { return _calcBookings; }
            set => Set(ref _calcBookings, value);
        }
    }
}
