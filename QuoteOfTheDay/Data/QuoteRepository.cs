using System.Collections.Generic;
using QuoteOfTheDay.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace QuoteOfTheDay.Data
{
    public class QuoteRepository
    {
        private readonly QuoteOfTheDayDbContext _dbContext;

        public QuoteRepository(QuoteOfTheDayDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Quote> GetAll()
        {
            return _dbContext.Quotes.Include(q => q.Category);
        }

        public Quote GetById(int id)
        {
            return _dbContext.Quotes  // needs refilming
                .Include( q => q.Category)
                .SingleOrDefault(c => c.Id == id);
        }

        public Quote AddQuote(Quote quote)
        {
            _dbContext.Add<Quote>(quote);
            _dbContext.SaveChanges();

            _dbContext
                .Entry(quote)
                .Reference(q => q.Category)
                .Load();
            
            return quote;
        }

        public Quote UpdateQuote(Quote quote)
        {
            _dbContext.Attach(quote);
            _dbContext.Entry(quote).State = EntityState.Modified;
            _dbContext.SaveChanges();

            _dbContext
               .Entry(quote)
               .Reference(q => q.Category)
               .Load();

            return quote;
        }

        public void DeleteQuote(int id)
        {
            var quote = GetById(id);
            _dbContext.Remove(quote);
            _dbContext.SaveChanges(); 
        }

        public IEnumerable<Category> GetCategories()
        {
            return _dbContext.Categories;
        }
    }
}
