using BookingCalc.Models;
using Microsoft.EntityFrameworkCore;


namespace BookingCalc.DataBase
{
    public class ApplicationContext : DbContext
    {

        public ApplicationContext()
        {
            // Для тестирования создания базы данных. Перед миграцией закомментировать!
            //`Database.EnsureDeleted();
            Database.EnsureCreated();

        }

        public DbSet<PriceBooking> PriceBookings { get; set; } = null!;
    

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=databasecsb.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           // modelBuilder.ApplyConfiguration(new PersonConfiguration());
         
        }
    
    }
}
