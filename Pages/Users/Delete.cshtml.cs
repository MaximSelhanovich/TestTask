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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TestTask.Pages.Users
{
    public class DeleteModel : PageModel
    {
        private readonly MySqlConnection _connection;

        public DeleteModel(MySqlConnection connection)
        {
            _connection = connection;
        }

        [BindProperty]
        public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var db = new MySqlDBHandler(_connection);
            User = await db.GetUserById((uint)id);

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
                CommandText = "delete_user"
            };
            command.Parameters.AddWithValue("@id_user", User.Id);
            await command.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
            return RedirectToPage("./Index");
        }
    }
}
