using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;

namespace TestTask.Pages.Offices
{
    public class EditModel : PageModel
    {
        private readonly TestTask.Data.TestTaskContext _context;

        public EditModel(TestTask.Data.TestTaskContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Office Office { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(uint? id)
        {
            if (id == null || _context.Office == null)
            {
                return NotFound();
            }

            var office =  await _context.Office.FirstOrDefaultAsync(m => m.Id == id);
            if (office == null)
            {
                return NotFound();
            }
            Office = office;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Office).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfficeExists(Office.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool OfficeExists(uint id)
        {
          return (_context.Office?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
