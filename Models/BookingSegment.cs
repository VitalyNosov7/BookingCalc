namespace BookingCalc.Models
{
    // Информация о сегменте периода бронирования
    public class BookingSegment
    {
        // Идентификатор сегмента бронирования
        private Int32 _bookingSegmentID;
        // Идентификатор сегмента бронирования
        public Int32 BookingSegmentID
        {
            get { return _bookingSegmentID; }
            set { _bookingSegmentID = value; }
        }

        // Дата начала сегмента периода бронирования
        public DateTime StartBookingPeriodSegment { get; set; }

        // Дата окончания сегмента периода бронирования
        public DateTime EndBookingPeriodSegment { get; set; }

        // Количество дней(ночей) в текущем сегменте периода бронирования
        public Int32 DaysCount { get; set; }

        // Цена периода за 1 день(ночь)
        public Decimal PricePerDay { get; set; }

        // Стоимость бронирования текущего сегмента
        public Decimal TotalPrice => DaysCount * PricePerDay;
    }
}
