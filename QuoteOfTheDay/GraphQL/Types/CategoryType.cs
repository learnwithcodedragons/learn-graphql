using GraphQL.Types;
using QuoteOfTheDay.Entities;

namespace QuoteOfTheDay.GraphQL.Types
{
    public class CategoryType :ObjectGraphType<Category>
    {
        public CategoryType()
        {
            Field( t => t.Id);
            Field(t => t.Name).Description("The category description");
        }
    }
}
