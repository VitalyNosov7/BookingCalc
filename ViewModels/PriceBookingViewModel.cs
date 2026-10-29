using BookingCalc.Commands;
using BookingCalc.ViewModels.Base;
using BookingCalc.Views.Interfaces;
using BookingCalc.Views.Services;

namespace BookingCalc.ViewModels
{
    public class PriceBookingViewModel : ViewModelBase
    {

        public PriceBookingViewModel(IDialogService dialogService)
        {            
            _dialogService = dialogService;
        }

        private readonly IDialogService _dialogService;

        private OpenWindowsCommands _openWindowsCommands = new OpenWindowsCommands(new WindowManager());

        public OpenWindowsCommands OpenWindowsCommands
        {
            get { return _openWindowsCommands; }
            set { _openWindowsCommands = value; }
        }
    }
}
