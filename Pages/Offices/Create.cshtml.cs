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

namespace TestTask.Pages.Offices
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
        public Office Office { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Office == null)
            {
                return Page();
            }
            await _connection.OpenAsync();
            MySqlCommand command = new()
            {
                Connection = _connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "insert_office"
            };
            command.Parameters.AddWithValue("@new_phone_number", Office.PhoneNumber);
            command.Parameters.AddWithValue("@new_address", Office.Address);
            await command.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
            return RedirectToPage("./Index");
        }
    }
}
