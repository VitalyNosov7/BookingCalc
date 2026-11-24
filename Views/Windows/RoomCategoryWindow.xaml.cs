using BookingCalc.ViewModels;
using BookingCalc.Views.Services;
using System.Windows;

namespace BookingCalc.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для RoomCategoryWindow.xaml
    /// </summary>
    public partial class RoomCategoryWindow : Window
    {
        public RoomCategoryWindow()
        {
            InitializeComponent();
            DataContext = new RoomCategoryViewModel(new DialogService());
        }
    }
}
