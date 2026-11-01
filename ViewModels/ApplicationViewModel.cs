using System.Collections.ObjectModel;
using System.IO;
using BookingCalc.Commands;
using BookingCalc.DataBase;
using BookingCalc.Models;
using BookingCalc.ViewModels.Base;
using BookingCalc.Views.Services;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

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
			}
		}

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
					  //RoomCategoryList.Clear();
					  //AccommodationTypeList.Clear();
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

		/// <summary>Загрузка списков из базы данных</summary>
		private void LoadingListsFromDatabase()
		{
			//CleanFields(); // Очищаем поля
			//LoadManagerList();  // Загрузка списка менеджеров.
			LoadSanatoriumList(); // Загрузка списка санаториев
			//LoadRoomCategoryFullList(); // Загрузка всех категорий номеров
			//LoadAccommodationTypeFullList(); // Загрузка всех видов размещения
			//LoadTarifCategoryFullList(); // Загрузка всех категорий тарифов
			//LoadBookingOperationList(); // Загрузка списка операций над бронированием вместе с текстовыми шаблонами
			//LoadTemplateVariableDictionary(); // Загрузка переменных шаблона текста сообщений в словарь
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
