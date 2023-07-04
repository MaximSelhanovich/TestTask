using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySqlConnector;
using TestTask.Data;
using TestTask.Models;

namespace TestTask.Pages.PolicyType
{
    public class CreateModel : PageModel
    {
        private readonly MySqlConnection _connection;

        public CreateModel(MySqlConnection connection)
        {
            _connection = connection;
        }


        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public InsPolicyType InsPolicyType { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || InsPolicyType == null)
            {
                return Page();
            }
            await _connection.OpenAsync();
            MySqlCommand command = new()
            {
                Connection = _connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "insert_policy_type"
            };
            command.Parameters.AddWithValue("@new_name", InsPolicyType.Name);
            command.Parameters.AddWithValue("@new_insured_sum", InsPolicyType.InsuredSum);
            await command.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
            return RedirectToPage("./Index");
        }
    }
}
