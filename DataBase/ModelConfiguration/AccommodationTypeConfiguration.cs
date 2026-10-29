using BookingCalc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BookingCalc.DataBase.ModelConfiguration
{
    public class AccommodationTypeConfiguration : IEntityTypeConfiguration<AccommodationType>
    {
        public void Configure(EntityTypeBuilder<AccommodationType> builder)
        {
            // Инициализация базы данных начальными данными
            builder.HasData(
                new AccommodationType() { AccommodationTypeID = 1, SanatoriumID = 1, AccommodationTypeName = "Одноместное" },
                new AccommodationType() { AccommodationTypeID = 2, SanatoriumID = 1, AccommodationTypeName = "Двухместное" }
           );
        }
    }
}
