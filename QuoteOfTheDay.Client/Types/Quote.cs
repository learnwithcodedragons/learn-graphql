

namespace QuoteOfTheDay.Client.Types
{
    public class Quote
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
        public Category Category { get; set; }
    }
}
