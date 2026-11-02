using BookingCalc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BookingCalc.DataBase.ModelConfiguration
{
    /// <summary>Конфигурация таблицы Вида размещения</summary>
    public class AccommodationTypeConfiguration : IEntityTypeConfiguration<AccommodationType>
    {
        public void Configure(EntityTypeBuilder<AccommodationType> builder)
        {
            // Инициализация базы данных начальными данными
            builder.HasData(
                // Вид размещения Планета
                new AccommodationType() { AccommodationTypeID = 1, SanatoriumID = 1, AccommodationTypeName = "Одноместное" },
                new AccommodationType() { AccommodationTypeID = 2, SanatoriumID = 1, AccommodationTypeName = "Двухместное" },
                new AccommodationType() { AccommodationTypeID = 3, SanatoriumID = 1, AccommodationTypeName = "Основное" },
                new AccommodationType() { AccommodationTypeID = 4, SanatoriumID = 1, AccommodationTypeName = "Доп. 3-12 лет" },
                new AccommodationType() { AccommodationTypeID = 5, SanatoriumID = 1, AccommodationTypeName = "Доп. место" },

                 // Вид размещения Киев
                 new AccommodationType() { AccommodationTypeID = 6, SanatoriumID = 2, AccommodationTypeName = "Одноместное" },
                 new AccommodationType() { AccommodationTypeID = 7, SanatoriumID = 2, AccommodationTypeName = "Двухместное" },
                 new AccommodationType() { AccommodationTypeID = 8, SanatoriumID = 2, AccommodationTypeName = "Основное" },
                 new AccommodationType() { AccommodationTypeID = 9, SanatoriumID = 2, AccommodationTypeName = "Доп. место 5-13 лет (вкл.)" },
                 new AccommodationType() { AccommodationTypeID = 10, SanatoriumID = 2, AccommodationTypeName = "Доп. место с 14 лет и старше" }
           );
        }
    }
}
