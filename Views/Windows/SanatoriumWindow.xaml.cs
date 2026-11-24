using BookingCalc.ViewModels;
using BookingCalc.Views.Services;
using System.Windows;

namespace BookingCalc.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для SanatoriumWindow.xaml
    /// </summary>
    public partial class SanatoriumWindow : Window
    {
        public SanatoriumWindow()
        {
            InitializeComponent();
            DataContext = new SanatoriumViewModel(new DialogService());
        }
    }
}
