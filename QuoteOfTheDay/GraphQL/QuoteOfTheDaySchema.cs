using GraphQL.Types;
using System;

namespace QuoteOfTheDay.GraphQL
{
    public class QuoteOfTheDaySchema : Schema
    {
        public QuoteOfTheDaySchema(IServiceProvider provider, 
            QuoteQuery quoteQuery, QuoteMutation quoteMutation) : base(provider)
        {
            Query = quoteQuery;
            Mutation = quoteMutation;
        }
    }
}
