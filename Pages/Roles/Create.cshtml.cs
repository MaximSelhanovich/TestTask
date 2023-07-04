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

namespace TestTask.Pages.Roles
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
        public Role Role { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || Role == null)
            {
                return Page();
            }
            await _connection.OpenAsync();
            MySqlCommand command = new()
            {
                Connection = _connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "insert_role"
            };
            command.Parameters.AddWithValue("@new_name", Role.Name);
            await command.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
            return RedirectToPage("./Index");
        }
    }
}
