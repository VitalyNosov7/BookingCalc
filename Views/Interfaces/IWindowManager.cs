using System.Windows;

namespace BookingCalc.Views.Interfaces
{
   /// <summary>Интерфейс для взаимодействия с дополнительными окнами</summary>
    public interface IWindowManager
    {
        void ShowOrActivate<TWindow>() where TWindow : Window, new();
    }
}
