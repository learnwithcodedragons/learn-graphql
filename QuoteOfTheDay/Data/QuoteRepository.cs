using System.Collections.Generic;
using QuoteOfTheDay.Entities;

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
            return _dbContext.Quotes;
        }
    }
}
