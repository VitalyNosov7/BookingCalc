using BookingCalc.Views.Interfaces;
using System.Windows;

namespace BookingCalc.Views.Services
{
    public class DialogService : IDialogService
    {
        public bool? Confirm(String title, String message)
        {
            return MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
        }
    }
}
