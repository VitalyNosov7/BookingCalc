using BookingCalc.Commands;
using BookingCalc.DataBase;
using BookingCalc.Models;
using BookingCalc.ViewModels.Base;
using BookingCalc.Views.Interfaces;
using BookingCalc.Views.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace BookingCalc.ViewModels
{
    /// <summary>Модель-Представление Прайс бронирования</summary>
    public class PriceBookingViewModel : ViewModelBase
    {

        public PriceBookingViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        // Взаимодействие с диалоговыми окнами
        private readonly IDialogService _dialogService;

        private OpenWindowsCommands _openWindowsCommands = new OpenWindowsCommands(new WindowManager());

        public OpenWindowsCommands OpenWindowsCommands
        {
            get { return _openWindowsCommands; }
            set { _openWindowsCommands = value; }
        }

        /// <summary>Список прайсов бронирования</summary>
        private ObservableCollection<PriceBooking> _priceBookings = new ObservableCollection<PriceBooking>();
        /// <summary>Список прайсов бронирования</summary>
        public ObservableCollection<PriceBooking> PriceBookings
        {
            get { return _priceBookings; }
            set => Set(ref _priceBookings, value);
        }

        /// <summary>Выбранный прайс бронирования</summary>
        private PriceBooking _selectedPriceBooking = null!;
        /// <summary>Выбранный прайс бронирования</summary>
        public PriceBooking SelectedPriceBooking
        {
            get { return _selectedPriceBooking!; }
            set
            {
                Set(ref _selectedPriceBooking, value);
                //  FillPropertyPriceBooking();
            }
        }

        /// <summary>Список санаториев</summary>
        private ObservableCollection<Sanatorium> _sanatoriumList = new ObservableCollection<Sanatorium>();
        /// <summary>Список санаториев</summary>
        public ObservableCollection<Sanatorium> SanatoriumList
        {
            get { return _sanatoriumList; }
            set => Set(ref _sanatoriumList, value);
        }

        /// <summary>Выбранный санаторий</summary>
        private Sanatorium _selectedSanatorium = new Sanatorium();
        /// <summary>Выбранный санаторий</summary>
        public Sanatorium SelectedSanatorium
        {
            get { return _selectedSanatorium!; }
            set
            {
                Set(ref _selectedSanatorium, value);
                if (SelectedSanatorium != null)
                {
                    SanatoriumID = SelectedSanatorium.SanatoriumID;
                }
                else { return; }

            }
        }

        #region СВОЙСТВА PriceBooking

        ///// <summary>Идентификатор санатория</summary>
        //private Int32 _sanatoriumID = default;
        ///// <summary>Идентификатор санатория</summary>
        //public Int32 SanatoriumID
        //{
        //    get { return _sanatoriumID; }
        //    set => Set(ref _sanatoriumID, value);
        //}

        /// <summary>Идентификатор стоимости бронирования</summary>
        private Int32 _priceBookingID = default;
        /// <summary>Идентификатор стоимости бронирования</summary>
        public Int32 PriceBookingID
        {
            get { return _priceBookingID; }
            set => Set(ref _priceBookingID, value);
        }

        /// <summary>Дата вступления цены в силу</summary>
        private DateTime _priceEffectiveDate = default;
        /// <summary>Дата вступления цены в силу</summary>
        public DateTime PriceEffectiveDate
        {
            get { return _priceEffectiveDate; }
            set => Set(ref _priceEffectiveDate, value);
        }

        /// <summary>Дата начала периода стоимости бронирования</summary>
        private DateTime _startDatePeriodPrice = default;
        /// <summary>Дата начала периода стоимости бронирования</summary>
        public DateTime StartDatePeriodPrice
        {
            get { return _startDatePeriodPrice; }
            set => Set(ref _startDatePeriodPrice, value);
        }

        /// <summary>Дата окончания периода стоимости бронирования</summary>
        private DateTime _endDatePeriodPrice = default;
        /// <summary>Дата окончания периода стоимости бронирования</summary>
        public DateTime EndDatePeriodPrice
        {
            get { return _endDatePeriodPrice; }
            set => Set(ref _endDatePeriodPrice, value);
        }

        /// <summary>Стоимость 1 дня(ночи) бронирования</summary>
        private Decimal _pricePeriodDay = default;
        /// <summary>Стоимость 1 дня(ночи) бронирования</summary>
        public Decimal PricePeriodDay
        {
            get { return _pricePeriodDay; }
            set => Set(ref _pricePeriodDay, value);
        }

        /// <summary>Идентификатор санатория</summary>
        private Int32 _sanatoriumID = default;
        /// <summary>Идентификатор санатория</summary>
        public Int32 SanatoriumID
        {
            get { return _sanatoriumID; }
            set => Set(ref _sanatoriumID, value);
        }

        /// <summary>Санаторий</summary>
        private Sanatorium _currentSanatorium = null!;
        /// <summary>Санаторий</summary>
        public Sanatorium CurrentSanatorium
        {
            get { return _currentSanatorium; }
            set => Set(ref _currentSanatorium, value);
        }

        /// <summary>Идентификатор категории номера</summary>
        private Int32 _roomCategoryID = default;
        /// <summary>Идентификатор категории номера</summary>
        public Int32 RoomCategoryID
        {
            get { return _roomCategoryID; }
            set => Set(ref _roomCategoryID, value);
        }

        /// <summary>Категория номера</summary>
        private RoomCategory _currentRoomCategory = null!;
        /// <summary>Категория номера</summary>
        public RoomCategory CurrentRoomCategory
        {
            get { return _currentRoomCategory; }
            set => Set(ref _currentRoomCategory, value);
        }

        /// <summary>Идентификатор вида размещения</summary>
        private Int32 _accommodationTypeID = default;
        /// <summary>Идентификатор вида размещения</summary>
        public Int32 AccommodationTypeID
        {
            get { return _accommodationTypeID; }
            set => Set(ref _accommodationTypeID, value);
        }

        /// <summary>Вид размещения/summary>
        private AccommodationType _currentAccommodationType = null!;
        /// <summary>Вид размещения/summary>
        public AccommodationType CurrentAccommodationType
        {
            get { return _currentAccommodationType; }
            set => Set(ref _currentAccommodationType, value);
        }

        /// <summary>Идентификатор категории тарифа</summary>
        private Int32 _tarifCategoryID = default;
        /// <summary>Идентификатор категории тарифа</summary>
        public Int32 TarifCategoryID
        {
            get { return _tarifCategoryID; }
            set => Set(ref _tarifCategoryID, value);
        }

        /// <summary>Категория тарифа</summary>
        private TarifCategory _currentTarifCategory = null!;
        /// <summary>Категория тарифа</summary>
        public TarifCategory CurrentTarifCategory
        {
            get { return _currentTarifCategory; }
            set => Set(ref _currentTarifCategory, value);
        }

        /// <summary>Идентификатор целевой категории стоимости бронирования</summary>
        private Int32 _priceTargetCategoryID = default;
        /// <summary>Идентификатор целевой категории стоимости бронирования</summary>
        public Int32 PriceTargetCategoryID
        {
            get { return _priceTargetCategoryID; }
            set => Set(ref _priceTargetCategoryID, value);
        }

        /// <summary>Целевая категория стоимости бронирования</summary>
        private PriceTargetCategory _currentPriceTargetCategory = null!;
        /// <summary>Целевая категория стоимости бронирования</summary>
        public PriceTargetCategory CurrentPriceTargetCategory
        {
            get { return _currentPriceTargetCategory; }
            set => Set(ref _currentPriceTargetCategory, value);
        }

        #endregion СВОЙСТВА PriceBooking

        #region КОМАНДЫ

        // Получение данных о прайсе бронирования из базы данных
        private RelayCommand? _extractDataPriceBookingFromDBCommand;
        // Получение данных о прайсе бронирования из базы данных
        public RelayCommand ExtractDataPriceBookingFromDBCommand
        {
            get
            {
                return _extractDataPriceBookingFromDBCommand ??
                  (_extractDataPriceBookingFromDBCommand = new RelayCommand(obj =>
                  {
                      using (ApplicationContext db = new ApplicationContext())
                      {
                          //List<PriceBooking> listPriceBooking = db.PriceBookings.ToList();
                          //PriceBookings.Clear();
                          //foreach (PriceBooking priceBooking in listPriceBooking)
                          //{

                          //    Sanatorium? foundSanatorium = db.Sanatoriums.Find(priceBooking.SanatoriumID);
                          //    if (foundSanatorium != null)
                          //    {
                          //        Sanatorium createdNewSanatorium = new Sanatorium
                          //        {
                          //            SanatoriumID = foundSanatorium.SanatoriumID,
                          //            SanatoriumName = foundSanatorium.SanatoriumName,
                          //            EmailSanatorium = foundSanatorium.EmailSanatorium
                          //        };
                          //        priceBooking.CurrentSanatorium = createdNewSanatorium;
                          //        PriceBookings.Add(priceBooking);
                          //    }
                          //    else
                          //    {
                          //        MessageBox.Show("Данные о санатории не найдены в базе данных!");
                          //        return;
                          //    }

                          }

                          //SelectedAccommodationType = null!;
                          //ExtractDataSanatoriumFromDBCommand.Execute(null!);
                          //ClearPropertyAccommodationType();
                      }

                  }));
            }
        }

        #endregion КОМАНДЫ


    }
}
