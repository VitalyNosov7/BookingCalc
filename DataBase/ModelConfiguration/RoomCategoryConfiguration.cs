using BookingCalc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingCalc.DataBase.ModelConfiguration
{
    /// <summary>Конфигурация таблицы Категории номера санатория</summary>
    public class RoomCategoryConfiguration : IEntityTypeConfiguration<RoomCategory>
    {
        public void Configure(EntityTypeBuilder<RoomCategory> builder)
        {
            // Инициализация базы данных начальными данными
            builder.HasData(

                new RoomCategory() { RoomCategoryID = 1, SanatoriumID = 1, RoomCategoryName = "Стандарт" }  // Категория номера санатория Планета	
           );
        }
    }
}
