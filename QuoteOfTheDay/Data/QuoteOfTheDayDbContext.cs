using Microsoft.EntityFrameworkCore;
using QuoteOfTheDay.Entities;

namespace QuoteOfTheDay.Data
{
    public class QuoteOfTheDayDbContext : DbContext
    {
        public QuoteOfTheDayDbContext(DbContextOptions<QuoteOfTheDayDbContext> options) : base(options)
        {
        }

        public DbSet<Quote> Quotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }
    }
}
