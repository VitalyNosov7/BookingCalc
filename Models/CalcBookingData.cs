namespace BookingCalc.Models
{
    /// <summary>Расчет бронирования</summary>
    public class CalcBookingData
    {
        /// <summary>Идентификатор данных о расчете бронирования</summary>
        private Int32 _calcBookingDataID = default;
        /// <summary>Идентификатор данных о расчете бронирования</summary>
        public Int32 CalcBookingDataID
        {
            get { return _calcBookingDataID; }
            set { _calcBookingDataID = value; }
        }

        /// <summary>Дата вступления цены в силу</summary>
        private DateTime _priceEffectiveDate = default;
        /// <summary>Дата вступления цены в силу</summary>
        public DateTime PriceEffectiveDate
        {
            get { return _priceEffectiveDate; }
            set { _priceEffectiveDate = value; }
        }

        /// <summary>Дата начала бронирования</summary>
        private DateTime _startDate = default;
        /// <summary>Дата начала бронирования</summary>
        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        /// <summary>Дата окончания бронирования</summary>
        private DateTime _endDate = default;
        /// <summary>Дата окончания бронирования</summary>
        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        /// <summary>Идентификатор категории номера</summary>
        private Int32 _roomCategoryID = default;
        /// <summary>Идентификатор категории номера</summary>
        public Int32 RoomCategoryID
        {
            get { return _roomCategoryID; }
            set { _roomCategoryID = value; }
        }

        /// <summary>Идентификатор вида размещения</summary>
        private Int32 _accommodationTypeID = default;
        /// <summary>Идентификатор вида размещения</summary>
        public Int32 AccommodationTypeID
        {
            get { return _accommodationTypeID; }
            set { _accommodationTypeID = value; }
        }

        /// <summary>Идентификатор категории тарифа/summary>
        private Int32 _tarifCategoryID = default;
        /// <summary>Идентификатор категории тарифа/summary>
        public Int32 TarifCategoryID
        {
            get { return _tarifCategoryID; }
            set { _tarifCategoryID = value; }
        }

        /// <summary>Стоимость бронирования для гостей</summary>
        private Decimal _priceFromGuest = default;
        /// <summary>Стоимость бронирования для гостей</summary>
        public Decimal PriceFromGuest
        {
            get { return _priceFromGuest; }
            set { _priceFromGuest = value; }
        }

        /// <summary>Результат расчета бронирования для гостей</summary>
        private Decimal _calcResultFromGuest = default;
        /// <summary>Результат расчета бронирования для гостей</summary>
        public Decimal CalcResultFromGuest
        {
            get { return _calcResultFromGuest; }
            set { _calcResultFromGuest = value; }
        }

        /// <summary>Стоимость бронирования для санатория</summary>
        private Decimal _priceFromSanatorium = default;
        /// <summary>Стоимость бронирования для санатория</summary>
        public Decimal PriceFromSanatorium
        {
            get { return _priceFromSanatorium; }
            set { _priceFromSanatorium = value; }
        }

        /// <summary>Результат расчета бронирования для санатория</summary>
        private Decimal _calcResultFromSanatorium = default;
        /// <summary>Результат расчета бронирования для санатория</summary>
        public Decimal CalcResultFromSanatorium
        {
            get { return _calcResultFromSanatorium; }
            set { _calcResultFromSanatorium = value; }
        }
    }
}
