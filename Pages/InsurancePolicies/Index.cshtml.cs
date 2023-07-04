using System;
using System.Collections.Generic;
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
    public class IndexModel : PageModel
    {
        private readonly MySqlConnection _connection;

        public IndexModel(MySqlConnection connection)
        {
            _connection = connection;
        }

        public IList<InsurancePolicy> InsurancePolicy { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var db = new MySqlDBHandler(_connection);
            InsurancePolicy = await db.GetInsurancePolicies();
        }
    }
}
