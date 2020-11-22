using GraphQL;
using GraphQL.Types;
using QuoteOfTheDay.Data;
using QuoteOfTheDay.Entities;
using QuoteOfTheDay.GraphQL.Types;
using System;

namespace QuoteOfTheDay.GraphQL
{
    public class QuoteMutation:  ObjectGraphType
    {
        public QuoteMutation(Defer<QuoteRepository> quoteRepository)
        {
            Field<QuoteType>(
                "createQuote",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<QuoteInputType>> { Name = "quote" }),
                resolve: context =>
                {
                    var quote = context.GetArgument<Quote>("quote");
                    return quoteRepository.Value.AddQuote(quote);
                });

            Field<QuoteType>(
               "updateQuote",
               arguments: new QueryArguments(
                   new QueryArgument<NonNullGraphType<QuoteInputType>> { Name = "quote" },
                   new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "quoteId" }),

               resolve: context =>
               {
                   var quote = context.GetArgument<Quote>("quote");
                   var quoteId = context.GetArgument<int>("quoteId");
                  
                   quote.Id = quoteId;
                   
                   return quoteRepository.Value.UpdateQuote(quote);
               });


            Field<StringGraphType>(
               "deleteQuote",
               arguments: new QueryArguments(
                   new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "quoteId" }),
               resolve: context =>
               {
                   var quoteId = context.GetArgument<int>("quoteId");
                   quoteRepository.Value.DeleteQuote(quoteId);
                   return "Quote Deleted";
               });

        }
    }
}
