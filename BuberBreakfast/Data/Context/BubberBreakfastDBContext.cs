using BuberBreakfast.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace BuberBreakfast.Data.Context
{
    public class BuberBreakfastContext : DbContext
    {
        public BuberBreakfastContext(DbContextOptions<BuberBreakfastContext> options) : base(options)
        {
        }

        public DbSet<Breakfast> Breakfasts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Breakfast>().ToTable("Breakfast");
        }
    }
}