using BookingCalc.ViewModels;
using BookingCalc.Views.Services;
using System.Windows;


namespace BookingCalc.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для PriceTargetCategoryWindow.xaml
    /// </summary>
    public partial class PriceTargetCategoryWindow : Window
    {
        public PriceTargetCategoryWindow()
        {
            InitializeComponent();
            DataContext = new PriceTargetCategoryViewModel(new DialogService());
        }
    }
}
