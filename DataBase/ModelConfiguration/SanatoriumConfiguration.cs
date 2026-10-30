using BookingCalc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BookingCalc.DataBase.ModelConfiguration
{
    /// <summary>Конфигурация таблицы Санатория)</summary>
    public class SanatoriumConfiguration : IEntityTypeConfiguration<Sanatorium>
    {
        public void Configure(EntityTypeBuilder<Sanatorium> builder)
        {
            // Инициализация базы данных начальными данными
            builder.HasData(
            new Sanatorium() { SanatoriumID = 1, SanatoriumName = "Планета" , EmailSanatorium = "olgaakopyan@mail.ru" },
            new Sanatorium() { SanatoriumID = 2, SanatoriumName = "Киев", EmailSanatorium = "alushtasankiev-rus@mail.ru" },
            new Sanatorium() { SanatoriumID = 3, SanatoriumName = "Сакрополь", EmailSanatorium = "sakropol@yandex.ru" },
            new Sanatorium() { SanatoriumID = 4, SanatoriumName = "ТЭС", EmailSanatorium = "teshotel@rambler.ru" },
            new Sanatorium() { SanatoriumID = 5, SanatoriumName = "Узбекистан", EmailSanatorium = "marketing@yalta-uzbekistan.ru" }
           );
        }
    }
}
