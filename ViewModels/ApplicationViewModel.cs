using BookingCalc.Commands;
using BookingCalc.DataBase;
using BookingCalc.Models;
using BookingCalc.ViewModels.Base;
using BookingCalc.Views.Services;
using NPOI.HPSF;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections.ObjectModel;
using System.IO;

namespace BookingCalc.ViewModels
{
	/// <summary>Модель-Представление главного окна</summary>
	public class ApplicationViewModel : ViewModelBase
	{
		public ApplicationViewModel()
		{
			new ApplicationContext(); // TODO: Убрать после добавления миграции БД. 
			LoadingListsFromDatabase();
		}

		// Доступ к командам, которые открывают дополнительные окна в единственном экземпляре
		private OpenWindowsCommands _openWindowsCommands = new OpenWindowsCommands(new WindowManager());
		// Доступ к командам, которые открывают дополнительные окна в единственном экземпляре
		public OpenWindowsCommands OpenWindowsCommands
		{
			get { return _openWindowsCommands; }
			set { _openWindowsCommands = value; }
		}

		/// <summary>Список расчетов бронирования</summary>
		private ObservableCollection<CalcBookingData> _calcBookings = new ObservableCollection<CalcBookingData>();
		/// <summary>Список расчетов бронирования</summary>
		public ObservableCollection<CalcBookingData> CalcBookings
		{
			get { return _calcBookings; }
			set => Set(ref _calcBookings, value);
		}

        #region САНАТОРИИ

        /// <summary>Список санаториев</summary>
        private ObservableCollection<Sanatorium> _sanatoriumList = new ObservableCollection<Sanatorium>();
		/// <summary>Список санаториев</summary>
		public ObservableCollection<Sanatorium> SanatoriumList
		{
			get { return _sanatoriumList; }
			set => Set(ref _sanatoriumList, value);
		}

		/// <summary>Выбранный санаторий</summary>
		private Sanatorium _selectedSanatorium = null!;
		/// <summary>Выбранный санаторий</summary>
		public Sanatorium SelectedSanatorium
		{
			get { return _selectedSanatorium; }
			set
			{
				Set(ref _selectedSanatorium, value);
                // CleanFields(); // Очищаем поля	
                LoadPriceBookingList();
                LoadRoomCategoryList();
                LoadAccommodationTypeList();
                LoadTarifCategoryList();
                LoadPriceEffectiveDateList();                               	
              //  LoadingListsFromDatabase();
            }
		}

        // Загрузка списка санаториев из базы данных
        private RelayCommand? _loadingSanatoriumListFromDatabaseCommand;
        public RelayCommand LoadingSanatoriumListFromDatabaseCommand
        {
            get
            {
                return _loadingSanatoriumListFromDatabaseCommand ??
                  (_loadingSanatoriumListFromDatabaseCommand = new RelayCommand(obj =>
                  {
                      // CleanFields();
                      PriceBookingList.Clear();
                      SanatoriumList.Clear();
                      RoomCategoryList.Clear();
                      AccommodationTypeList.Clear();
                      TarifCategoryList.Clear();
                      PriceEffectiveDateList.Clear();                     

                      LoadSanatoriumList();
                  }));
            }
        }

        /// <summary>Загрузка списка санаториев</summary>
        private void LoadSanatoriumList()
        {

            // Загрузка списка санаториев из базы данных.
            using (ApplicationContext db = new ApplicationContext())
            {
                var sanatoriums = db.Sanatoriums.ToList();
                //	SanatoriumList.Clear();
                foreach (Sanatorium sanatorium in sanatoriums)
                {
                    SanatoriumList.Add(sanatorium);
                }
            }
        }
        #endregion САНАТОРИИ

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
        #endregion КАТЕГОРИЯ НОМЕРА

        #region ВИД РАЗМЕЩЕНИЯ

        /// <summary>Весь список видов размещения из базы данных</summary>
        private List<AccommodationType> _accommodationTypeFullList = new List<AccommodationType>();
        /// <summary>Весь список видов размещения из базы данных</summary>
        private List<AccommodationType> AccommodationTypeFullList
        {
            get { return _accommodationTypeFullList; }
            set { _accommodationTypeFullList = value; }
        }

        /// <summary>Список видов размещения</summary>
        private ObservableCollection<AccommodationType> _accommodationTypeList = new ObservableCollection<AccommodationType>();
        /// <summary>Список видов размещения</summary>
        public ObservableCollection<AccommodationType> AccommodationTypeList
        {
            get { return _accommodationTypeList; }
            set => Set(ref _accommodationTypeList, value);
        }

        /// <summary>Выбранный вид размещения</summary>
        private AccommodationType _selectedAccommodationType = null!;
        /// <summary>Выбранный вид размещения</summary>
        public AccommodationType SelectedAccommodationType
        {
            get { return _selectedAccommodationType; }
            set => Set(ref _selectedAccommodationType, value);
        }

        /// <summary>Загрузка списка видов размещения</summary>
        private void LoadAccommodationTypeFullList()
        {

            // Загрузка всего списка видов размещения из базы данных.
            using (ApplicationContext db = new ApplicationContext())
            {
                AccommodationTypeFullList = db.AccommodationTypes.ToList();
            }
        }


        /// <summary>Загрузка списка видов размещения в зависимости от того какой санаторий был выбран</summary>
        private void LoadAccommodationTypeList()
        {
            if (SelectedSanatorium != null)
            {
                var filteredAccommodationType = AccommodationTypeFullList.Where(x => x.SanatoriumID == SelectedSanatorium.SanatoriumID).ToList();
                AccommodationTypeList = new ObservableCollection<AccommodationType>(filteredAccommodationType);
            }
            else { return; }
        }

        #endregion ВИД РАЗМЕЩЕНИЯ

        #region КАТЕГОРИЯ ТАРИФА

        /// <summary>Весь список категорий тарифов из базы данных</summary>
        private List<TarifCategory> _tarifCategoryFullList = new List<TarifCategory>();
        /// <summary>Весь список категорий тарифов из базы данных</summary>
        private List<TarifCategory> TarifCategoryFullList
        {
            get { return _tarifCategoryFullList; }
            set { _tarifCategoryFullList = value; }
        }

        /// <summary>Список категорий тарифов</summary>
        private ObservableCollection<TarifCategory> _tarifCategoryList = new ObservableCollection<TarifCategory>();
        /// <summary>Список категорий тарифов</summary>
        public ObservableCollection<TarifCategory> TarifCategoryList
        {
            get { return _tarifCategoryList; }
            set => Set(ref _tarifCategoryList, value);
        }

        /// <summary>Выбранная  категория тарифа</summary>
        private TarifCategory _selectedTarifCategory = null!;
        /// <summary>Выбранная  категория тарифа</summary>
        public TarifCategory SelectedTarifCategory
        {
            get { return _selectedTarifCategory; }
            set => Set(ref _selectedTarifCategory, value);
        }

        /// <summary>Загрузка списка категорий тарифов</summary>
        private void LoadTarifCategoryFullList()
        {

            // Загрузка всего списка категорий тарифов из базы данных.
            using (ApplicationContext db = new ApplicationContext())
            {
                TarifCategoryFullList = db.TarifCategories.ToList();
            }
        }

        /// <summary>Загрузка списка  категорий тарифов в зависимости от того какой санаторий был выбран</summary>
        private void LoadTarifCategoryList()
        {
            if (SelectedSanatorium != null)
            {
                var filteredTarifCategory = TarifCategoryFullList.Where(x => x.SanatoriumID == SelectedSanatorium.SanatoriumID).ToList();
                TarifCategoryList = new ObservableCollection<TarifCategory>(filteredTarifCategory);
            }
            else { return; }
        }

        #endregion КАТЕГОРИЯ ТАРИФА

        #region АКТУАЛЬНАЯ ДАТА ПРАЙСА

        /// <summary>Весь список дат актуальности прайсов из базы данных</summary>
        private List<DateTime> _priceEffectiveDateFullList = new List<DateTime>();
        /// <summary>Весь список дат актуальности прайсов из базы данных</summary>
        private List<DateTime> PriceEffectiveDateFullList
        {
            get { return _priceEffectiveDateFullList; }
            set { _priceEffectiveDateFullList = value; }
        }

        /// <summary>Список дат актуальности прайсов из базы данных</summary>
        private ObservableCollection<DateTime> _priceEffectiveDateList = new ObservableCollection<DateTime>();
        /// <summary>Список дат актуальности прайсов из базы данных</summary>
        public ObservableCollection<DateTime> PriceEffectiveDateList
        {
            get { return _priceEffectiveDateList; }
            set => Set(ref _priceEffectiveDateList, value);
        }

        /// <summary>Выбранная  дата актуального прайса</summary>
        private DateTime _selectedPriceEffectiveDate = default!;
        /// <summary>Выбранная  дата актуального прайса</summary>
        public DateTime SelectedPriceEffectiveDate
        {
            get { return _selectedPriceEffectiveDate; }
            set => Set(ref _selectedPriceEffectiveDate, value);
        }

        ///// <summary>Загрузка списка  дат актуальности прайсов</summary>
        //private void LoadPriceBookingFullList()
        //{

        //    // Загрузка всего списка  дат актуальности прайсов из базы данных.
        //    using (ApplicationContext db = new ApplicationContext())
        //    {
        //        //TarifCategoryFullList = db.TarifCategories.ToList();

        //        // TODO: 1 Получить полный список актуальных дат прайсов из таблицы PriceBooking  = ПРОТЕСТИРОВАТЬ!
        //        var priceEffectiveDates = from d in db.PriceBookings select d.PriceEffectiveDate;
        //        PriceEffectiveDateFullList = priceEffectiveDates.ToList();
        //    }
        //}

        /// <summary>Загрузка списка  дат актуальности прайсов в зависимости от того какой санаторий был выбран</summary>
        private void LoadPriceEffectiveDateList()
        {
            if (SelectedSanatorium != null)
            {
                //var filteredTarifCategory = TarifCategoryFullList.Where(x => x.SanatoriumID == SelectedSanatorium.SanatoriumID).ToList();
                //TarifCategoryList = new ObservableCollection<TarifCategory>(filteredTarifCategory);

                // TODO: 2. Из полного списка полученных дат выбрать  даты для текущего(выбранного) санатория

                if (SelectedSanatorium != null)
                {
                    var priceBookingLists = PriceBookingFullList.ToList();
                    var selectedPriceBookingLists = priceBookingLists.Where(p => p.SanatoriumID == SelectedSanatorium.SanatoriumID).ToList();


                    PriceEffectiveDateList = new ObservableCollection<DateTime>(priceBookingLists
                                .Where(p => p.SanatoriumID == SelectedSanatorium.SanatoriumID)          // фильтрация по идентификатору
                                .Select(p => p.PriceEffectiveDate)                                      // выбираем только даты
                                .Distinct()                                                             // убирает повторяющиеся даты
                                .ToList());                                                             // ToList() нужен, чтобы избежать проблем с отложенным выполнением и изменениями коллекции во время итерации      
                }
                else { return; }
             
            }
            else { return; }
        }

        #endregion АКТУАЛЬНАЯ ДАТА ПРАЙСА

        #region ПРАЙСЫ

        /// <summary>Весь список прайсов из базы данных</summary>
        private List<PriceBooking> _priceBookingFullList = new List<PriceBooking>();
        /// <summary>Весь список прайсов из базы данных</summary>
        private List<PriceBooking> PriceBookingFullList
        {
            get { return _priceBookingFullList; }
            set { _priceBookingFullList = value; }
        }

        /// <summary>Список прайсов из базы данных</summary>
        private ObservableCollection<PriceBooking> _priceBookingList = new ObservableCollection<PriceBooking>();
        /// <summary>Список прайсов из базы данных</summary>
        public ObservableCollection<PriceBooking> PriceBookingList
        {
            get { return _priceBookingList; }
            set => Set(ref _priceBookingList, value);
        }

        /// <summary>Выбранный прайс</summary>
        private PriceBooking _selectedPriceBooking = null!;
        /// <summary>Выбранный прайс</summary>
        public PriceBooking SelectedPriceBooking
        {
            get { return _selectedPriceBooking; }
            set => Set(ref _selectedPriceBooking, value);
        }

        /// <summary>Загрузка всего списка прайсов</summary>
        private void LoadPriceBookingFullList()
        {

            // Загрузка всего списка прайсов из базы данных.
            using (ApplicationContext db = new ApplicationContext())
            {
                PriceBookingFullList = db.PriceBookings.ToList();         
            }
        }
       
        /// <summary>Загрузка прайсов в зависимости от того какой санаторий был выбран</summary>
        private void LoadPriceBookingList()
        {
            if (SelectedSanatorium != null)
            {
                var filteredPriceBooking = PriceBookingFullList.Where(x => x.SanatoriumID == SelectedSanatorium.SanatoriumID).ToList();
                PriceBookingList = new ObservableCollection<PriceBooking>(filteredPriceBooking);
            }
            else { return; }
        }

        #endregion ПРАЙСЫ

        /// <summary>Выбранный расчет бронирования</summary>
        private CalcBookingData _selectedCalcBooking = null!;
		/// <summary>Выбранный расчет бронирования</summary>
		public CalcBookingData SelectedBookingOperation
		{
			get { return _selectedCalcBooking!; }
			set
			{
				Set(ref _selectedCalcBooking, value);
			}
		}

		// Получить расчет (выводит расчет в таблицу главного окна приложения)
		private RelayCommand? _getBookingQuoteCommand;
		public RelayCommand GetBookingQuoteCommand
		{
			get
			{
				return _getBookingQuoteCommand ??
				  (_getBookingQuoteCommand = new RelayCommand(obj =>
				  {
					  //MessageBox.Show("Проверка работы команды GetBookingQuoteCommand");
					  CalcBookings = GetObsCollectCalcBookingDataTestData();
				  }));
			}
		}


		/// <summary>Загрузка списков из базы данных</summary>
		private void LoadingListsFromDatabase()
		{
            CleanFields(); // Очищаем поля	
            LoadPriceBookingFullList(); // Загрузка всех прайсов	
            LoadSanatoriumList(); // Загрузка списка санаториев
			LoadRoomCategoryFullList(); // Загрузка всех категорий номеров
			LoadAccommodationTypeFullList(); // Загрузка всех видов размещения
			LoadTarifCategoryFullList(); // Загрузка всех категорий тарифов            
		}



        /// <summary>Очистить поля</summary>
        private void CleanFields()
        {
            SanatoriumList.Clear();
            RoomCategoryList.Clear();
            AccommodationTypeList.Clear();
            TarifCategoryList.Clear();
            PriceBookingList.Clear();
        }


        // Сохранить в Excel
        private RelayCommand? _saveToExcelCommand;
		public RelayCommand SaveToExcelCommand
		{
			get
			{
				return _saveToExcelCommand ??
				  (_saveToExcelCommand = new RelayCommand(obj =>
				  {
					  // Создаем рабочую книгу Excel
					  using var workbook = new XSSFWorkbook();
					  var sheet = workbook.CreateSheet("Лист1");

					  // Стили: дата и общий
					  var dateStyle = workbook.CreateCellStyle();
					  dateStyle.DataFormat = (short)workbook.CreateDataFormat().GetFormat("dd.MM.yyyy");

					  var headerStyle = workbook.CreateCellStyle();
					  // headerStyle.FillForegroundColor = NPOI.HPSF.HSSFColor.Grey25Percent.Index;
					  // headerStyle.FillPattern = FillPattern.SolidForeground;

					  // Заголовки
					  var headerRow = sheet.CreateRow(0);
					  string[] headers = { "ID", "Дата прайса", "Начало заезда", "Конец заезда", "Стоимость брони" };
					  for (int i = 0; i < headers.Length; i++)
					  {
						  var cell = headerRow.CreateCell(i);
						  cell.SetCellValue(headers[i]);
						  cell.CellStyle = headerStyle;						  
					  }

                      // TODO: заменить тестовые данные на рабочие!
                      CalcBookingData[] data = GetObsCollectCalcBookingDataTestData().ToArray();

					  // Данные
					  for (int i = 0; i < data.Length; i++)
					  {
						  int rowNum = i + 1;
						  var row = sheet.CreateRow(rowNum);

						  row.CreateCell(0).SetCellValue(data[i].CalcBookingDataID);

						  var priceEffectiveDateCell = row.CreateCell(1);
						  priceEffectiveDateCell.SetCellValue(data[i].PriceEffectiveDate);
						  priceEffectiveDateCell.CellStyle = dateStyle;

						  var startDateCell = row.CreateCell(2);
						  startDateCell.SetCellValue(data[i].StartDate);
						  startDateCell.CellStyle = dateStyle;

						  var endDateCell = row.CreateCell(3);
						  endDateCell.SetCellValue(data[i].EndDate);
						  endDateCell.CellStyle = dateStyle;

						  var calcResultFromGuestCell = row.CreateCell(4);
						  calcResultFromGuestCell.SetCellValue(Convert.ToDouble(data[i].CalcResultFromGuest));

						  // Сохранение
						  using var fs = new FileStream("Result_Calc.xlsx", FileMode.Create, FileAccess.Write);
						  workbook.Write(fs);
					  }

				  }));
			}
		}

        // TODO: Убрать(перенести) тестовые данные
        // Тестовые данны для таблицы в главном окне
		private ObservableCollection<CalcBookingData> GetObsCollectCalcBookingDataTestData()
		{
			ObservableCollection<CalcBookingData> result = new ObservableCollection<CalcBookingData>();

			result.Add(new CalcBookingData() { CalcBookingDataID = 1, StartDate = new DateTime(2026, 11, 01), EndDate = new DateTime(2026, 11, 11), CalcResultFromGuest = 48300 });
			result.Add(new CalcBookingData() { CalcBookingDataID = 2, StartDate = new DateTime(2026, 11, 01), EndDate = new DateTime(2026, 11, 12), CalcResultFromGuest = 53130 });
			result.Add(new CalcBookingData() { CalcBookingDataID = 3, StartDate = new DateTime(2026, 11, 01), EndDate = new DateTime(2026, 11, 13), CalcResultFromGuest = 57960 });
			result.Add(new CalcBookingData() { CalcBookingDataID = 4, StartDate = new DateTime(2026, 11, 01), EndDate = new DateTime(2026, 11, 14), CalcResultFromGuest = 62790 });
			result.Add(new CalcBookingData() { CalcBookingDataID = 5, StartDate = new DateTime(2026, 11, 01), EndDate = new DateTime(2026, 11, 15), CalcResultFromGuest = 67620 });
			result.Add(new CalcBookingData() { CalcBookingDataID = 6, StartDate = new DateTime(2026, 11, 01), EndDate = new DateTime(2026, 11, 16), CalcResultFromGuest = 72450 });

			return result;
		}
	}
}
