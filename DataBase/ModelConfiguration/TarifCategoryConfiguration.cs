using BookingCalc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingCalc.DataBase.ModelConfiguration
{
    /// <summary>Конфигурация таблицы Тарифа санатория(с лечением, без лечения, оздоровление и т.п.)</summary>
    public class TarifCategoryConfiguration : IEntityTypeConfiguration<TarifCategory>
    {

        public void Configure(EntityTypeBuilder<TarifCategory> builder)
        {
            // Инициализация базы данных начальными данными
            builder.HasData(
                // Категория тарифа санатория Планета
                new TarifCategory() { TarifCategoryID = 1, SanatoriumID = 1, TarifCategoryName = "С лечением" }, 	
                new TarifCategory() { TarifCategoryID = 2, SanatoriumID = 1, TarifCategoryName = "Без лечения" },
                new TarifCategory() { TarifCategoryID = 3, SanatoriumID = 2, TarifCategoryName = "С лечением" },
                new TarifCategory() { TarifCategoryID = 4, SanatoriumID = 2, TarifCategoryName = "Оздоровление" }
           );
        }
    }
}
