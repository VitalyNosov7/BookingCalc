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
				// Прайс Планета
                // Актуальный прайс на дату 29.06.2026
                new PriceBooking() { PriceBookingID = 1, SanatoriumID = 1, PriceEffectiveDate = new DateTime(2026, 06, 29), StartDatePeriodPrice = new DateTime(2026, 11, 01), EndDatePeriodPrice = new DateTime(2026, 12, 31), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 1, AccommodationTypeID = 1, PricePeriodDayFromGuest = 4830 },
                new PriceBooking() { PriceBookingID = 2, SanatoriumID = 1, PriceEffectiveDate = new DateTime(2026, 06, 29), StartDatePeriodPrice = new DateTime(2026, 11, 01), EndDatePeriodPrice = new DateTime(2026, 12, 31), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 1, AccommodationTypeID = 2, PricePeriodDayFromGuest = 6440 },

				new PriceBooking() { PriceBookingID = 3, SanatoriumID = 1, PriceEffectiveDate = new DateTime(2026, 06, 29), StartDatePeriodPrice = new DateTime(2027, 01, 01), EndDatePeriodPrice = new DateTime(2027, 02, 28), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 1, AccommodationTypeID = 1, PricePeriodDayFromGuest = 5600 },
				new PriceBooking() { PriceBookingID = 4, SanatoriumID = 1, PriceEffectiveDate = new DateTime(2026, 06, 29), StartDatePeriodPrice = new DateTime(2027, 01, 01), EndDatePeriodPrice = new DateTime(2027, 02, 28), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 1, AccommodationTypeID = 2, PricePeriodDayFromGuest = 7500 },

				new PriceBooking() { PriceBookingID = 5, SanatoriumID = 1, PriceEffectiveDate = new DateTime(2026, 06, 29), StartDatePeriodPrice = new DateTime(2027, 03, 01), EndDatePeriodPrice = new DateTime(2027, 03, 31), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 1, AccommodationTypeID = 1, PricePeriodDayFromGuest = 6250 },
				new PriceBooking() { PriceBookingID = 6, SanatoriumID = 1, PriceEffectiveDate = new DateTime(2026, 06, 29), StartDatePeriodPrice = new DateTime(2027, 03, 01), EndDatePeriodPrice = new DateTime(2027, 03, 31), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 1, AccommodationTypeID = 2, PricePeriodDayFromGuest = 8300 },

				 // =============== Актуальный прайс на дату 01.10.26 Планета

				 new PriceBooking() { PriceBookingID = 7, SanatoriumID = 1, PriceEffectiveDate = new DateTime(2026, 10, 01), StartDatePeriodPrice = new DateTime(2026, 11, 01), EndDatePeriodPrice = new DateTime(2026, 12, 31), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 1, AccommodationTypeID = 1, PricePeriodDayFromGuest = 3000 },
				new PriceBooking() { PriceBookingID = 8, SanatoriumID = 1, PriceEffectiveDate = new DateTime(2026, 10, 01), StartDatePeriodPrice = new DateTime(2026, 11, 01), EndDatePeriodPrice = new DateTime(2026, 12, 31), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 1, AccommodationTypeID = 2, PricePeriodDayFromGuest = 6000 },

				new PriceBooking() { PriceBookingID = 9, SanatoriumID = 1, PriceEffectiveDate = new DateTime(2026, 10, 01), StartDatePeriodPrice = new DateTime(2027, 01, 01), EndDatePeriodPrice = new DateTime(2027, 02, 28), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 1, AccommodationTypeID = 1, PricePeriodDayFromGuest = 5000 },
				new PriceBooking() { PriceBookingID = 10, SanatoriumID = 1, PriceEffectiveDate = new DateTime(2026, 10, 019), StartDatePeriodPrice = new DateTime(2027, 01, 01), EndDatePeriodPrice = new DateTime(2027, 02, 28), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 1, AccommodationTypeID = 2, PricePeriodDayFromGuest = 7000 },

				new PriceBooking() { PriceBookingID = 11, SanatoriumID = 1, PriceEffectiveDate = new DateTime(2026, 10, 01), StartDatePeriodPrice = new DateTime(2027, 03, 01), EndDatePeriodPrice = new DateTime(2027, 03, 31), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 1, AccommodationTypeID = 1, PricePeriodDayFromGuest = 6000 },
				new PriceBooking() { PriceBookingID = 12, SanatoriumID = 1, PriceEffectiveDate = new DateTime(2026, 10, 01), StartDatePeriodPrice = new DateTime(2027, 03, 01), EndDatePeriodPrice = new DateTime(2027, 03, 31), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 1, AccommodationTypeID = 2, PricePeriodDayFromGuest = 8000 },
                // КОНЕЦ Прайс Планета

                // Прайс Киев 
                // Актуальный прайс на дату 10.09.2026
                new PriceBooking() { PriceBookingID = 13, SanatoriumID = 2, PriceEffectiveDate = new DateTime(2026, 09, 10), StartDatePeriodPrice = new DateTime(2026, 11, 01), EndDatePeriodPrice = new DateTime(2026, 12, 31), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 5, AccommodationTypeID = 1, PricePeriodDayFromGuest = 5760 },
                new PriceBooking() { PriceBookingID = 14, SanatoriumID = 2, PriceEffectiveDate = new DateTime(2026, 09, 10), StartDatePeriodPrice = new DateTime(2026, 11, 01), EndDatePeriodPrice = new DateTime(2026, 12, 31), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 5, AccommodationTypeID = 2, PricePeriodDayFromGuest = 7920 },

                // Актуальный прайс на дату 10.09.2026
                new PriceBooking() { PriceBookingID = 15, SanatoriumID = 2, PriceEffectiveDate = new DateTime(2026, 09, 10), StartDatePeriodPrice = new DateTime(2027, 02, 01), EndDatePeriodPrice = new DateTime(2027, 03, 31), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 5, AccommodationTypeID = 1, PricePeriodDayFromGuest = 7200 },
                new PriceBooking() { PriceBookingID = 16, SanatoriumID = 2, PriceEffectiveDate = new DateTime(2026, 09, 10), StartDatePeriodPrice = new DateTime(2027, 02, 01), EndDatePeriodPrice = new DateTime(2027, 03, 31), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 5, AccommodationTypeID = 2, PricePeriodDayFromGuest = 9900 },

                new PriceBooking() { PriceBookingID = 17, SanatoriumID = 2, PriceEffectiveDate = new DateTime(2026, 09, 10), StartDatePeriodPrice = new DateTime(2027, 04, 01), EndDatePeriodPrice = new DateTime(2027, 04, 30), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 5, AccommodationTypeID = 1, PricePeriodDayFromGuest = 7500 },
                new PriceBooking() { PriceBookingID = 18, SanatoriumID = 2, PriceEffectiveDate = new DateTime(2026, 09, 10), StartDatePeriodPrice = new DateTime(2027, 04, 01), EndDatePeriodPrice = new DateTime(2027, 04, 30), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 5, AccommodationTypeID = 2, PricePeriodDayFromGuest = 10460 },

                // =============== Актуальный прайс на дату 10.10.26 Планета


                new PriceBooking() { PriceBookingID = 19, SanatoriumID = 2, PriceEffectiveDate = new DateTime(2026, 10, 10), StartDatePeriodPrice = new DateTime(2026, 11, 01), EndDatePeriodPrice = new DateTime(2026, 12, 31), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 5, AccommodationTypeID = 1, PricePeriodDayFromGuest = 6000 },
                new PriceBooking() { PriceBookingID = 20, SanatoriumID = 2, PriceEffectiveDate = new DateTime(2026, 10, 10), StartDatePeriodPrice = new DateTime(2026, 11, 01), EndDatePeriodPrice = new DateTime(2026, 12, 31), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 5, AccommodationTypeID = 2, PricePeriodDayFromGuest = 8000 },

                // Актуальный прайс на дату 10.09.2026
                new PriceBooking() { PriceBookingID = 21, SanatoriumID = 2, PriceEffectiveDate = new DateTime(2026, 10, 10), StartDatePeriodPrice = new DateTime(2027, 02, 01), EndDatePeriodPrice = new DateTime(2027, 03, 31), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 5, AccommodationTypeID = 1, PricePeriodDayFromGuest = 7800 },
                new PriceBooking() { PriceBookingID = 22, SanatoriumID = 2, PriceEffectiveDate = new DateTime(2026, 10, 10), StartDatePeriodPrice = new DateTime(2027, 02, 01), EndDatePeriodPrice = new DateTime(2027, 03, 31), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 5, AccommodationTypeID = 2, PricePeriodDayFromGuest = 10000 },

                new PriceBooking() { PriceBookingID = 23, SanatoriumID = 2, PriceEffectiveDate = new DateTime(2026, 10, 10), StartDatePeriodPrice = new DateTime(2027, 04, 01), EndDatePeriodPrice = new DateTime(2027, 04, 30), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 5, AccommodationTypeID = 1, PricePeriodDayFromGuest = 8500 },
                new PriceBooking() { PriceBookingID = 24, SanatoriumID = 2, PriceEffectiveDate = new DateTime(2026, 10, 10), StartDatePeriodPrice = new DateTime(2027, 04, 01), EndDatePeriodPrice = new DateTime(2027, 04, 30), PriceTargetCategoryID = 1, TarifCategoryID = 1, RoomCategoryID = 5, AccommodationTypeID = 2, PricePeriodDayFromGuest = 11000 }
            );
        }
    }
}
