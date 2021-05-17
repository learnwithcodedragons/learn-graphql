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

        public async Task<Quote> GetById(int id)
        {
            var request = new GraphQLRequest
            {
                Query = @"query($id: ID!){
                              quote(id: $id){
                                  id
                                  text
                                  author
                                  category
                                  {
                                      id
                                      name
                                  }
                               }
                           }",
                Variables = new { id }
            };

            var response = await _graphQlClient.SendQueryAsync<QuoteResponse>(request);
            return response.Data.Quote;
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

        public async Task<Quote> UpdateQuote(int id, QuoteInput quote)
        {
            var quoteRequest = new GraphQLRequest
            {
                Query = @"mutation($id: ID!, $quote: quoteInput!){
                                  updateQuote(quoteId: $id, quote: $quote){
                                      id
                                      text
                                      author
                                      category
                                      {
                                           id
                                           name
                                      }
                                  }
                              }",
                Variables = new
                {
                    quote,
                    id
                }
            };

            var response = await _graphQlClient.SendQueryAsync<QuoteResponse>(quoteRequest);
            return response.Data.Quote;
        }

        public async Task<ICollection<Category>> GetCategories()
        {
            var request = new GraphQLRequest
            {
                Query = @" { categories { id 
                                        name
                                        }
                                    }"
            };

            var response = await _graphQlClient.SendQueryAsync<CategoryCollectionResponse>(request);
            return response.Data.Categories;
        }

        public async Task DeleteQuote(int id)
        {
            var request = new GraphQLRequest
            {
                Query = @"mutation($id: ID!) {
                              deleteQuote(quoteId: $id)
                           }",
                Variables = new { id }
            };

            await _graphQlClient.SendMutationAsync<StringResponse>(request);
        }
    }

}
