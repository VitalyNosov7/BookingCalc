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
                LoadRoomCategoryList();
                LoadAccommodationTypeList();
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
                      SanatoriumList.Clear();
                      RoomCategoryList.Clear();
                      AccommodationTypeList.Clear();
                      //TarifCategoryList.Clear();
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

		// Получить расчет
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
			//LoadManagerList();  // Загрузка списка менеджеров.
			LoadSanatoriumList(); // Загрузка списка санаториев
			LoadRoomCategoryFullList(); // Загрузка всех категорий номеров
			LoadAccommodationTypeFullList(); // Загрузка всех видов размещения
			//LoadTarifCategoryFullList(); // Загрузка всех категорий тарифов
			//LoadBookingOperationList(); // Загрузка списка операций над бронированием вместе с текстовыми шаблонами
			//LoadTemplateVariableDictionary(); // Загрузка переменных шаблона текста сообщений в словарь
		}



        /// <summary>Очистить поля</summary>
        private void CleanFields()
        {
            //ResultTemplate = String.Empty;
            //FileName = String.Empty;
            //ManagerList.Clear();
            SanatoriumList.Clear();
            RoomCategoryList.Clear();
            AccommodationTypeList.Clear();
            //TarifCategoryList.Clear();
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
					  // MessageBox.Show("Проверка работы команды SaveToExcelCommand");	

					  // Пример создания и записи в файл Excel

					  // var data = new[]
					  // {
					  //	new { Id = 1, Name = "Анна", Date = DateTime.Today },
					  //	new { Id = 2, Name = "Борис", Date = DateTime.Today.AddDays(1) },
					  //	new { Id = 3, Name = "Виктор", Date = DateTime.Today.AddDays(2) },
					  //	new { Id = 4, Name = "Захар", Date = DateTime.Today.AddDays(3) }
					  // };

					  // // Создаем рабочую книгу Excel
					  // using var workbook = new XSSFWorkbook();
					  // var sheet = workbook.CreateSheet("Лист1");

					  // // Стили: дата и общий
					  // var dateStyle = workbook.CreateCellStyle();
					  // dateStyle.DataFormat = (short)workbook.CreateDataFormat().GetFormat("dd.MM.yyyy");

					  // var headerStyle = workbook.CreateCellStyle();
					  // // headerStyle.FillForegroundColor = NPOI.HPSF.HSSFColor.Grey25Percent.Index;
					  //// headerStyle.FillPattern = FillPattern.SolidForeground;

					  // // Заголовки
					  // var headerRow = sheet.CreateRow(0);
					  // string[] headers = { "ID", "Имя", "Дата" };
					  // for (int i = 0; i < headers.Length; i++)
					  // {
					  //  var cell = headerRow.CreateCell(i);
					  //  cell.SetCellValue(headers[i]);
					  //  cell.CellStyle = headerStyle;
					  // }

					  // // Данные
					  // for (int i = 0; i < data.Length; i++)
					  // {
					  //  int rowNum = i + 1;
					  //  var row = sheet.CreateRow(rowNum);

					  //  row.CreateCell(0).SetCellValue(data[i].Id);

					  //  var nameCell = row.CreateCell(1);
					  //  nameCell.SetCellValue(data[i].Name ?? "");

					  //  var dateCell = row.CreateCell(2);
					  //  dateCell.SetCellValue(data[i].Date);
					  //  dateCell.CellStyle = dateStyle;
					  // }

					  // //// Автоподбор ширины колонок
					  // //for (int i = 0; i < headers.Length; i++)
					  // // sheet.AutoSizeColumn(i);

					  // // Сохранение
					  // using var fs = new FileStream("data_npoi.xlsx", FileMode.Create, FileAccess.Write);
					  // workbook.Write(fs);

					  // =============================================

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

					  CalcBookingData [] data = GetObsCollectCalcBookingDataTestData().ToArray();

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
