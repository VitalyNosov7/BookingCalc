
namespace BookingCalc.Models
{
    /// <summary>Класс содержит информацию о стоимости бронирования</summary>
    public class PriceBooking
    {


        /// <summary>Инициализация данных о стоимости бронирования без параметров</summary>
        public PriceBooking() { }

        /// <summary>Идентификатор стоимости бронирования</summary>
        private Int32 _priceBookingID = default;
        /// <summary>Идентификатор стоимости бронирования</summary>
        public Int32 PriceBookingID
        {
            get { return _priceBookingID; }
            set { _priceBookingID = value; }
        }

        /// <summary>Дата вступления цены в силу</summary>
        private DateTime _priceEffectiveDate = default;
        /// <summary>Дата вступления цены в силу</summary>
        public DateTime PriceEffectiveDate
        {
            get { return _priceEffectiveDate; }
            set { _priceEffectiveDate = value; }
        }

        /// <summary>Дата начала периода стоимости бронирования</summary>
        private DateTime _startDatePeriodPrice = default;
        /// <summary>Дата начала периода стоимости бронирования</summary>
        public DateTime StartDatePeriodPrice
        {
            get { return _startDatePeriodPrice; }
            set { _startDatePeriodPrice = value; }
        }

        /// <summary>Дата окончания периода стоимости бронирования</summary>
        private DateTime _endDatePeriodPrice = default;
        /// <summary>Дата окончания периода стоимости бронирования</summary>
        public DateTime EndDatePeriodPrice
        {
            get { return _endDatePeriodPrice; }
            set { _endDatePeriodPrice = value; }
        }

        /// <summary>Стоимость 1 дня(ночи) бронирования</summary>
        private Decimal _pricePeriodDay = default;
        /// <summary>Стоимость 1 дня(ночи) бронирования</summary>
        public Decimal PricePeriodDay
        {
            get { return _pricePeriodDay; }
            set { _pricePeriodDay = value; }
        }

        /// <summary>Идентификатор санатория</summary>
        private Int32 _sanatoriumID = default;
        /// <summary>Идентификатор санатория</summary>
        public Int32 SanatoriumID
        {
            get { return _sanatoriumID; }
            set { _sanatoriumID = value; }
        }

        /// <summary>Санаторий</summary>
        private Sanatorium _currentSanatorium = null!;
        /// <summary>Санаторий</summary>
        public Sanatorium CurrentSanatorium
        {
            get { return _currentSanatorium; }
            set { _currentSanatorium = value; }
        }

        /// <summary>Идентификатор категории номера</summary>
        private Int32 _roomCategoryID = default;
        /// <summary>Идентификатор категории номера</summary>
        public Int32 RoomCategoryID
        {
            get { return _roomCategoryID; }
            set { _roomCategoryID = value; }
        }

        /// <summary>Категория номера</summary>
        private RoomCategory _currentRoomCategory = null!;
        /// <summary>Категория номера</summary>
        public RoomCategory CurrentRoomCategory
        {
            get { return _currentRoomCategory; }
            set { _currentRoomCategory = value; }
        }

        /// <summary>Идентификатор вида размещения</summary>
        private Int32 _accommodationTypeID = default;
        /// <summary>Идентификатор вида размещения</summary>
        public Int32 AccommodationTypeID
        {
            get { return _accommodationTypeID; }
            set { _accommodationTypeID = value; }
        }

        /// <summary>Вид размещения/summary>
        private AccommodationType _currentAccommodationType = null!;
        /// <summary>Вид размещения/summary>
        public AccommodationType CurrentAccommodationType
        {
            get { return _currentAccommodationType; }
            set { _currentAccommodationType = value; }
        }

        /// <summary>Идентификатор категории тарифа/summary>
        private Int32 _tarifCategoryID = default;
        /// <summary>Идентификатор категории тарифа/summary>
        public Int32 TarifCategoryID
        {
            get { return _tarifCategoryID; }
            set { _tarifCategoryID = value; }
        }

        /// <summary>Категория тарифа/summary>
        private TarifCategory _currentTarifCategory = null!;
        /// <summary>Категория тарифа/summary>
        public TarifCategory CurrentTarifCategory
        {
            get { return _currentTarifCategory; }
            set { _currentTarifCategory = value; }
        }

        /// <summary>Идентификатор целевой категории стоимости бронирования</summary>
        private Int32 _priceTargetCategoryID = default;
        /// <summary>Идентификатор целевой категории стоимости бронирования</summary>
        public Int32 PriceTargetCategoryID
        {
            get { return _priceTargetCategoryID; }
            set { _priceTargetCategoryID = value; }
        }

        /// <summary>Целевая категория стоимости бронирования</summary>
        private PriceTargetCategory _currentPriceTargetCategory = null!;
        /// <summary>Целевая категория стоимости бронирования</summary>
        public PriceTargetCategory CurrentPriceTargetCategory
        {
            get { return _currentPriceTargetCategory; }
            set { _currentPriceTargetCategory = value; }
        }
    }
}
