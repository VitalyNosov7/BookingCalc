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
    public class RoomCategoryViewModel : ViewModelBase
    {
        public RoomCategoryViewModel(IDialogService dialogService)
        {
            ExtractDataRoomCategoryFromDBCommand.Execute(null!);
            _dialogService = dialogService;
        }

        private readonly IDialogService _dialogService;

        private OpenWindowsCommands _openWindowsCommands = new OpenWindowsCommands(new WindowManager());

        public OpenWindowsCommands OpenWindowsCommands
        {
            get { return _openWindowsCommands; }
            set { _openWindowsCommands = value; }
        }

        /// <summary>Список категорий номеров</summary>
        private ObservableCollection<RoomCategory> _roomCategories = new ObservableCollection<RoomCategory>();
        /// <summary>Список категорий номеров</summary>
        public ObservableCollection<RoomCategory> RoomCategories
        {
            get { return _roomCategories; }
            set => Set(ref _roomCategories, value);
        }

        /// <summary>Выбранная категория номеров</summary>
        private RoomCategory _selectedRoomCategory = null!;
        /// <summary>Выбранная категория номеров</summary>
        public RoomCategory SelectedRoomCategory
        {
            get { return _selectedRoomCategory!; }
            set
            {
                Set(ref _selectedRoomCategory, value);
                FillPropertyRoomCategory();
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

        /// <summary>Идентификатор категории номера санатория</summary>
        private Int32 _roomCategoryID;
        /// <summary>Идентификатор категории номера санатория</summary>
        public Int32 RoomCategoryID
        {
            get { return _roomCategoryID; }
            set => Set(ref _roomCategoryID, value);
        }

        /// <summary>Идентификатор санатория</summary>
        private Int32 _sanatoriumID;
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

        /// <summary>Наименование категории номера санатория</summary>
        private String _roomCategoryName = String.Empty;
        /// <summary>Наименование категории номера санатория</summary>
        public String RoomCategoryName
        {
            get { return _roomCategoryName; }
            set => Set(ref _roomCategoryName, value);
        }

        // Получение данных о категории номера из базы данных
        private RelayCommand? _extractDataRoomCategoryFromDBCommand;
        // Получение данных о категории номера из базы данных
        public RelayCommand ExtractDataRoomCategoryFromDBCommand
        {
            get
            {
                return _extractDataRoomCategoryFromDBCommand ??
                  (_extractDataRoomCategoryFromDBCommand = new RelayCommand(obj =>
                  {
                      using (ApplicationContext db = new ApplicationContext())
                      {
                          List<RoomCategory> listRoomCategories = db.RoomCategories.ToList();
                          RoomCategories.Clear();
                          foreach (RoomCategory roomCategory in listRoomCategories)
                          {

                              Sanatorium? foundSanatorium = db.Sanatoriums.Find(roomCategory.SanatoriumID);
                              if (foundSanatorium != null)
                              {
                                  Sanatorium createdNewSanatorium = new Sanatorium
                                  {
                                      SanatoriumID = foundSanatorium.SanatoriumID,
                                      SanatoriumName = foundSanatorium.SanatoriumName,
                                      EmailSanatorium = foundSanatorium.EmailSanatorium
                                  };
                                  roomCategory.CurrentSanatorium = createdNewSanatorium;
                                  RoomCategories.Add(roomCategory);
                              }
                              else
                              {
                                  MessageBox.Show("Данные о санатории не найдены в базе данных!");
                                  return;
                              }

                          }

                          SelectedRoomCategory = null!;
                          ExtractDataSanatoriumFromDBCommand.Execute(null!);
                          ClearPropertyRoomCategory();
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
                          ClearPropertyRoomCategory();
                      }

                  }));
            }
        }


        // Добавление данных о категории номеров в базу данных
        private RelayCommand? _addRoomCategoryFromDBCommand;
        // Добавление данных о категории номеров в базу данных
        public RelayCommand AddRoomCategoryFromDBCommand
        {
            get
            {
                return _addRoomCategoryFromDBCommand ??
                  (_addRoomCategoryFromDBCommand = new RelayCommand(obj =>
                  {
                      using (ApplicationContext db = new ApplicationContext())
                      {
                          RoomCategory newRoomCategory = CreateNewRoomCategory();
                          if (newRoomCategory != null)
                          {
                              db.RoomCategories.Add(newRoomCategory);
                              db.SaveChanges();

                              // TODO: Подумать как еще можно обновлять данные
                              ExtractDataRoomCategoryFromDBCommand.Execute(null!);
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

        // Редактирование данных о категории номеров в базе данных
        private RelayCommand? _editRoomCategoryToDBCommand;
        // Редактирование данных о категории номеров в базе данных
        public RelayCommand EditRoomCategoryToDBCommand
        {
            get
            {
                return _editRoomCategoryToDBCommand ??
                  (_editRoomCategoryToDBCommand = new RelayCommand(obj =>
                  {
                      using (ApplicationContext db = new ApplicationContext())
                      {
                          if (SelectedRoomCategory != null)
                          {
                              RoomCategory? selectedRoomCategory = SelectedRoomCategory;
                              RoomCategory? foundEditedRoomCategory = db.RoomCategories.Find(selectedRoomCategory.RoomCategoryID);
                              RoomCategory editedRoomCategory = CreateNewRoomCategory();

                              if (foundEditedRoomCategory != null)
                              {
                                  bool? confirmed = _dialogService.Confirm("Подтверждение редактирования", $"Вы действительно хотите редактировать дынне категории номеа c ID «{foundEditedRoomCategory.RoomCategoryID}», ID санатория «{foundEditedRoomCategory.SanatoriumID}» - «{foundEditedRoomCategory.RoomCategoryName}»?");

                                  if (confirmed == true) // Если подтверждаем редактирования
                                  {
                                      // TODO: Подумать как можно сдклать валидатор
                                      if (foundEditedRoomCategory.RoomCategoryName != editedRoomCategory.RoomCategoryName)
                                      { foundEditedRoomCategory.RoomCategoryName = editedRoomCategory.RoomCategoryName; }

                                      db.SaveChanges();

                                      // TODO: Подумать как еще можно обновлять данные
                                      ExtractDataRoomCategoryFromDBCommand.Execute(null!);
                                  }
                                  else { return; } // Если не подтверждаем редактирования
                              }
                          }
                          else
                          {
                              MessageBox.Show("Необходимо выбрать категорию номера из списка");
                              return;
                          }
                      }
                  }));
            }
        }


        // Удаление категории номера из базы данных
        private RelayCommand? _deleteRoomCategoryToDBCommand;
        // Удаление категории номера из базы данных
        public RelayCommand DeleteRoomCategoryToDBCommand
        {
            get
            {
                return _deleteRoomCategoryToDBCommand ??
                  (_deleteRoomCategoryToDBCommand = new RelayCommand(obj =>
                  {
                      using (ApplicationContext db = new ApplicationContext())
                      {
                          if (SelectedRoomCategory != null)
                          {
                              RoomCategory selectedRoomCategory = SelectedRoomCategory;
                              RoomCategory? foundDeletedRoomCategory = db.RoomCategories.Find(selectedRoomCategory.RoomCategoryID);

                              if (foundDeletedRoomCategory != null)
                              {
                                  bool? confirmed = _dialogService.Confirm("Подтверждение удаления", $"Вы действительно хотите удалить категорию номера c ID «{foundDeletedRoomCategory.RoomCategoryID}», ID санатория «{foundDeletedRoomCategory.SanatoriumID}» - «{foundDeletedRoomCategory.RoomCategoryName}»?");

                                  if (confirmed == true) // Если подтверждаем удаление
                                  {
                                      db.RoomCategories.Remove(foundDeletedRoomCategory!);
                                      db.SaveChanges();

                                      // TODO: Подумать как еще можно обновлять данные
                                      ExtractDataRoomCategoryFromDBCommand.Execute(null!);
                                  }
                                  else { return; } // Если не подтверждаем удаление
                              }
                          }
                          else
                          {
                              MessageBox.Show("Необходимо выбрать категорию номера из списка");
                              return;
                          }
                      }
                  }));
            }
        }


        // Снять выделение категорию номера
        private RelayCommand? _deselectRoomCategoryCommand;
        // Снять выделение категорию номера
        public RelayCommand DeselectRoomCategoryCommand
        {
            get
            {
                return _deselectRoomCategoryCommand ??
                  (_deselectRoomCategoryCommand = new RelayCommand(obj =>
                  {
                      SelectedRoomCategory = null!;
                      ClearPropertyRoomCategory();
                  }));
            }
        }

        // Обновление данных о категориях номеров из базы данных
        private RelayCommand? _updateDataRoomCategoryFromDBCommand;
        // Обновление данных о категориях номеров из базы данных
        public RelayCommand UpdateDataRoomCategoryFromDBCommand
        {
            get
            {
                return _updateDataRoomCategoryFromDBCommand ??
                  (_updateDataRoomCategoryFromDBCommand = new RelayCommand(obj =>
                  {
                      ExtractDataRoomCategoryFromDBCommand.Execute(null!);
                  }));
            }
        }


        /// <summary>Заполнить свойства категории номеров</summary>
        private void FillPropertyRoomCategory()
        {
            if (SelectedRoomCategory != null)
            {
                RoomCategoryID = SelectedRoomCategory.RoomCategoryID;
                SanatoriumID = SelectedRoomCategory.SanatoriumID;
                CurrentSanatorium = SelectedRoomCategory.CurrentSanatorium;
                RoomCategoryName = SelectedRoomCategory.RoomCategoryName;
            }
            else
            {
                return;
            }
        }

        /// <summary>Очистить свойства категории номеров</summary>
        private void ClearPropertyRoomCategory()
        {
            RoomCategoryID = default;
            SanatoriumID = default;
            RoomCategoryName = String.Empty;
        }

        private RoomCategory CreateNewRoomCategory()
        {
            RoomCategory createdNewRoomCategory = null!;
            if (SanatoriumID > 0)
            {
                //createdNewRoomCategory = new RoomCategory(SanatoriumID, RoomCategoryName);
                createdNewRoomCategory = new RoomCategory()
                { SanatoriumID = SanatoriumID, RoomCategoryName = RoomCategoryName };
                return createdNewRoomCategory;
            }
            else
            {
                MessageBox.Show("Необходимо выбрать санаторий");
                return createdNewRoomCategory;
            }

        }
    }
}
