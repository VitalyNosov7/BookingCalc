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
                // Категории номеров Планета
                new RoomCategory() { RoomCategoryID = 1, SanatoriumID = 1, RoomCategoryName = "Стандарт" },  // Категория номера санатория Планета	
                new RoomCategory() { RoomCategoryID = 2, SanatoriumID = 1, RoomCategoryName = "Полулюкс" },  // Категория номера санатория Планета	
                new RoomCategory() { RoomCategoryID = 3, SanatoriumID = 1, RoomCategoryName = "Люкс" },      // Категория номера санатория Планета	

                // Категории номеров Киев
                new RoomCategory() { RoomCategoryID = 4, SanatoriumID = 2, RoomCategoryName = "1-местный номер, 2 корпус" },     // Категория номера санатория Киев
                new RoomCategory() { RoomCategoryID = 5, SanatoriumID = 2, RoomCategoryName = "2-местный номер 1,2 корпус" },    // Категория номера санатория Киев
                new RoomCategory() { RoomCategoryID = 6, SanatoriumID = 2, RoomCategoryName = "2-3-местный номер,5 корпус" },    // Категория номера санатория Киев
                new RoomCategory() { RoomCategoryID = 7, SanatoriumID = 2, RoomCategoryName = "2-х комнатный 2-4 местный, 1,2 корпус" },    // Категория номера санатория Киев
                new RoomCategory() { RoomCategoryID = 8, SanatoriumID = 2, RoomCategoryName = "КОМФОРТ СЕМЕЙНЫЙ" },    // Категория номера санатория Киев
                new RoomCategory() { RoomCategoryID = 9, SanatoriumID = 2, RoomCategoryName = "2-4-х местный люкс" }     // Категория номера санатория Киев

           );
        }
    }
}
