namespace BookingCalc.Models
{
    public static class BookingCalculator
    {
        #region РАСЧЕТ ДНЕЙ        

        // Расчет дней

        public static List<BookingSegment> CalculateSegmentsDoDaytimeRule(
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

                int daysCount = (intersectEnd - intersectStart).Days + 1; // включительно

                segments.Add(new BookingSegment
                {
                    StartBookingPeriodSegment = intersectStart,
                    EndBookingPeriodSegment = intersectEnd,
                    DaysCount = daysCount,
                    PricePerDay = period.PricePeriodDay
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

        // КОНЕЦ Расчет дней

        #endregion РАСЧЕТ ДНЕЙ

        #region РАСЧЕТ НОЧЕЙ

        // Расчет по "ночному" правилу
        public static List<BookingSegment> CalculateSegmentsDoNighttimeRule(
    List<PriceBooking> pricePeriods,
    BookingRequest request)
        {
            var start = request.StartDateBookingPeriod.Date;     // день заезда — тарифицируется
            var checkout = request.EndDateBookingPeriod.Date;     // день выезда — НЕ тарифицируется

            if (checkout <= start)
                throw new ArgumentException("Дата выезда должна быть позже даты заезда.");

            var segments = new List<BookingSegment>();

            foreach (var period in pricePeriods.OrderBy(p => p.StartDatePeriodPrice))
            {
                var pStart = period.StartDatePeriodPrice.Date;
                var pEnd = period.EndDatePeriodPrice.Date;

                if (pEnd < start) continue; // период целиком раньше брони

                var segStart = pStart < start ? start : pStart;

                // Последний ли это период в рамках брони (покрывает дату выезда и дальше)
                bool isLast = pEnd >= checkout;

                int days;
                DateTime displayEnd;

                if (isLast)
                {
                    // День выезда не тарифицируем: считаем как разность дат
                    days = (checkout - segStart).Days;
                    if (days <= 0) continue;

                    displayEnd = checkout; // в отображении показываем дату выезда
                }
                else
                {
                    // Период целиком внутри брони — считаем включительно
                    days = (pEnd - segStart).Days + 1;
                    displayEnd = pEnd;
                }

                segments.Add(new BookingSegment
                {
                    StartBookingPeriodSegment = segStart, 
                    EndBookingPeriodSegment = displayEnd,                 
                    DaysCount = days,
                    PricePerDay  = period.PricePeriodDay
                    
                });
            }

            // Контроль: сумма сегментов должна совпасть с общим числом дней брони
            int totalDays = (checkout - start).Days;
            int sum = segments.Sum(s => s.DaysCount);
            if (sum != totalDays)
            {
                // есть «дыры» между прайс-периодами или бронь не покрыта —
                // обработайте по своей бизнес-логике
                // throw new InvalidOperationException($"Прайс покрывает {sum} из {totalDays} дней.");
            }

            return segments;
        }

        #endregion РАСЧЕТ НОЧЕЙ
    }
}
