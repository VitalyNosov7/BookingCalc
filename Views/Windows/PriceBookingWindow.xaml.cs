
using BookingCalc.ViewModels;
using BookingCalc.Views.Services;
using System.Windows;


namespace BookingCalc.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для PriceBookingWindow.xaml
    /// </summary>
    public partial class PriceBookingWindow : Window
    {
        public PriceBookingWindow()
        {
            InitializeComponent();
            DataContext = new PriceBookingViewModel(new DialogService());
        }
    }
}
