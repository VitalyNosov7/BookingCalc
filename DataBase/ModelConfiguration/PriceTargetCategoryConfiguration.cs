using BookingCalc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BookingCalc.DataBase.ModelConfiguration
{
    public class PriceTargetCategoryConfiguration : IEntityTypeConfiguration<PriceTargetCategory>
    {
        public void Configure(EntityTypeBuilder<PriceTargetCategory> builder)
        {
            builder.HasData(
     
                new PriceTargetCategory() { PriceTargetCategoryID = 1, PriceTargetCategoryName = "Прайс для гостей" },
                new PriceTargetCategory() { PriceTargetCategoryID = 2, PriceTargetCategoryName = "Прайс для cсанатория" }
            );
        }
    }
}