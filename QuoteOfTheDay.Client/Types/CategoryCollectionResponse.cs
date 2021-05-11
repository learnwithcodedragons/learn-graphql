using System.Collections.Generic;

namespace QuoteOfTheDay.Client.Types
{
    public class CategoryCollectionResponse
    {
        public ICollection<Category> Categories { get; set; }
    }
}
