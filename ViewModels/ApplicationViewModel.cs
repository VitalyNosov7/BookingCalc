using BookingCalc.DataBase;
using BookingCalc.ViewModels.Base;


namespace BookingCalc.ViewModels
{
    public class ApplicationViewModel : ViewModelBase
    {
        public ApplicationViewModel()
        {
            new ApplicationContext();
        }
    }
}
