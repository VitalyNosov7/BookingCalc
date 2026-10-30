using BookingCalc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingCalc.DataBase.ModelConfiguration
{
    /// <summary>Конфигурация таблицы Прайса бронирования</summary>
    public class PriceBookingConfiguration : IEntityTypeConfiguration<PriceBooking>
    {
        public void Configure(EntityTypeBuilder<PriceBooking> builder)
        {
            builder.HasData(     
                new PriceBooking() { PriceBookingID = 1, SanatoriumID = 1, PriceEffectiveDate = new DateTime(2026, 06, 29), StartDatePeriodPrice = new DateTime(2026, 11, 01), EndDatePeriodPrice = new DateTime(2026, 12, 31), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 1, AccommodationTypeID = 1, PricePeriodDay = 4830 },
                new PriceBooking() { PriceBookingID = 2, SanatoriumID = 1, PriceEffectiveDate = new DateTime(2026, 06, 29), StartDatePeriodPrice = new DateTime(2026, 11, 01), EndDatePeriodPrice = new DateTime(2026, 12, 31), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 1, AccommodationTypeID = 2, PricePeriodDay = 6440 }
            );
        }
    }
}
