using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ManagerOfTheYear.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            if (Request.Cookies["hasVoted"] != null)
            {
                return Redirect("/Results");
            }

            return Page();
        }

        public async Task<IActionResult> OnGetVote(int id)
        {
            Response.Cookies.Append("hasVoted", "true");

            // log results
            return null;

        }
    }
}
