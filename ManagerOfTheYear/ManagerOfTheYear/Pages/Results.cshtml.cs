using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace ManagerOfTheYear.Pages
{
    public class ResultsModel : PageModel
    {
        public ConnectionStrings _connection { get; set; }

        public ResultsModel(Microsoft.Extensions.Options.IOptionsSnapshot<ConnectionStrings> connection)
        {
            _connection = connection.Value;
        }

        public void OnGet()
        {
            int total = 0;
            int davidCount = 0;
            int tommyCount = 0;

            using (SqlConnection con = new Microsoft.Data.SqlClient.SqlConnection(_connection.DefaultConnection))
            {
                con.Open();
                
                using (SqlCommand sqlCommand = new SqlCommand("select count(*) as cnt, vote from votes " + 
                                                                " where vote = 1 or vote = 2 " + 
                                                                " group by vote", con))
                {
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader[1].ToString() == "1")
                        {
                            davidCount = (int)reader[0];
                            ViewData["DavidCount"] = reader[0].ToString();
                        }
                        else
                        {
                            tommyCount  = (int)reader[0];
                            ViewData["TommyCount"] = reader[0].ToString();
                        }
                    }
                }
            }

            total = davidCount + tommyCount;

            ViewData["DavidPercentage"] = (int)(Math.Round((decimal)davidCount / (decimal)total, 2) * 100);
            ViewData["TommyPercentage"] = (int)(Math.Round((decimal)tommyCount / (decimal)total, 2) * 100);
            ViewData["Total"] = total;
        }
    }
}