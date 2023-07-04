using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;
using System.Data.Common;

namespace TestTask.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly MySqlConnection _connection;

        public IndexModel(ILogger<IndexModel> logger, MySqlConnection connection)
        {
            _connection = connection;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            await _connection.CloseAsync();
            await _connection.OpenAsync();
            using var command = new MySqlCommand("SELECT * FROM roles;", _connection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var value = reader.GetValue(1);
                ViewData["test"] = value;
                // do something with 'value'
            }
            await _connection.CloseAsync();
        }
    }
}