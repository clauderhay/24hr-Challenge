using Microsoft.EntityFrameworkCore;
using _24hr_Code_Challenge.Models;

namespace _24hr_Code_Challenge.Data
{
    public class PizzaPlaceDbContext : DbContext
    {
        public PizzaPlaceDbContext(DbContextOptions<PizzaPlaceDbContext> options) : base(options) { }

        public DbSet<Pizza>? Pizzas { get; set; }
        public DbSet<PizzaType>? PizzaTypes { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<OrderDetail>? OrderDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=localhost;port=3306;database=pizza_place_sales_archive;user=root;password=claude;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => od.OrderDetailId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany()
                .HasForeignKey(od => od.OrderId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Pizza)
                .WithMany()
                .HasForeignKey(od => od.PizzaId);

            modelBuilder.Entity<Pizza>()
                .HasKey(p => p.PizzaId);

            modelBuilder.Entity<Pizza>()
                .HasOne(p => p.PizzaType)
                .WithMany()
                .HasForeignKey(p => p.PizzaTypeId);
        }
    }
}