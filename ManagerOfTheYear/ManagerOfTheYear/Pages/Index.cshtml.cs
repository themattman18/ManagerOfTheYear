using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;

namespace ManagerOfTheYear.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public ConnectionStrings _connection { get; set; }

        public IndexModel(ILogger<IndexModel> logger, Microsoft.Extensions.Options.IOptionsSnapshot<ConnectionStrings> connection)
        {
            _logger = logger;
            _connection = connection.Value;
        }

        public IActionResult OnGet()
        {
            if (Request.Cookies["hasVoted"] != null)
            {
                return Redirect("/Results");
            }

            return Page();
        }

        public ActionResult OnGetVote(int id)
        {
            Response.Cookies.Append("hasVoted", "true");

            using (Microsoft.Data.SqlClient.SqlConnection con = new Microsoft.Data.SqlClient.SqlConnection(_connection.DefaultConnection))
            {
                con.Open();
                using (SqlCommand sqlCommand = new SqlCommand("Insert into votes values(@vote)", con))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@vote", id));
                    sqlCommand.ExecuteNonQuery();
                }
            }

            // log results
            return null;

        }
    }
}
