using BookingCalc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingCalc.DataBase.ModelConfiguration
{
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
