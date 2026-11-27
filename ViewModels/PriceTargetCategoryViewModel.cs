using BookingCalc.Commands;
using BookingCalc.DataBase;
using BookingCalc.Models;
using BookingCalc.ViewModels.Base;
using BookingCalc.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
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
                          ClearPropertySanatorium();
                      }

                  }));
            }
        }

        /// <summary>Заполнить свойства целевого прайса</summary>
        private void FillPropertyPriceTargetCategory()
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
    }
}
