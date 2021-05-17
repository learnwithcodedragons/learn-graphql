using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuoteOfTheDay.Client.Types;

namespace QuoteOfTheDay.Client
{
    public class UpdateQuoteModel : PageModel
    {
        private readonly QuoteOfTheDayApiClient _quoteClient;

        public UpdateQuoteModel(QuoteOfTheDayApiClient quoteClient)
        {
            _quoteClient = quoteClient;
        }

        [BindProperty]
        public QuoteInput Quote { get; set; } = new QuoteInput();

        [BindProperty]
        public int Id { get; set; }

        public List<SelectListItem> Categories { get; set; }

        public async Task OnGet(int id)
        {
            var categories = await _quoteClient.GetCategories();

            Categories = categories
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .ToList();

            var quote = await _quoteClient.GetById(id);

            Id = quote.Id;
            Quote.Author = quote.Author;
            Quote.Text = quote.Text;
            Quote.CategoryId = quote.Category.Id;
        }

        public async Task<ActionResult> OnPost()
        {
            await _quoteClient.UpdateQuote(Id, Quote);
            return Redirect("/");
        }
    }
}