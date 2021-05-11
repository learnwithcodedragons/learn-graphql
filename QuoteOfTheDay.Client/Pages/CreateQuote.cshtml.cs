using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuoteOfTheDay.Client.Types;

namespace QuoteOfTheDay.Client
{
    public class CreateQuoteModel : PageModel
    {
        private readonly QuoteOfTheDayApiClient _quoteClient;

        public CreateQuoteModel(QuoteOfTheDayApiClient quoteClient)
        {
            _quoteClient = quoteClient;
        }

        public List<SelectListItem> Categories { get; set; }

        [BindProperty]
        public QuoteInput Quote { get; set; }

        public async Task OnGet()
        {
            var categories = await _quoteClient.GetCategories();

            Categories = categories
                .Select( c => new SelectListItem {
                    Value = c.Id.ToString(), Text = c.Name})
                .ToList();
        }

        public async Task<IActionResult> OnPost()
        {
            await _quoteClient.CreateQuote(Quote);
            return Redirect("/");
        }
    }
}