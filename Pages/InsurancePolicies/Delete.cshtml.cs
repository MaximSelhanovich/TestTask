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

namespace TestTask.Pages.InsurancePolicies
{
    public class DeleteModel : PageModel
    {
        private readonly MySqlConnection _connection;

        public DeleteModel(MySqlConnection connection)
        {
            _connection = connection;
        }

        [BindProperty]
      public InsurancePolicy InsurancePolicy { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var db = new MySqlDBHandler(_connection);
            InsurancePolicy = await db.GetInsurancePolicyById((uint)id);
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
                CommandText = "delete_insurance_policy"
            };
            command.Parameters.AddWithValue("@id_insurance_policy", InsurancePolicy.Id);
            await command.ExecuteNonQueryAsync();
            await _connection.CloseAsync();

            return RedirectToPage("./Index");
        }
    }
}
