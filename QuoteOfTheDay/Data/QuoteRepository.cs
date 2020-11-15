using System.Collections.Generic;
using QuoteOfTheDay.Entities;
using Microsoft.EntityFrameworkCore;

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
            return _dbContext.Find<Quote>(id);
        }
    }
}
