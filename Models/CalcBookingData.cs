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

		/// <summary>Идентификатор санатория</summary>
		private Int32 _sanatoriumID = default;
		/// <summary>Идентификатор санатория</summary>
		public Int32 SanatoriumID
		{
			get { return _sanatoriumID; }
			set { _sanatoriumID = value; }
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


        /// <summary>Результат расчета бронирования</summary>
        private Decimal _totalBookingSum = default;
        /// <summary>Результат расчета бронирования</summary>
        public Decimal TotalBookingSum
        {
            get { return _totalBookingSum; }
            set { _totalBookingSum = value; }
        }
    }
}
