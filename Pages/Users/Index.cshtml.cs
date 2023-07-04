using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Newtonsoft.Json;
using NuGet.Packaging;
using TestTask.Data;
using TestTask.Models;

namespace TestTask.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly MySqlConnection _connection;

        public IndexModel(MySqlConnection connection)
        {
            _connection = connection;
        }

        public IList<User> User { get; set; } = new List<User>();
        public IList<Role> Roles { get; set; } = new List<Role>();
        public async Task OnGetAsync()
        {
            await _connection.OpenAsync();
            MySqlCommand command = new()
            {
                Connection = _connection,
                CommandType = CommandType.Text,
                CommandText = "get_users"
            };
            command.CommandType = CommandType.StoredProcedure;
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var user = new User
                {
                    Id = reader.GetUInt32("id"),
                    Name = reader.GetString("name"),
                    Surname = reader.GetString("surname"),
                    Email = reader.GetString("email"),
                    Role = reader.GetString("role_name")
                };
                User.Add(user);
            }
            await reader.CloseAsync();
            await _connection.CloseAsync();
            var db = new MySqlDBHandler(_connection);

            var tmp = await db.GetRoles();
            Roles.AddRange(tmp);
            await _connection.CloseAsync();
        }
    }
}
