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
            ExtractDataPriceBookingFromDBCommand.Execute(null!);
            _dialogService = dialogService;
            LoadingListsFromDatabase();
        }

        // Взаимодействие с диалоговыми окнами
        private readonly IDialogService _dialogService;

        private OpenWindowsCommands _openWindowsCommands = new OpenWindowsCommands(new WindowManager());

        public OpenWindowsCommands OpenWindowsCommands
        {
            get { return _openWindowsCommands; }
            set { _openWindowsCommands = value; }
        }

        #region ПРАЙС БРОНИРОВАНИЯ



        

        /// <summary>Список прайсов бронирования</summary>
        private ObservableCollection<PriceBooking> _priceBookingList = new ObservableCollection<PriceBooking>();
        /// <summary>Список прайсов бронирования</summary>
        public ObservableCollection<PriceBooking> PriceBookingList
        {
            get { return _priceBookingList; }
            set => Set(ref _priceBookingList, value);
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
                FillPropertyPriceBooking();
            }
        }

        /// <summary>Заполнить свойства категории номеров</summary>
        private void FillPropertyPriceBooking()
        {
            if (SelectedPriceBooking != null)
            {
                PriceBookingID = SelectedPriceBooking.PriceBookingID;
                SanatoriumID = SelectedPriceBooking.SanatoriumID;
                CurrentSanatorium = SelectedPriceBooking.CurrentSanatorium;
                // RoomCategoryName = SelectedRoomCategory.RoomCategoryName;
            }
            else
            {
                return;
            }
        }

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

        #endregion ПРАЙС БРОНИРОВАНИЯ

        // ================================================================================

        #region САНАТОРИЙ

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
                    LoadRoomCategoryList();
                }
                else { return; }

            }
        }

        #endregion САНАТОРИЙ

        // ================================================================================

        #region КАТЕГОРИЯ НОМЕРА

        /// <summary>Весь список категорий номера из базы данных</summary>
        private List<RoomCategory> _roomCategoryFullList = new List<RoomCategory>();
        /// <summary>Весь список категорий номера из базы данных</summary>
        private List<RoomCategory> RoomCategoryFullList
        {
            get { return _roomCategoryFullList; }
            set { _roomCategoryFullList = value; }
        }

        /// <summary>Список категорий номера</summary>
        private ObservableCollection<RoomCategory> _roomCategoryList = new ObservableCollection<RoomCategory>();
        /// <summary>Список категорий номера</summary>
        public ObservableCollection<RoomCategory> RoomCategoryList
        {
            get { return _roomCategoryList; }
            set => Set(ref _roomCategoryList, value);
        }

        /// <summary>Выбранная категория номера</summary>
        private RoomCategory _selectedRoomCategory = null!;
        /// <summary>Выбранная категория номера</summary>
        public RoomCategory SelectedRoomCategory
        {
            get { return _selectedRoomCategory; }

            set => Set(ref _selectedRoomCategory, value);
        }



        /// <summary>Загрузка списка категорий номеров</summary>
        private void LoadRoomCategoryFullList()
        {

            // Загрузка всего списка категорий номеров из базы данных.
            using (ApplicationContext db = new ApplicationContext())
            {
                RoomCategoryFullList = db.RoomCategories.ToList();
            }
        }

        /// <summary>Загрузка списка категорий номеров в зависимости от того какой санаторий был выбран</summary>
        private void LoadRoomCategoryList()
        {
            if (SelectedSanatorium != null)
            {
                var filteredRoomCategory = RoomCategoryFullList.Where(x => x.SanatoriumID == SelectedSanatorium.SanatoriumID).ToList();
                RoomCategoryList = new ObservableCollection<RoomCategory>(filteredRoomCategory);
            }
            else { return; }
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
        #endregion КАТЕГОРИЯ НОМЕРА

        // ================================================================================

        #region ВИД РАЗМЕЩЕНИЯ

        /// <summary>Весь список вида размещения из базы данных</summary>
        private List<AccommodationType>_accommodationTypeFullList = new List<AccommodationType>();
        /// <summary>Весь список вида размещения из базы данных</summary>
        private List<AccommodationType> AccommodationTypeFullList
        {
            get { return _accommodationTypeFullList; }
            set { _accommodationTypeFullList = value; }
        }

        /// <summary>Список вида размещения</summary>
        private ObservableCollection<AccommodationType> _accommodationTypeList = new ObservableCollection<AccommodationType>();
        /// <summary>Список вида размещения</summary>
        public ObservableCollection<AccommodationType> AccommodationTypeList
        {
            get { return _accommodationTypeList; }
            set => Set(ref _accommodationTypeList, value);
        }

        /// <summary>Выбранный вида размещения</summary>
        private AccommodationType _selectedAccommodationType = null!;
        /// <summary>Выбранный вида размещения</summary>
        public AccommodationType SelectedAccommodationType
        {
            get { return _selectedAccommodationType; }

            set => Set(ref _selectedAccommodationType, value);
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

        #endregion ВИД РАЗМЕЩЕНИЯ

        // ================================================================================

        #region СВОЙСТВА PriceBooking

        #region САНАТОРИЙ

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

        #endregion САНАТОРИЙ






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

        // ================================================================================

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
                          List<PriceBooking> priceBookingList = db.PriceBookings.ToList();
                          PriceBookingList.Clear();
                          foreach (PriceBooking priceBooking in priceBookingList)
                          {

                              Sanatorium? foundSanatorium = db.Sanatoriums.Find(priceBooking.SanatoriumID);
                              if (foundSanatorium != null)
                              {
                                  Sanatorium createdNewSanatorium = new Sanatorium
                                  {
                                      SanatoriumID = foundSanatorium.SanatoriumID,
                                      SanatoriumName = foundSanatorium.SanatoriumName,
                                      EmailSanatorium = foundSanatorium.EmailSanatorium
                                  };
                                  priceBooking.CurrentSanatorium = createdNewSanatorium;
                                  PriceBookingList.Add(priceBooking);
                              }
                              else
                              {
                                  MessageBox.Show("Данные о санатории не найдены в базе данных!");
                                  return;
                              }

                          }

                          SelectedPriceBooking = null!;
                          ExtractDataSanatoriumFromDBCommand.Execute(null!);
                          ClearPropertyPriceBooking();
                      }

                  }));
            }
        }

        // Получение данных о санаториях из базы данных
        private RelayCommand? extractDataSanatoriumFromDBCommand;
        // Получение данных о санаториях из базы данных
        public RelayCommand ExtractDataSanatoriumFromDBCommand
        {
            get
            {
                return extractDataSanatoriumFromDBCommand ??
                  (extractDataSanatoriumFromDBCommand = new RelayCommand(obj =>
                  {
                      using (ApplicationContext db = new ApplicationContext())
                      {
                          List<Sanatorium> listSanatoriums = db.Sanatoriums.ToList();
                          SanatoriumList.Clear();
                          foreach (Sanatorium sanatorium in listSanatoriums)
                          {
                              Sanatorium? foundSanatorium = db.Sanatoriums.Find(sanatorium.SanatoriumID);
                              if (foundSanatorium != null)
                              {
                                  Sanatorium createdNewSanatorium = new Sanatorium
                                  {
                                      SanatoriumID = foundSanatorium.SanatoriumID,
                                      SanatoriumName = foundSanatorium.SanatoriumName,
                                      EmailSanatorium = foundSanatorium.EmailSanatorium
                                  };

                                  SanatoriumList.Add(createdNewSanatorium);
                              }
                              else
                              {
                                  MessageBox.Show("Данные о санатории не найдены в базе данных!");
                                  return;
                              }

                          }

                          SelectedSanatorium = null!;
                          ClearPropertyPriceBooking();
                      }

                  }));
            }
        }



        #endregion КОМАНДЫ\

        // ================================================================================

        #region МЕТОДЫ

        /// <summary>Очистить свойства прайса бронирования</summary>
        private void ClearPropertyPriceBooking()
        {
            PriceBookingID = default;
            SanatoriumID = default;           
        }

        /// <summary>Загрузка списков из базы данных</summary>
        private void LoadingListsFromDatabase()
        {
            LoadRoomCategoryFullList(); // Загрузка всех категорий номеров
        }

        #endregion МЕТОДЫ

        // ================================================================================

    }
}
