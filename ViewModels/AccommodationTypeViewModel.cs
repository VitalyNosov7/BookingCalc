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
    public class AccommodationTypeViewModel : ViewModelBase
    {
        public AccommodationTypeViewModel(IDialogService dialogService)
        {
            ExtractDataAccommodationTypeFromDBCommand.Execute(null!);
            _dialogService = dialogService;
        }

        private readonly IDialogService _dialogService;

        private OpenWindowsCommands _openWindowsCommands = new OpenWindowsCommands(new WindowManager());

        public OpenWindowsCommands OpenWindowsCommands
        {
            get { return _openWindowsCommands; }
            set { _openWindowsCommands = value; }
        }

        /// <summary>Список видов размещения</summary>
        private ObservableCollection<AccommodationType> _accommodationTypes = new ObservableCollection<AccommodationType>();
        /// <summary>Список видов размещения</summary>
        public ObservableCollection<AccommodationType> AccommodationTypes
        {
            get { return _accommodationTypes; }
            set => Set(ref _accommodationTypes, value);
        }

        /// <summary>Выбранная категория номеров</summary>
        private AccommodationType _selectedAccommodationType = null!;
        /// <summary>Выбранная категория номеров</summary>
        public AccommodationType SelectedAccommodationType
        {
            get { return _selectedAccommodationType!; }
            set
            {
                Set(ref _selectedAccommodationType, value);
                FillPropertyAccommodationType();
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


        /// <summary>Идентификатор вида размещения</summary>
        private Int32 _accommodationTypeID;
        /// <summary>Идентификатор вида размещения</summary>
        public Int32 AccommodationTypeID
        {
            get { return _accommodationTypeID; }
            set => Set(ref _accommodationTypeID, value);
        }

        /// <summary>Идентификатор санатория</summary>
        private Int32 _sanatoriumID = default;
        /// <summary>Идентификатор санатория</summary>
        public Int32 SanatoriumID
        {
            get { return _sanatoriumID; }
            set => Set(ref _sanatoriumID, value);
        }

        /// <summary>Текущий санаторий</summary>
        private Sanatorium _currentSanatorium = null!;
        /// <summary>Текущий санаторий</summary>
        public Sanatorium CurrentSanatorium
        {
            get { return _currentSanatorium; }
            set => Set(ref _currentSanatorium, value);
        }

        /// <summary>Наименование вида размещения</summary>
        private String _accommodationTypeName = String.Empty;
        /// <summary>Наименование вида размещения</summary>
        public String AccommodationTypeName
        {
            get { return _accommodationTypeName; }
            set => Set(ref _accommodationTypeName, value);
        }

        // Получение данных о виде размещения из базы данных
        private RelayCommand? _extractDataAccommodationTypeFromDBCommand;
        // Получение данных о виде размещения из базы данных
        public RelayCommand ExtractDataAccommodationTypeFromDBCommand
        {
            get
            {
                return _extractDataAccommodationTypeFromDBCommand ??
                  (_extractDataAccommodationTypeFromDBCommand = new RelayCommand(obj =>
                  {
                      using (ApplicationContext db = new ApplicationContext())
                      {
                          List<AccommodationType> listAccommodationType = db.AccommodationTypes.ToList();
                          AccommodationTypes.Clear();
                          foreach (AccommodationType accommodationType in listAccommodationType)
                          {

                              Sanatorium? foundSanatorium = db.Sanatoriums.Find(accommodationType.SanatoriumID);
                              if (foundSanatorium != null)
                              {
                                  Sanatorium createdNewSanatorium = new Sanatorium
                                  {
                                      SanatoriumID = foundSanatorium.SanatoriumID,
                                      SanatoriumName = foundSanatorium.SanatoriumName,
                                      EmailSanatorium = foundSanatorium.EmailSanatorium
                                  };
                                  accommodationType.CurrentSanatorium = createdNewSanatorium;
                                  AccommodationTypes.Add(accommodationType);
                              }
                              else
                              {
                                  MessageBox.Show("Данные о санатории не найдены в базе данных!");
                                  return;
                              }

                          }

                          SelectedAccommodationType = null!;
                          ExtractDataSanatoriumFromDBCommand.Execute(null!);
                          ClearPropertyAccommodationType();
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
                          ClearPropertyAccommodationType();
                      }

                  }));
            }
        }


        // Добавление данных о виде размещения в базу данных
        private RelayCommand? _addAccommodationTypeFromDBCommand;
        // Добавление данных о виде размещения в базу данных
        public RelayCommand AddAccommodationTypeFromDBCommand
        {
            get
            {
                return _addAccommodationTypeFromDBCommand ??
                  (_addAccommodationTypeFromDBCommand = new RelayCommand(obj =>
                  {
                      using (ApplicationContext db = new ApplicationContext())
                      {
                          AccommodationType newAccommodationType = CreateNewAccommodationType();
                          if (newAccommodationType != null)
                          {
                              db.AccommodationTypes.Add(newAccommodationType);
                              db.SaveChanges();

                              // TODO: Подумать как еще можно обновлять данные
                              ExtractDataAccommodationTypeFromDBCommand.Execute(null!);
                          }
                          else
                          {
                              // MessageBox.Show("Данные не сохранились. Проверьте выбран ли санаторий!");
                              return;
                          }
                      }

                  }));
            }
        }


        // Редактирование данных о  виде размещения в базе данных
        private RelayCommand? _editAccommodationTypeToDBCommand;
        // Редактирование данных о  виде размещения в базе данных
        public RelayCommand EditAccommodationTypeToDBCommand
        {
            get
            {
                return _editAccommodationTypeToDBCommand ??
                  (_editAccommodationTypeToDBCommand = new RelayCommand(obj =>
                  {
                      using (ApplicationContext db = new ApplicationContext())
                      {
                          if (SelectedAccommodationType != null)
                          {
                              AccommodationType? selectedAccommodationType = SelectedAccommodationType;
                              AccommodationType? foundEditedAccommodationType = db.AccommodationTypes.Find(selectedAccommodationType.AccommodationTypeID);
                              AccommodationType editedAccommodationType = CreateNewAccommodationType();

                              if (foundEditedAccommodationType != null)
                              {
                                  bool? confirmed = _dialogService.Confirm("Подтверждение редактирования", $"Вы действительно хотите редактировать дынне виде размещения c ID «{foundEditedAccommodationType.AccommodationTypeID}», ID санатория «{foundEditedAccommodationType.SanatoriumID}» - «{foundEditedAccommodationType.AccommodationTypeName}»?");

                                  if (confirmed == true) // Если подтверждаем редактирования
                                  {
                                      // TODO: Подумать как можно сдклать валидатор
                                      if (foundEditedAccommodationType.AccommodationTypeName != foundEditedAccommodationType.AccommodationTypeName)
                                      { foundEditedAccommodationType.AccommodationTypeName = foundEditedAccommodationType.AccommodationTypeName; }

                                      db.SaveChanges();

                                      // TODO: Подумать как еще можно обновлять данные
                                      ExtractDataAccommodationTypeFromDBCommand.Execute(null!);
                                  }
                                  else { return; } // Если не подтверждаем редактирования
                              }
                          }
                          else
                          {
                              MessageBox.Show("Необходимо выбрать вид размещения из списка");
                              return;
                          }
                      }
                  }));
            }
        }



        // Удаление вида размещения из базы данных
        private RelayCommand? _deleteAccommodationTypeToDBCommand;
        // Удаление вида размещения из базы данных
        public RelayCommand DeleteAccommodationTypeToDBCommand
        {
            get
            {
                return _deleteAccommodationTypeToDBCommand ??
                  (_deleteAccommodationTypeToDBCommand = new RelayCommand(obj =>
                  {
                      using (ApplicationContext db = new ApplicationContext())
                      {
                          if (SelectedAccommodationType != null)
                          {
                              AccommodationType selectedAccommodationType = SelectedAccommodationType;
                              AccommodationType? foundDeletedAccommodationType = db.AccommodationTypes.Find(selectedAccommodationType.AccommodationTypeID);

                              if (foundDeletedAccommodationType != null)
                              {
                                  bool? confirmed = _dialogService.Confirm("Подтверждение удаления", $"Вы действительно хотите удалить вид размещения c ID «{foundDeletedAccommodationType.AccommodationTypeID}», ID санатория «{foundDeletedAccommodationType.SanatoriumID}» - «{foundDeletedAccommodationType.AccommodationTypeName}»?");

                                  if (confirmed == true) // Если подтверждаем удаление
                                  {
                                      db.AccommodationTypes.Remove(foundDeletedAccommodationType!);
                                      db.SaveChanges();

                                      // TODO: Подумать как еще можно обновлять данные
                                      ExtractDataAccommodationTypeFromDBCommand.Execute(null!);
                                  }
                                  else { return; } // Если не подтверждаем удаление
                              }
                          }
                          else
                          {
                              MessageBox.Show("Необходимо выбрать вид размещения из списка");
                              return;
                          }
                      }
                  }));
            }
        }

        // Снять выделение вида размещения
        private RelayCommand? _deselectAccommodationTypeCommand;
        // Снять выделение вида размещения
        public RelayCommand DeselectAccommodationTypeCommand
        {
            get
            {
                return _deselectAccommodationTypeCommand ??
                  (_deselectAccommodationTypeCommand = new RelayCommand(obj =>
                  {
                      SelectedAccommodationType = null!;
                      ClearPropertyAccommodationType();
                  }));
            }
        }

        // Обновление данных о видах размещения из базы данных
        private RelayCommand? _updateDataAccommodationTypeFromDBCommand;
        // Обновление данных о видах размещения из базы данных
        public RelayCommand UpdateDataAccommodationTypeFromDBCommand
        {
            get
            {
                return _updateDataAccommodationTypeFromDBCommand ??
                  (_updateDataAccommodationTypeFromDBCommand = new RelayCommand(obj =>
                  {
                      ExtractDataAccommodationTypeFromDBCommand.Execute(null!);
                  }));
            }
        }

        private AccommodationType CreateNewAccommodationType()
        {
            AccommodationType createdNewAccommodationType = null!;
            if (SanatoriumID > 0)
            {
                createdNewAccommodationType = new AccommodationType()
                { SanatoriumID = SanatoriumID, AccommodationTypeName = AccommodationTypeName };
                return createdNewAccommodationType;
            }
            else
            {
                MessageBox.Show("Необходимо выбрать санаторий");
                return createdNewAccommodationType; // Тут возвращает null
            }

        }

        /// <summary>Очистить свойства вида размещения</summary>
        private void ClearPropertyAccommodationType()
        {
            AccommodationTypeID = default;
            SanatoriumID = default;
            AccommodationTypeName = String.Empty;
        }

        /// <summary>Заполнить свойства вида размещения</summary>
        private void FillPropertyAccommodationType()
        {
            if (SelectedAccommodationType != null)
            {
                AccommodationTypeID = SelectedAccommodationType.AccommodationTypeID;
                SanatoriumID = SelectedAccommodationType.SanatoriumID;
                CurrentSanatorium = SelectedAccommodationType.CurrentSanatorium;
                AccommodationTypeName = SelectedAccommodationType.AccommodationTypeName;
            }
            else
            {
                return;
            }
        }
    }
}
