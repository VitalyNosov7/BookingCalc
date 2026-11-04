namespace BookingCalc.Models
{
    public static class BookingCalculator
    {
        public static List<BookingSegment> CalculateSegments(
        List<PriceBooking> pricePeriods,
        BookingRequest request)
        {
            if (request.StartDateBookingPeriod > request.EndDateBookingPeriod)
                throw new ArgumentException("Дата начала не может быть позже даты окончания.");

            // Нормализация: берём только дату (без времени)
            var start = request.StartDateBookingPeriod.Date;
            var end = request.EndDateBookingPeriod.Date;

            var sorted = pricePeriods
                .OrderBy(p => p.StartDatePeriodPrice)
                .ToList();

            var segments = new List<BookingSegment>();

            foreach (var period in sorted)
            {
                var pStart = period.StartDatePeriodPrice.Date;
                var pEnd = period.EndDatePeriodPrice.Date;

                // Пересечение
                var intersectStart = start > pStart ? start : pStart;
                var intersectEnd = end < pEnd ? end : pEnd;

                if (intersectStart > intersectEnd)
                    continue; // нет пересечения

                //int daysCount = (intersectEnd - intersectStart).Days + 1; // включительно

                int daysCount = (intersectEnd - intersectStart).Days; // включительно

                segments.Add(new BookingSegment
                {
                    StartBookingPeriodSegment = intersectStart,
                    EndBookingPeriodSegment = intersectEnd,
                    DaysCount = daysCount,
                    PricePerDay = period.PricePeriodDayFromGuest
                });
            }

            //// Опционально: проверить, что весь период покрыт
            //if (!IsFullCoverage(segments, start, end))
            //{
            //    // Можно выбросить исключение, вернуть предупреждение или обработать как нужно
            //     throw new InvalidOperationException("Не все дни в запросе на бронирование охвачены ценовыми периодами.");
            //}

            return segments;
        }

        // Проверка, что между start и end нет «дыр» в сегментах
        //private static bool IsFullCoverage(List<BookingSegment> segments, DateTime start, DateTime end)
        //{
        //    if (!segments.Any())
        //        return false;

        //    var sorted = segments.OrderBy(s => s.StartBookingPeriodSegment).ToList();
        //    if (sorted.First().StartBookingPeriodSegment != start)
        //        return false;
        //    if (sorted.Last().EndBookingPeriodSegment != end)
        //        return false;

        //    for (int i = 0; i < sorted.Count - 1; i++)
        //    {
        //        // Следующий должен начинаться ровно на день после конца текущего
        //        if ((sorted[i + 1].StartBookingPeriodSegment - sorted[i].EndBookingPeriodSegment).Days != 1)
        //            return false;
        //    }

        //    return true;
        //}
    }
}
