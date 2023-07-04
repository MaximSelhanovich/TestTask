using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;

namespace TestTask.Pages.InsurancePolicies
{
    public class DetailsModel : PageModel
    {
        private readonly TestTask.Data.TestTaskContext _context;

        public DetailsModel(TestTask.Data.TestTaskContext context)
        {
            _context = context;
        }

      public InsurancePolicy InsurancePolicy { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(uint? id)
        {
            if (id == null || _context.InsurancePolicy == null)
            {
                return NotFound();
            }

            var insurancepolicy = await _context.InsurancePolicy.FirstOrDefaultAsync(m => m.Id == id);
            if (insurancepolicy == null)
            {
                return NotFound();
            }
            else 
            {
                InsurancePolicy = insurancepolicy;
            }
            return Page();
        }
    }
}
