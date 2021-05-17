using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using QuoteOfTheDay.Client.Types;

namespace QuoteOfTheDay.Client.Pages
{
    public class IndexModel : PageModel
    {
        private readonly QuoteOfTheDayApiClient _quoteClient;

        public ICollection<Quote> Quotes { get; set; }

        [BindProperty]
        public int Id { get; set; }

        public IndexModel(QuoteOfTheDayApiClient quoteClient)
        {
            _quoteClient = quoteClient;
        }

        public async Task OnGet()
        {
            Quotes = await _quoteClient.GetAllQuotes();
        }

        public async Task<ActionResult> OnPost()
        {
            await _quoteClient.DeleteQuote(Id);
            return Redirect("/");
        }
    }
}
