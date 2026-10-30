using BookingCalc.Commands;
using BookingCalc.DataBase;
using BookingCalc.ViewModels.Base;
using BookingCalc.Views.Services;

namespace BookingCalc.ViewModels
{
      /// <summary>Модель-Представление главного окна</summary>
    public class ApplicationViewModel : ViewModelBase
    {
        public ApplicationViewModel()
        {
            new ApplicationContext(); // TODO: Убрать после добавления миграции БД. 
        }

        // Доступ к командам, которые открывают дополнительные окна в единственном экземпляре
        private OpenWindowsCommands _openWindowsCommands = new OpenWindowsCommands(new WindowManager());
        // Доступ к командам, которые открывают дополнительные окна в единственном экземпляре
        public OpenWindowsCommands OpenWindowsCommands
        {
            get { return _openWindowsCommands; }
            set { _openWindowsCommands = value; }
        }
    }
}
