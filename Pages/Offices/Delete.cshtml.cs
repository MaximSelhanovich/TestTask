using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using TestTask.Data;
using TestTask.Models;

namespace TestTask.Pages.Offices
{
    public class DeleteModel : PageModel
    {
        private readonly MySqlConnection _connection;

        public DeleteModel(MySqlConnection connection)
        {
            _connection = connection;
        }

        [BindProperty]
      public Office Office { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var db = new MySqlDBHandler(_connection);
            Office = await db.GetOfficeById((uint)id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await _connection.OpenAsync();
            MySqlCommand command = new()
            {
                Connection = _connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "delete_office"
            };
            command.Parameters.AddWithValue("@id_office", Office.Id);
            await command.ExecuteNonQueryAsync();
            await _connection.CloseAsync();

            return RedirectToPage("./Index");
        }
    }
}
