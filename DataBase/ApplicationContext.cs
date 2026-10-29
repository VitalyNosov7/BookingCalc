using BookingCalc.DataBase.ModelConfiguration;
using BookingCalc.Models;
using Microsoft.EntityFrameworkCore;


namespace BookingCalc.DataBase
{
    public class ApplicationContext : DbContext
    {

        public ApplicationContext()
        {
            // Для тестирования создания базы данных. Перед миграцией закомментировать!
          // Database.EnsureDeleted();
            Database.EnsureCreated();

        }

        public DbSet<PriceBooking> PriceBookings { get; set; } = null!;
        public DbSet<Sanatorium> Sanatoriums { get; set; } = null!;
        public DbSet<TarifCategory> TarifCategories { get; set; } = null!;
        public DbSet<AccommodationType> AccommodationTypes { get; set; } = null!;
        public DbSet<PriceTargetCategory> PriceTargetCategories { get; set; } = null!;
        public DbSet<RoomCategory> RoomCategories { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=databaseBC.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PriceBookingConfiguration());
            modelBuilder.ApplyConfiguration(new AccommodationTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PriceTargetCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new RoomCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new SanatoriumConfiguration());
            modelBuilder.ApplyConfiguration(new TarifCategoryConfiguration());
        }
    
    }
}
