using BookingCalc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingCalc.DataBase.ModelConfiguration
{
    public class TarifCategoryConfiguration : IEntityTypeConfiguration<TarifCategory>
    {

        public void Configure(EntityTypeBuilder<TarifCategory> builder)
        {
            // Инициализация базы данных начальными данными
            builder.HasData(

                new TarifCategory() { TarifCategoryID = 1, SanatoriumID = 1, TarifCategoryName = "С лечением" }, // Категория тарифа санатория Планета		
                new TarifCategory() { TarifCategoryID = 2, SanatoriumID = 1, TarifCategoryName = "Без лечения" } // Категория тарифа санатория Планета		
           );
        }
    }
}
