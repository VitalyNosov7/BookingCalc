using BookingCalc.Views.Interfaces;
using System.Windows;

namespace BookingCalc.Views.Services
{
    /// <summary>Взаимодействие с дополнительными окнами</summary>
     class WindowManager : IWindowManager
    {
        public void ShowOrActivate<TWindow>() where TWindow : Window, new()
        {
            // Проверяем, существует ли открытое окно TWindow
            var existing = Application.Current.Windows.OfType<TWindow>().FirstOrDefault();
            if (existing != null)
            {
                existing.Activate();
                return;
            }

            var window = new TWindow();
            window.Show();
        }
    }
}
