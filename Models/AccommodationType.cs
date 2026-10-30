namespace BookingCalc.Models
{
    /// <summary>Класс содержит информацию о виде размещения(одноместное, двухместное, доп. и т.п.)</summary>
    public class AccommodationType
    {

        /// <summary>Инициализация вида размещения без параметров</summary>
        public AccommodationType() {}

        /// <summary>Идентификатор вида размещения</summary>
        private Int32 _accommodationTypeID;
        /// <summary>Идентификатор вида размещения</summary>
        public Int32 AccommodationTypeID
        {
            get { return _accommodationTypeID; }
            set { _accommodationTypeID = value; }
        }

        /// <summary>Идентификатор санатория</summary>
        private Int32 _sanatoriumID = default;
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

        /// <summary>Наименование вида размещения</summary>
        private String _accommodationTypeName = String.Empty;
        /// <summary>Наименование вида размещения</summary>
        public String AccommodationTypeName
        {
            get { return _accommodationTypeName; }
            set { _accommodationTypeName = value; }
        }
    }
}
