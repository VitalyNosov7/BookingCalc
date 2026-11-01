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
                new PriceBooking() { PriceBookingID = 2, SanatoriumID = 1, PriceEffectiveDate = new DateTime(2026, 06, 29), StartDatePeriodPrice = new DateTime(2026, 11, 01), EndDatePeriodPrice = new DateTime(2026, 12, 31), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 1, AccommodationTypeID = 2, PricePeriodDay = 6440 },

				new PriceBooking() { PriceBookingID = 3, SanatoriumID = 1, PriceEffectiveDate = new DateTime(2026, 06, 29), StartDatePeriodPrice = new DateTime(2027, 01, 01), EndDatePeriodPrice = new DateTime(2027, 02, 28), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 1, AccommodationTypeID = 1, PricePeriodDay = 5600 },
				new PriceBooking() { PriceBookingID = 4, SanatoriumID = 1, PriceEffectiveDate = new DateTime(2026, 06, 29), StartDatePeriodPrice = new DateTime(2027, 01, 01), EndDatePeriodPrice = new DateTime(2027, 02, 28), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 1, AccommodationTypeID = 2, PricePeriodDay = 7500 },

				new PriceBooking() { PriceBookingID = 5, SanatoriumID = 1, PriceEffectiveDate = new DateTime(2026, 06, 29), StartDatePeriodPrice = new DateTime(2027, 03, 01), EndDatePeriodPrice = new DateTime(2027, 03, 31), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 1, AccommodationTypeID = 1, PricePeriodDay = 6250 },
				new PriceBooking() { PriceBookingID = 6, SanatoriumID = 1, PriceEffectiveDate = new DateTime(2026, 06, 29), StartDatePeriodPrice = new DateTime(2027, 03, 01), EndDatePeriodPrice = new DateTime(2027, 03, 31), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 1, AccommodationTypeID = 2, PricePeriodDay = 8300 },

				 // =============== Актуальный прайс на другую дату

				 new PriceBooking() { PriceBookingID = 7, SanatoriumID = 1, PriceEffectiveDate = new DateTime(2026, 10, 01), StartDatePeriodPrice = new DateTime(2026, 11, 01), EndDatePeriodPrice = new DateTime(2026, 12, 31), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 1, AccommodationTypeID = 1, PricePeriodDay = 3000 },
				new PriceBooking() { PriceBookingID = 8, SanatoriumID = 1, PriceEffectiveDate = new DateTime(2026, 10, 01), StartDatePeriodPrice = new DateTime(2026, 11, 01), EndDatePeriodPrice = new DateTime(2026, 12, 31), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 1, AccommodationTypeID = 2, PricePeriodDay = 6000 },

				new PriceBooking() { PriceBookingID = 9, SanatoriumID = 1, PriceEffectiveDate = new DateTime(2026, 10, 01), StartDatePeriodPrice = new DateTime(2027, 01, 01), EndDatePeriodPrice = new DateTime(2027, 02, 28), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 1, AccommodationTypeID = 1, PricePeriodDay = 5000 },
				new PriceBooking() { PriceBookingID = 10, SanatoriumID = 1, PriceEffectiveDate = new DateTime(2026, 10, 019), StartDatePeriodPrice = new DateTime(2027, 01, 01), EndDatePeriodPrice = new DateTime(2027, 02, 28), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 1, AccommodationTypeID = 2, PricePeriodDay = 7000 },

				new PriceBooking() { PriceBookingID = 11, SanatoriumID = 1, PriceEffectiveDate = new DateTime(2026, 10, 01), StartDatePeriodPrice = new DateTime(2027, 03, 01), EndDatePeriodPrice = new DateTime(2027, 03, 31), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 1, AccommodationTypeID = 1, PricePeriodDay = 6000 },
				new PriceBooking() { PriceBookingID = 12, SanatoriumID = 1, PriceEffectiveDate = new DateTime(2026, 10, 01), StartDatePeriodPrice = new DateTime(2027, 03, 01), EndDatePeriodPrice = new DateTime(2027, 03, 31), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 1, AccommodationTypeID = 2, PricePeriodDay = 8000 }
			);
        }
    }
}
