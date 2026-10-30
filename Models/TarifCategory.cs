namespace BookingCalc.Models
{
    /// <summary>Класс содержит информацию о категории тарифа санатория(с лечением, без лечения, оздоровление и т.п.)</summary>
    public class TarifCategory
    {
        /// <summary>Конструктор категории тарифа санатория без параметров</summary>
		public TarifCategory() { }

        /// <summary>Идентификатор категории тарифа санатория</summary>
        private Int32 _tarifCategoryID;
        /// <summary>Идентификатор категории тарифа санатория</summary>
        public Int32 TarifCategoryID
        {
            get { return _tarifCategoryID; }
            set { _tarifCategoryID = value; }
        }

        /// <summary>Идентификатор санатория</summary>
        private Int32 _sanatoriumID;
        /// <summary>Идентификатор санатория</summary>
        public Int32 SanatoriumID
        {
            get { return _sanatoriumID; }
            set { _sanatoriumID = value; }
        }

        /// <summary>Текущий санаторий</summary>
        private Sanatorium _currentSanatorium = null!;
        /// <summary>Текущий санаторий</summary>
        public Sanatorium CurrentSanatorium
        {
            get { return _currentSanatorium; }
            set { _currentSanatorium = value; }
        }

        /// <summary>Наименование категории тарифа санатория</summary>
        private String _tarifCategoryName = String.Empty;
        /// <summary>Наименование категории тарифа санатория</summary>
        public String TarifCategoryName
        {
            get { return _tarifCategoryName; }
            set { _tarifCategoryName = value; }
        }
    }
}
