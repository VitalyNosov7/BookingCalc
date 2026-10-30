using BookingCalc.ViewModels.Base;
using BookingCalc.Views.Interfaces;

namespace BookingCalc.ViewModels
{
    /// <summary>Модель-Представление Прайс бронирования</summary>
    public class PriceBookingViewModel : ViewModelBase
    {

        public PriceBookingViewModel(IDialogService dialogService)
        {            
            _dialogService = dialogService;
        }

        // Взаимодействие с диалоговыми окнами
        private readonly IDialogService _dialogService;
    }
}
