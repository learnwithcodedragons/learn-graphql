using GraphQL;
using GraphQL.Client.Http;
using QuoteOfTheDay.Client.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuoteOfTheDay.Client
{
    public class QuoteOfTheDayApiClient
    {
        private readonly GraphQLHttpClient _graphQlClient;

        public QuoteOfTheDayApiClient(GraphQLHttpClient graphQlClient)
        {
            _graphQlClient = graphQlClient;
        }

        public async Task<ICollection<Quote>> GetAllQuotes()
        {
            var request = new GraphQLRequest
            {
                Query = @"{quotes {
                               id
                               author
                               text
                               category {
                                   name
                                   }
                           }}"
            };

            var response = await _graphQlClient.SendQueryAsync<QuoteCollectionResponse>(request);
            return response.Data.Quotes;
        }

        public async Task<Quote> CreateQuote(QuoteInput quote)
        {
            var request = new GraphQLRequest
            {
                Query = @"mutation($quote: quoteInput!){
                             createQuote(quote: $quote){
                                id
                                author
                                text
                                category{
                                    name
                                  }
                              }
                           }",
                Variables = new { quote }
            };

            var response = await _graphQlClient.SendMutationAsync<QuoteResponse>(request);
            return response.Data.Quote;
        }
    }

}
