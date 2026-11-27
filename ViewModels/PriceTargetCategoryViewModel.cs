using BookingCalc.Commands;
using BookingCalc.DataBase;
using BookingCalc.Models;
using BookingCalc.ViewModels.Base;
using BookingCalc.Views.Interfaces;
using System.Collections.ObjectModel;
using System.Windows;

namespace BookingCalc.ViewModels
{
    public class PriceTargetCategoryViewModel : ViewModelBase
    {

        public PriceTargetCategoryViewModel(IDialogService dialogService)
        {
            ExtractDataPriceTargetCategoryFromDBCommand.Execute(null!);
            _dialogService = dialogService;
        }

        private readonly IDialogService _dialogService;

        /// <summary>Список целевых категорий прайсов</summary>
        private ObservableCollection<PriceTargetCategory> _priceTargetCategoryList = new ObservableCollection<PriceTargetCategory>();
        /// <summary>Список целевых категорий прайсов</summary>
        public ObservableCollection<PriceTargetCategory> PriceTargetCategoryList
        {
            get { return _priceTargetCategoryList; }
            set => Set(ref _priceTargetCategoryList, value);
        }

        /// <summary>Идентификатор целевой категории прайса</summary>
        private Int32 _priceTargetCategoryID = default;
        /// <summary>Идентификатор целевой категории прайса</summary>
        public Int32 PriceTargetCategoryID
        {
            get { return _priceTargetCategoryID; }
            set => Set(ref _priceTargetCategoryID, value);
        }

        /// <summary>Название  целевой категории прайса</summary>
        private String _priceTargetCategoryName = String.Empty;

        /// <summary>Название  целевой категории прайса</summary>
        public String PriceTargetCategoryName
        {
            get { return _priceTargetCategoryName; }
            set => Set(ref _priceTargetCategoryName, value);
        }

        /// <summary>Описание целевой категории прайса</summary>
        private String _priceTargetCategoryDescription = String.Empty;

        /// <summary>Описание  целевой категории прайса</summary>
        public String PriceTargetCategoryDescription
        {
            get { return _priceTargetCategoryDescription; }
            set => Set(ref _priceTargetCategoryDescription, value);
        }

        /// <summary>Выбранная целевая категория прайса</summary>
        private PriceTargetCategory _selectedPriceTargetCategory = null!;
        /// <summary>Выбранная целевая категория прайса</summary>
        public PriceTargetCategory SelectedPriceTargetCategory
        {
            get { return _selectedPriceTargetCategory!; }
            set
            {
                Set(ref _selectedPriceTargetCategory, value);
                FillPropertyPriceTargetCategory();
            }
        }

        // Получение данных о целевых категориях прайсов из базы данных
        private RelayCommand? _extractDataPriceTargetCategoryFromDBCommand;
        // Получение данных о целевых категориях прайсов из базы данных
        public RelayCommand ExtractDataPriceTargetCategoryFromDBCommand
        {
            get
            {
                return _extractDataPriceTargetCategoryFromDBCommand ??
                  (_extractDataPriceTargetCategoryFromDBCommand = new RelayCommand(obj =>
                  {
                      using (ApplicationContext db = new ApplicationContext())
                      {
                          List<PriceTargetCategory> listPriceTargetCategory = db.PriceTargetCategories.ToList();
                          PriceTargetCategoryList.Clear();
                          foreach (PriceTargetCategory priceTargetCategory in listPriceTargetCategory)
                          {
                              PriceTargetCategory? foundPriceTargetCategory = db.PriceTargetCategories.Find(priceTargetCategory.PriceTargetCategoryID);
                              if (foundPriceTargetCategory != null)
                              {
                                  PriceTargetCategory createdNewSanatorium = new PriceTargetCategory
                                  {                                    
                                      PriceTargetCategoryID = foundPriceTargetCategory.PriceTargetCategoryID,
                                      PriceTargetCategoryName = foundPriceTargetCategory.PriceTargetCategoryName,
                                      PriceTargetCategoryDescription = foundPriceTargetCategory.PriceTargetCategoryDescription
                                  };

                                  PriceTargetCategoryList.Add(createdNewSanatorium);
                              }
                              else
                              {
                                  MessageBox.Show("Данные о санатории не найдены в базе данных!");
                                  return;
                              }

                          }

                          SelectedPriceTargetCategory = null!;
                          ClearPropertyPriceTargetCategory();
                      }

                  }));
            }
        }

        // Добавление данных о целевой категории прайса в базу данных
        private RelayCommand? _addPriceTargetCategoryToDBCommand;
        // Добавление данных о целевой категории прайса в базу данных
        public RelayCommand AddPriceTargetCategoryToDBCommand
        {
            get
            {
                return _addPriceTargetCategoryToDBCommand ??
                  (_addPriceTargetCategoryToDBCommand = new RelayCommand(obj =>
                  {
                      using (ApplicationContext db = new ApplicationContext())
                      {
                          PriceTargetCategory newPriceTargetCategory = CreateNewPriceTargetCategory();
                          db.PriceTargetCategories.Add(newPriceTargetCategory);
                          db.SaveChanges();

                          // TODO: Подумать как еще можно обновлять данные
                          ExtractDataPriceTargetCategoryFromDBCommand.Execute(null!);
                      }
                  }));
            }
        }


        // Редактирование данных о целевой категории прайса в базе данных
        private RelayCommand? _editPriceTargetCategoryToDBCommand;
        // Редактирование данных о целевой категории прайса в базе данных
        public RelayCommand EditPriceTargetCategoryToDBCommand
        {
            get
            {
                return _editPriceTargetCategoryToDBCommand ??
                  (_editPriceTargetCategoryToDBCommand = new RelayCommand(obj =>
                  {
                      using (ApplicationContext db = new ApplicationContext())
                      {
                          if (SelectedPriceTargetCategory != null)
                          {
                              PriceTargetCategory? selectedPriceTargetCategory = SelectedPriceTargetCategory;
                              PriceTargetCategory editedPriceTargetCategory = CreateNewPriceTargetCategory();
                              PriceTargetCategory? foundEditedPriceTargetCategory = db.PriceTargetCategories.Find(selectedPriceTargetCategory.PriceTargetCategoryID);

                              if (foundEditedPriceTargetCategory != null)
                              {
                                  bool? confirmed = _dialogService.Confirm("Подтверждение редактирования", $"Вы действительно хотите редактировать дынне целевой категории прайса c ID «{foundEditedPriceTargetCategory.PriceTargetCategoryID}» - «{foundEditedPriceTargetCategory.PriceTargetCategoryName}»?");

                                  if (confirmed == true) // Если подтверждаем редактирования
                                  {
                                      // TODO: Подумать как можно сдклать валидатор
                                      if (foundEditedPriceTargetCategory.PriceTargetCategoryName != editedPriceTargetCategory.PriceTargetCategoryName)
                                      { foundEditedPriceTargetCategory.PriceTargetCategoryName = editedPriceTargetCategory.PriceTargetCategoryName; }
                                      if (foundEditedPriceTargetCategory.PriceTargetCategoryDescription != editedPriceTargetCategory.PriceTargetCategoryDescription)
                                      { foundEditedPriceTargetCategory.PriceTargetCategoryDescription = editedPriceTargetCategory.PriceTargetCategoryDescription; }

                                      db.SaveChanges();

                                      // TODO: Подумать как еще можно обновлять данные
                                      ExtractDataPriceTargetCategoryFromDBCommand.Execute(null!);
                                  }
                                  else { return; } // Если не подтверждаем редактирования
                              }
                          }
                          else
                          {
                              MessageBox.Show("Необходимо выбрать целевую категорию прайса из списка");
                              return;
                          }
                      }
                  }));
            }
        }

        // Удаление целевой категории прайса из базы данных
        private RelayCommand? _deletePriceTargetCategoryToDBCommand;
        // Удаление целевой категории прайса из базы данных
        public RelayCommand DeletePriceTargetCategoryToDBCommand
        {
            get
            {
                return _deletePriceTargetCategoryToDBCommand ??
                  (_deletePriceTargetCategoryToDBCommand = new RelayCommand(obj =>
                  {
                      using (ApplicationContext db = new ApplicationContext())
                      {
                          if (SelectedPriceTargetCategory != null)
                          {
                              PriceTargetCategory selectedPriceTargetCategory = SelectedPriceTargetCategory;
                              PriceTargetCategory? foundDeletedPriceTargetCategory = db.PriceTargetCategories.Find(selectedPriceTargetCategory.PriceTargetCategoryID);
                              if (foundDeletedPriceTargetCategory != null)
                              {
                                  bool? confirmed = _dialogService.Confirm("Подтверждение удаления", $"Вы действительно хотите удалить целевую категорию прайса c ID «{foundDeletedPriceTargetCategory.PriceTargetCategoryID}» -  «{foundDeletedPriceTargetCategory.PriceTargetCategoryName}»?");

                                  if (confirmed == true) // Если подтверждаем удаление
                                  {
                                      db.PriceTargetCategories.Remove(foundDeletedPriceTargetCategory!);
                                      db.SaveChanges();

                                      // TODO: Подумать как еще можно обновлять данные
                                      ExtractDataPriceTargetCategoryFromDBCommand.Execute(null!);
                                  }
                                  else { return; } // Если не подтверждаем удаление

                              }

                          }
                          else
                          {
                              MessageBox.Show("Необходимо выбрать целевую категорию прайса из списка");
                              return;
                          }

                      }

                  }));
            }
        }

        // Снять выделение целевой категории прайса
        private RelayCommand? _deselectPriceTargetCategoryCommand;
        // Снять выделение целевой категории прайса
        public RelayCommand DeselectPriceTargetCategoryCommand
        {
            get
            {
                return _deselectPriceTargetCategoryCommand ??
                  (_deselectPriceTargetCategoryCommand = new RelayCommand(obj =>
                  {

                      SelectedPriceTargetCategory = null!;
                      ClearPropertyPriceTargetCategory();

                  }));
            }
        }

        // Обновление данных о целевых категориях прайса из базы данных
        private RelayCommand? _updateDataPriceTargetCategoryFromDBCommand;
        // Обновление данных о целевых категориях прайса из базы данных
        public RelayCommand UpdateDataPriceTargetCategoryFromDBCommand
        {
            get
            {
                return _updateDataPriceTargetCategoryFromDBCommand ??
                  (_updateDataPriceTargetCategoryFromDBCommand = new RelayCommand(obj =>
                  {
                      ExtractDataPriceTargetCategoryFromDBCommand.Execute(null!);
                  }));
            }
        }

        /// <summary>Очистить свойства санатория</summary>
        private void ClearPropertyPriceTargetCategory()
        {
            PriceTargetCategoryID = default;
            PriceTargetCategoryName = String.Empty;
            PriceTargetCategoryDescription = String.Empty;
        }

        /// <summary>Заполнить свойства целевого прайса</summary>
        private void FillPropertyPriceTargetCategory()
        {
            if (SelectedPriceTargetCategory != null)
            {
                PriceTargetCategoryID = SelectedPriceTargetCategory.PriceTargetCategoryID;
                PriceTargetCategoryName = SelectedPriceTargetCategory.PriceTargetCategoryName;
                PriceTargetCategoryDescription = SelectedPriceTargetCategory.PriceTargetCategoryDescription;
            }
            else
            {
                return;
            }

        }

        private PriceTargetCategory CreateNewPriceTargetCategory()
        {
            PriceTargetCategory createdNewPriceTargetCategory = new PriceTargetCategory { PriceTargetCategoryName = PriceTargetCategoryName, PriceTargetCategoryDescription = PriceTargetCategoryDescription };
            return createdNewPriceTargetCategory;
        }
    }
}
