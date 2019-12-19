using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ManagerOfTheYear.Pages
{
    public class ResultsModel : PageModel
    {
        public ConnectionStrings _connection { get; set; }

        public ResultsModel(Microsoft.Extensions.Options.IOptionsSnapshot<ConnectionStrings> connection)
        {
            _connection = connection.Value;
        }

        public IActionResult OnGet()
        {
            return Page();

        }
    }
}