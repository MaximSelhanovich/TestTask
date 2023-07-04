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

namespace TestTask.Pages.InsurancePolicies
{
    public class CreateModel : PageModel
    {
        private readonly MySqlConnection _connection;

        public CreateModel(MySqlConnection connection)
        {
            _connection = connection;
        }

        public IList<InsPolicyType> InsPolicyTypes { get; set; } = new List<InsPolicyType>();
        public async Task<IActionResult> OnGetAsync()
        {
            var db = new MySqlDBHandler(_connection);
            InsPolicyTypes = await db.GetPolicyTypes();
            return Page();
        }

        [BindProperty]
        public InsurancePolicy InsurancePolicy { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || InsurancePolicy == null)
            {
                return Page();
            }

            await _connection.OpenAsync();
            MySqlCommand command = new()
            {
                Connection = _connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "insert_insurance_policy"
            };
            command.Parameters.AddWithValue("@new_customer_passport_series", 
                                            InsurancePolicy.CustomerPassportSeries);
            command.Parameters.AddWithValue("@new_customer_passport_number", 
                                            InsurancePolicy.CustomerPassportNumber);
            command.Parameters.AddWithValue("@new_worker_id", InsurancePolicy.WorkerId);
            command.Parameters.AddWithValue("@new_office_id", InsurancePolicy.OfficeId);
            command.Parameters.AddWithValue("@new_policy_type_id", InsurancePolicy.PolicyTypeId);
            command.Parameters.AddWithValue("@new_periodic_fee", InsurancePolicy.PeriodicFee);
            command.Parameters.AddWithValue("@new_period_in_weeks", InsurancePolicy.WeeksPerPeriod);

            await command.ExecuteNonQueryAsync();
            await _connection.CloseAsync();

            return RedirectToPage("./Index");
        }
    }
}
