using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using TestTask.Data;
using TestTask.Models;

namespace TestTask.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly MySqlConnection _connection;

        public EditModel(MySqlConnection connection)
        {
            _connection = connection;
        }

        [BindProperty]
        public User User { get; set; } = default!;
        public IList<Role> Role { get; set; } = new List<Role>();

        public async Task<IActionResult> OnGetAsync(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var db = new MySqlDBHandler(_connection);
            User = await db.GetUserById((uint)id);
            Role = await db.GetRoles();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

           /* _context.Attach(User).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(User.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
           */
            return RedirectToPage("./Index");
        }
    }
}
