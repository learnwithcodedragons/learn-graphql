using GraphQL.Types;
using QuoteOfTheDay.Data;
using QuoteOfTheDay.Entities;

namespace QuoteOfTheDay.GraphQL.Types
{
    public class QuoteType : ObjectGraphType<Quote>
    {
        public QuoteType()
        {
            Field(t => t.Id);
            Field(t => t.Author).Description("The name of the person the quote is attributed to");
            Field(t => t.Text).Description("The text of the quote");
        }
    }
}
