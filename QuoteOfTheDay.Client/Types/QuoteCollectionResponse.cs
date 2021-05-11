using System.Collections.Generic;

namespace QuoteOfTheDay.Client.Types
{
    public class QuoteCollectionResponse
    {
        public ICollection<Quote> Quotes { get; set; }
    }
}
