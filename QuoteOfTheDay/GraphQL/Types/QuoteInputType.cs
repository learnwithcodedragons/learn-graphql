using GraphQL.Types;

namespace QuoteOfTheDay.GraphQL
{
    public class QuoteInputType : InputObjectGraphType
    {
        public QuoteInputType()
        {
            Name = "quoteInput";
            Field<NonNullGraphType<StringGraphType>>("author");
            Field<NonNullGraphType<StringGraphType>>("text");
            Field<NonNullGraphType<IntGraphType>>("categoryid");
        }
    }
}
