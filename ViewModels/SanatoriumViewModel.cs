using BookingCalc.Commands;
using BookingCalc.DataBase;
using BookingCalc.Models;
using BookingCalc.ViewModels.Base;
using BookingCalc.Views.Interfaces;
using System.Collections.ObjectModel;
using System.Windows;

namespace BookingCalc.ViewModels
{
    public class SanatoriumViewModel : ViewModelBase
    {
        public SanatoriumViewModel(IDialogService dialogService)
        {
            ExtractDataSanatoriumFromDBCommand.Execute(null!);
            _dialogService = dialogService;
        }

        private readonly IDialogService _dialogService;

        /// <summary>Список санаториев</summary>
        private ObservableCollection<Sanatorium> _sanatoriums = new ObservableCollection<Sanatorium>();
        /// <summary>Список санаториев</summary>
        public ObservableCollection<Sanatorium> Sanatoriums
        {
            get { return _sanatoriums; }
            set => Set(ref _sanatoriums, value);
        }

        /// <summary>Выбранный санаторий</summary>
        private Sanatorium _selectedSanatorium = null!;
        /// <summary>Выбранный санаторий</summary>
        public Sanatorium SelectedSanatorium
        {
            get { return _selectedSanatorium!; }
            set
            {
                Set(ref _selectedSanatorium, value);
                FillPropertySanatorium();
            }
        }

        /// <summary>Идентификатор санатория</summary>
        private Int32 _sanatoriumID = default;
        /// <summary>Идентификатор санатория</summary>
        public Int32 SanatoriumID
        {
            get { return _sanatoriumID; }
            set => Set(ref _sanatoriumID, value);
        }

        /// <summary>Название санатория</summary>
        private String _sanatoriumName = String.Empty;

        /// <summary>Название санатория</summary>
        public String SanatoriumName
        {
            get { return _sanatoriumName; }
            set => Set(ref _sanatoriumName, value);
        }

        /// <summary> Электронная почта санатория</summary>
        private String _emailSanatorium = String.Empty;
        /// <summary> Электронная почта санатория</summary>
        public String EmailSanatorium
        {
            get { return _emailSanatorium; }
            set => Set(ref _emailSanatorium, value);
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
                          Sanatoriums.Clear();
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

                                  Sanatoriums.Add(createdNewSanatorium);
                              }
                              else
                              {
                                  MessageBox.Show("Данные о санатории не найдены в базе данных!");
                                  return;
                              }

                          }

                          SelectedSanatorium = null!;
                          ClearPropertySanatorium();
                      }

                  }));
            }
        }

        // Добавление данных о санатории из базу данных
        private RelayCommand? addSanatoriumToDBCommand;
        // Добавление данных о санатории из базу данных
        public RelayCommand AddSanatoriumToDBCommand
        {
            get
            {
                return addSanatoriumToDBCommand ??
                  (addSanatoriumToDBCommand = new RelayCommand(obj =>
                  {
                      using (ApplicationContext db = new ApplicationContext())
                      {
                          Sanatorium newSanatorium = CreateNewSanatorium();
                          db.Sanatoriums.Add(newSanatorium);
                          db.SaveChanges();

                          // TODO: Подумать как еще можно обновлять данные
                          ExtractDataSanatoriumFromDBCommand.Execute(null!);
                      }
                  }));
            }
        }


        // Редактирование данных о санатории в базе данных
        private RelayCommand? editSanatoriumToDBCommand;
        // Редактирование данных о санатории в базе данных
        public RelayCommand EditSanatoriumToDBCommand
        {
            get
            {
                return editSanatoriumToDBCommand ??
                  (editSanatoriumToDBCommand = new RelayCommand(obj =>
                  {
                      using (ApplicationContext db = new ApplicationContext())
                      {
                          if (SelectedSanatorium != null)
                          {
                              Sanatorium? selectedSanatorium = SelectedSanatorium;
                              Sanatorium editedSanatorium = CreateNewSanatorium();
                              Sanatorium? foundEditedSanatorium = db.Sanatoriums.Find(selectedSanatorium.SanatoriumID);

                              if (foundEditedSanatorium != null)
                              {
                                  bool? confirmed = _dialogService.Confirm("Подтверждение редактирования", $"Вы действительно хотите редактировать дынне санатория c ID «{foundEditedSanatorium.SanatoriumID}» - «{foundEditedSanatorium.SanatoriumName}»?");

                                  if (confirmed == true) // Если подтверждаем редактирования
                                  {
                                      // TODO: Подумать как можно сдклать валидатор
                                      if (foundEditedSanatorium.SanatoriumName != editedSanatorium.SanatoriumName)
                                      { foundEditedSanatorium.SanatoriumName = editedSanatorium.SanatoriumName; }
                                      if (foundEditedSanatorium.EmailSanatorium != editedSanatorium.EmailSanatorium)
                                      { foundEditedSanatorium.EmailSanatorium = editedSanatorium.EmailSanatorium; }

                                      db.SaveChanges();

                                      // TODO: Подумать как еще можно обновлять данные
                                      ExtractDataSanatoriumFromDBCommand.Execute(null!);
                                  }
                                  else { return; } // Если не подтверждаем редактирования
                              }
                          }
                          else
                          {
                              MessageBox.Show("Необходимо выбрать санаторий из списка");
                              return;
                          }
                      }
                  }));
            }
        }


        // Удаление санатория из базы данных
        private RelayCommand? deleteSanatoriumToDBCommand;
        // Удаление санатория из базы данных
        public RelayCommand DeleteSanatoriumToDBCommand
        {
            get
            {
                return deleteSanatoriumToDBCommand ??
                  (deleteSanatoriumToDBCommand = new RelayCommand(obj =>
                  {
                      using (ApplicationContext db = new ApplicationContext())
                      {
                          if (SelectedSanatorium != null)
                          {
                              Sanatorium selectedSanatorium = SelectedSanatorium;
                              Sanatorium? foundDeletedSanatorium = db.Sanatoriums.Find(selectedSanatorium.SanatoriumID);
                              if (foundDeletedSanatorium != null)
                              {
                                  bool? confirmed = _dialogService.Confirm("Подтверждение удаления", $"Вы действительно хотите удалить санаторий c ID «{foundDeletedSanatorium.SanatoriumID}» -  «{foundDeletedSanatorium.SanatoriumName}»?");

                                  if (confirmed == true) // Если подтверждаем удаление
                                  {
                                      db.Sanatoriums.Remove(foundDeletedSanatorium!);
                                      db.SaveChanges();

                                      // TODO: Подумать как еще можно обновлять данные
                                      ExtractDataSanatoriumFromDBCommand.Execute(null!);
                                  }
                                  else { return; } // Если не подтверждаем удаление

                              }

                          }
                          else
                          {
                              MessageBox.Show("Необходимо выбрать санаторий из списка");
                              return;
                          }

                      }

                  }));
            }
        }

        // Снять выделение санатория
        private RelayCommand? deselectSanatoriumCommand;
        // Снять выделение санатория
        public RelayCommand DeselectSanatoriumCommand
        {
            get
            {
                return deselectSanatoriumCommand ??
                  (deselectSanatoriumCommand = new RelayCommand(obj =>
                  {

                      SelectedSanatorium = null!;
                      ClearPropertySanatorium();

                  }));
            }
        }

        // Обновление данных о санаториях из базы данных
        private RelayCommand? updateDataSanatoriumFromDBCommand;
        // Обновление данных о санаториях из базы данных
        public RelayCommand UpdateDataSanatoriumFromDBCommand
        {
            get
            {
                return updateDataSanatoriumFromDBCommand ??
                  (updateDataSanatoriumFromDBCommand = new RelayCommand(obj =>
                  {
                      ExtractDataSanatoriumFromDBCommand.Execute(null!);
                  }));
            }
        }

        /// <summary>Очистить свойства санатория</summary>
        private void ClearPropertySanatorium()
        {
            SanatoriumID = default;
            SanatoriumName = String.Empty;
            EmailSanatorium = String.Empty;
        }

        /// <summary>Заполнить свойства санатория</summary>
        private void FillPropertySanatorium()
        {
            if (SelectedSanatorium != null)
            {
                SanatoriumID = SelectedSanatorium.SanatoriumID;
                SanatoriumName = SelectedSanatorium.SanatoriumName;
                EmailSanatorium = SelectedSanatorium.EmailSanatorium;
            }
            else
            {
                return;
            }


        }

        private Sanatorium CreateNewSanatorium()
        {
            Sanatorium createdNewSanatorium = new Sanatorium { SanatoriumName = SanatoriumName, EmailSanatorium = EmailSanatorium };
            return createdNewSanatorium;
        }
    
}
}
