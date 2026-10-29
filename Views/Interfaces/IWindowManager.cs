using System.Windows;

namespace BookingCalc.Views.Interfaces
{
    public interface IWindowManager
    {
        void ShowOrActivate<TWindow>() where TWindow : Window, new();
    }
}
