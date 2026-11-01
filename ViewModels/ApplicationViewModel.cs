using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using BookingCalc.Commands;
using BookingCalc.DataBase;
using BookingCalc.Models;
using BookingCalc.ViewModels.Base;
using BookingCalc.Views.Services;

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

		/// <summary>Выбранный расчет бронирования</summary>
		private CalcBookingData _selectedCalcBooking = null!;
		/// <summary>Выбранный расчет бронирования</summary>
		public CalcBookingData SelectedBookingOperation
		{
			get { return _selectedCalcBooking!; }
			set
			{
				Set(ref _selectedCalcBooking, value);
			}
		}

		// Получить расчет
		private RelayCommand? _getBookingQuote;
		public RelayCommand GetBookingQuote
		{
			get
			{
				return _getBookingQuote ??
				  (_getBookingQuote = new RelayCommand(obj =>
				  {
					  //MessageBox.Show("Проверка работы команды GetBookingQuote");
					  CalcBookings = GetObsCollectCalcBookingDataTestData();
				  }));
			}
		}

		private ObservableCollection<CalcBookingData> GetObsCollectCalcBookingDataTestData()
		{
			ObservableCollection<CalcBookingData>  result = new ObservableCollection<CalcBookingData>();

			result.Add(new CalcBookingData(){ CalcBookingDataID = 1, StartDate = new DateTime(2026, 11, 01), EndDate = new DateTime(2026, 11, 11), CalcResultFromGuest = 48300 });
			result.Add(new CalcBookingData() { CalcBookingDataID = 2, StartDate = new DateTime(2026, 11, 01), EndDate = new DateTime(2026, 11, 12), CalcResultFromGuest = 53130 });
			result.Add(new CalcBookingData() { CalcBookingDataID = 3, StartDate = new DateTime(2026, 11, 01), EndDate = new DateTime(2026, 11, 13), CalcResultFromGuest = 57960 });
			result.Add(new CalcBookingData() { CalcBookingDataID = 4, StartDate = new DateTime(2026, 11, 01), EndDate = new DateTime(2026, 11, 14), CalcResultFromGuest = 62790 });
			result.Add(new CalcBookingData() { CalcBookingDataID = 5, StartDate = new DateTime(2026, 11, 01), EndDate = new DateTime(2026, 11, 15), CalcResultFromGuest = 67620 });
			result.Add(new CalcBookingData() { CalcBookingDataID = 6, StartDate = new DateTime(2026, 11, 01), EndDate = new DateTime(2026, 11, 16), CalcResultFromGuest = 72450 });

			return result;
		}
	}
}
