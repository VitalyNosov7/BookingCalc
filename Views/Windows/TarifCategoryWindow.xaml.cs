using BookingCalc.ViewModels;
using BookingCalc.Views.Services;
using System.Windows;

namespace BookingCalc.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для TarifCategoryWindow.xaml
    /// </summary>
    public partial class TarifCategoryWindow : Window
    {
        public TarifCategoryWindow()
        {
            InitializeComponent();
            DataContext = new TarifCategoryViewModel(new DialogService());
        }
    }
}
