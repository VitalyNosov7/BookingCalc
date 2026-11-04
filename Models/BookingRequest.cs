namespace BookingCalc.Models
{
    // Запрос на бронирование(желаемый период бронирования)
    public class BookingRequest
    {
        // Идентификатор запроса на бронирование
        private Int32 _bookingRequestID;
        // Идентификатор запроса на бронирование
        public Int32 BookingRequestID
        {
            get { return _bookingRequestID; }
            set { _bookingRequestID = value; }
        }

        // Дата начала периода бронирования
        public DateTime StartDateBookingPeriod { get; set; }
        // Дата окончания периода бронирования
        public DateTime EndDateBookingPeriod { get; set; }
    }
}
