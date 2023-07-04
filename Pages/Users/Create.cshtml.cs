using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySqlConnector;
using Newtonsoft.Json;
using TestTask.Data;
using TestTask.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TestTask.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly MySqlConnection _connection;

        public CreateModel(MySqlConnection connection)
        {
            _connection = connection;
        }
        public IList<Role> Role { get; set; } = new List<Role>();
        public async Task<IActionResult> OnGetAsync()
        {
            var db = new MySqlDBHandler(_connection);

            Role = await db.GetRoles();
            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || User == null)
            {
                return Page();
            }
            await _connection.OpenAsync();
            MySqlCommand command = new()
            {
                Connection = _connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "insert_user"
            };
            command.Parameters.AddWithValue("@new_name", User.Name);
            command.Parameters.AddWithValue("@new_surname", User.Surname);
            command.Parameters.AddWithValue("@new_email", User.Email);
            command.Parameters.AddWithValue("@new_pswd", User.Password);
            command.Parameters.AddWithValue("@new_role", User.Role);

            await command.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
            return RedirectToPage("./Index");
        }
    }
}
