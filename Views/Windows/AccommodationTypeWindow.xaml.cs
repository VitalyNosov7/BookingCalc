using BookingCalc.ViewModels;
using BookingCalc.Views.Services;
using System.Windows;

namespace BookingCalc.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для AccommodationTypeWindow.xaml
    /// </summary>
    public partial class AccommodationTypeWindow : Window
    {
        public AccommodationTypeWindow()
        {
            InitializeComponent();
            DataContext = new AccommodationTypeViewModel(new DialogService());
        }
    }
}
