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

namespace TestTask.Pages.InsurancePolicies
{
    public class EditModel : PageModel
    {
        private readonly TestTask.Data.TestTaskContext _context;

        public EditModel(TestTask.Data.TestTaskContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InsurancePolicy InsurancePolicy { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(uint? id)
        {
            if (id == null || _context.InsurancePolicy == null)
            {
                return NotFound();
            }

            var insurancepolicy =  await _context.InsurancePolicy.FirstOrDefaultAsync(m => m.Id == id);
            if (insurancepolicy == null)
            {
                return NotFound();
            }
            InsurancePolicy = insurancepolicy;
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

            _context.Attach(InsurancePolicy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InsurancePolicyExists(InsurancePolicy.Id))
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

        private bool InsurancePolicyExists(uint id)
        {
          return (_context.InsurancePolicy?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
