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
    public class TarifCategoryViewModel : ViewModelBase
    {
        public TarifCategoryViewModel(IDialogService dialogService)
        {
            ExtractDataTarifCategoryFromDBCommand.Execute(null!);
            _dialogService = dialogService;
        }

        private readonly IDialogService _dialogService;

        private OpenWindowsCommands _openWindowsCommands = new OpenWindowsCommands(new WindowManager());

        public OpenWindowsCommands OpenWindowsCommands
        {
            get { return _openWindowsCommands; }
            set { _openWindowsCommands = value; }
        }

        /// <summary>Список категорий тарифа</summary>
        private ObservableCollection<TarifCategory> _tarifCategories = new ObservableCollection<TarifCategory>();
        /// <summary>Список категорий тарифа</summary>
        public ObservableCollection<TarifCategory> TarifCategories
        {
            get { return _tarifCategories; }
            set => Set(ref _tarifCategories, value);
        }

        /// <summary>Выбранная категория тарифа</summary>
        private TarifCategory _selectedTarifCategory = null!;
        /// <summary>Выбранная категория тарифа</summary>
        public TarifCategory SelectedTarifCategory
        {
            get { return _selectedTarifCategory!; }
            set
            {
                Set(ref _selectedTarifCategory, value);
                FillPropertyTarifCategory();
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


        /// <summary>Идентификатор категории тарифа санатория</summary>
        private Int32 _tarifCategoryID;
        /// <summary>Идентификатор категории тарифа санатория</summary>
        public Int32 TarifCategoryID
        {
            get { return _tarifCategoryID; }
            set => Set(ref _tarifCategoryID, value);
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

        /// <summary>Наименование категории тарифа санатория</summary>
        private String _tarifCategoryName = String.Empty;
        /// <summary>Наименование категории тарифа санатория</summary>
        public String TarifCategoryName
        {
            get { return _tarifCategoryName; }
            set => Set(ref _tarifCategoryName, value);
        }

        // Получение данных о категориях тарифа из базы данных
        private RelayCommand? _extractDataTarifCategoryFromDBCommand;
        // Получение данных о категориях тарифа из базы данных
        public RelayCommand ExtractDataTarifCategoryFromDBCommand
        {
            get
            {
                return _extractDataTarifCategoryFromDBCommand ??
                  (_extractDataTarifCategoryFromDBCommand = new RelayCommand(obj =>
                  {
                      using (ApplicationContext db = new ApplicationContext())
                      {
                          List<TarifCategory> listTarifCategory = db.TarifCategories.ToList();
                          TarifCategories.Clear();
                          foreach (TarifCategory tarifCategory in listTarifCategory)
                          {

                              Sanatorium? foundSanatorium = db.Sanatoriums.Find(tarifCategory.SanatoriumID);
                              if (foundSanatorium != null)
                              {
                                  Sanatorium createdNewSanatorium = new Sanatorium
                                  {
                                      SanatoriumID = foundSanatorium.SanatoriumID,
                                      SanatoriumName = foundSanatorium.SanatoriumName,
                                      EmailSanatorium = foundSanatorium.EmailSanatorium
                                  };
                                  tarifCategory.CurrentSanatorium = createdNewSanatorium;
                                  TarifCategories.Add(tarifCategory);
                              }
                              else
                              {
                                  MessageBox.Show("Данные о санатории не найдены в базе данных!");
                                  return;
                              }

                          }

                          SelectedTarifCategory = null!;
                          ExtractDataSanatoriumFromDBCommand.Execute(null!);
                          ClearPropertyTarifCategory();
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
                          ClearPropertyTarifCategory();
                      }

                  }));
            }
        }

        // Добавление данных о категории тарифа в базу данных
        private RelayCommand? _addTarifCategoryFromDBCommand;
        // Добавление данных о категории тарифа в базу данных
        public RelayCommand AddTarifCategoryFromDBCommand
        {
            get
            {
                return _addTarifCategoryFromDBCommand ??
                  (_addTarifCategoryFromDBCommand = new RelayCommand(obj =>
                  {
                      using (ApplicationContext db = new ApplicationContext())
                      {
                          TarifCategory newTarifCategory = CreateNewTarifCategory();
                          if (newTarifCategory != null)
                          {
                              db.TarifCategories.Add(newTarifCategory);
                              db.SaveChanges();

                              // TODO: Подумать как еще можно обновлять данные
                              ExtractDataTarifCategoryFromDBCommand.Execute(null!);
                          }
                          else
                          {
                              return;
                          }
                      }

                  }));
            }
        }


        // Редактирование данных  категории тарифа в базе данных
        private RelayCommand? _editTarifCategoryToDBCommand;
        // Редактирование данных  категории тарифа в базе данных
        public RelayCommand EditTarifCategoryToDBCommand
        {
            get
            {
                return _editTarifCategoryToDBCommand ??
                  (_editTarifCategoryToDBCommand = new RelayCommand(obj =>
                  {
                      using (ApplicationContext db = new ApplicationContext())
                      {
                          if (SelectedTarifCategory != null)
                          {
                              TarifCategory? selectedTarifCategory = SelectedTarifCategory;
                              TarifCategory? foundEditedTarifCategory = db.TarifCategories.Find(selectedTarifCategory.TarifCategoryID);
                              TarifCategory editedTarifCategory = CreateNewTarifCategory();

                              if (foundEditedTarifCategory != null)
                              {
                                  bool? confirmed = _dialogService.Confirm("Подтверждение редактирования", $"Вы действительно хотите редактировать дынне категории тарифа c ID «{foundEditedTarifCategory.TarifCategoryID}», ID санатория «{foundEditedTarifCategory.SanatoriumID}» - «{foundEditedTarifCategory.TarifCategoryName}»?");

                                  if (confirmed == true) // Если подтверждаем редактирования
                                  {
                                      // TODO: Подумать как можно сдклать валидатор
                                      if (foundEditedTarifCategory.TarifCategoryName != foundEditedTarifCategory.TarifCategoryName)
                                      { foundEditedTarifCategory.TarifCategoryName = foundEditedTarifCategory.TarifCategoryName; }

                                      db.SaveChanges();

                                      // TODO: Подумать как еще можно обновлять данные
                                      ExtractDataTarifCategoryFromDBCommand.Execute(null!);
                                  }
                                  else { return; } // Если не подтверждаем редактирования
                              }
                          }
                          else
                          {
                              MessageBox.Show("Необходимо выбрать категорию тарифа из списка");
                              return;
                          }
                      }
                  }));
            }
        }


        // Удаление категории тарифа из базы данных
        private RelayCommand? _deleteTarifCategoryToDBCommand;
        // Удаление категории тарифа из базы данных
        public RelayCommand DeleteTarifCategoryToDBCommand
        {
            get
            {
                return _deleteTarifCategoryToDBCommand ??
                  (_deleteTarifCategoryToDBCommand = new RelayCommand(obj =>
                  {
                      using (ApplicationContext db = new ApplicationContext())
                      {
                          if (SelectedTarifCategory != null)
                          {
                              TarifCategory selectedTarifCategory = SelectedTarifCategory;
                              TarifCategory? foundDeletedTarifCategory = db.TarifCategories.Find(selectedTarifCategory.TarifCategoryID);

                              if (foundDeletedTarifCategory != null)
                              {
                                  bool? confirmed = _dialogService.Confirm("Подтверждение удаления", $"Вы действительно хотите удалить категорию тарифа c ID «{foundDeletedTarifCategory.TarifCategoryID}», ID санатория «{foundDeletedTarifCategory.SanatoriumID}» - «{foundDeletedTarifCategory.TarifCategoryName}»?");

                                  if (confirmed == true) // Если подтверждаем удаление
                                  {
                                      db.TarifCategories.Remove(foundDeletedTarifCategory!);
                                      db.SaveChanges();

                                      // TODO: Подумать как еще можно обновлять данные
                                      ExtractDataTarifCategoryFromDBCommand.Execute(null!);
                                  }
                                  else { return; } // Если не подтверждаем удаление
                              }
                          }
                          else
                          {
                              MessageBox.Show("Необходимо выбрать категорию тарифа из списка");
                              return;
                          }
                      }
                  }));
            }
        }


        // Снять выделение категории тарифа
        private RelayCommand? _deselectTarifCategoryCommand;
        // Снять выделение категории тарифа
        public RelayCommand DeselectTarifCategoryCommand
        {
            get
            {
                return _deselectTarifCategoryCommand ??
                  (_deselectTarifCategoryCommand = new RelayCommand(obj =>
                  {
                      SelectedTarifCategory = null!;
                      ClearPropertyTarifCategory();
                  }));
            }
        }


        // Обновление данных о категориях тарифа из базы данных
        private RelayCommand? _updateDataTarifCategoryFromDBCommand;
        // Обновление данных о категориях тарифа из базы данных
        public RelayCommand UpdateDataTarifCategoryFromDBCommand
        {
            get
            {
                return _updateDataTarifCategoryFromDBCommand ??
                  (_updateDataTarifCategoryFromDBCommand = new RelayCommand(obj =>
                  {
                      ExtractDataTarifCategoryFromDBCommand.Execute(null!);
                  }));
            }
        }







        /// <summary>Очистить свойства категории тарифа</summary>
        private void ClearPropertyTarifCategory()
        {
            TarifCategoryID = default;
            SanatoriumID = default;
            TarifCategoryName = String.Empty;
        }

        /// <summary>Заполнить свойства категории тарифа</summary>
        private void FillPropertyTarifCategory()
        {
            if (SelectedTarifCategory != null)
            {
                TarifCategoryID = SelectedTarifCategory.TarifCategoryID;
                SanatoriumID = SelectedTarifCategory.SanatoriumID;
                CurrentSanatorium = SelectedTarifCategory.CurrentSanatorium;
                TarifCategoryName = SelectedTarifCategory.TarifCategoryName;
            }
            else
            {
                return;
            }
        }

        private TarifCategory CreateNewTarifCategory()
        {
            TarifCategory createdNewTarifCategory = null!;
            if (SanatoriumID > 0)
            {
                createdNewTarifCategory = new TarifCategory()
                { SanatoriumID = SanatoriumID, TarifCategoryName = TarifCategoryName };
                return createdNewTarifCategory;
            }
            else
            {
                MessageBox.Show("Необходимо выбрать санаторий");
                return createdNewTarifCategory; // Тут возвращает null
            }

        }
    }
}
