using MySqlConnector;
using System.Data;
using TestTask.Models;

namespace TestTask.Data
{
    public class MySqlDBHandler
    {
        private readonly MySqlConnection _connection;

        public MySqlDBHandler(MySqlConnection connection)
        {
            _connection = connection;
        }
        public async Task<User> GetUserById(uint id)
        {
            await _connection.OpenAsync();
            MySqlCommand command = new()
            {
                Connection = _connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "get_user_by_id"
            };
            command.Parameters.AddWithValue("@id_user", id);
            var reader = await command.ExecuteReaderAsync();
            User user = default;
            while (await reader.ReadAsync())
            {
                var u = new User
                {
                    Id = reader.GetUInt32("id"),
                    Name = reader.GetString("name"),
                    Surname = reader.GetString("surname"),
                    Email = reader.GetString("email"),
                    Role = reader.GetString("role_name")
                };
                user = u;
            }
            await reader.CloseAsync();
            await _connection.CloseAsync();
            return user;
        }

        public async void InsertUser(User user)
        {
            await _connection.OpenAsync();
            MySqlCommand command = new()
            {
                Connection = _connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "insert_user"
            };
            command.Parameters.AddWithValue("@new_name", user.Name);
            command.Parameters.AddWithValue("@new_surname", user.Surname);
            command.Parameters.AddWithValue("@new_email", user.Email);
            command.Parameters.AddWithValue("@new_pswd", user.Password);
            command.Parameters.AddWithValue("@new_role", user.Role);

            await command.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
        }

        public async Task<List<Role>> GetRoles()
        {
            await _connection.OpenAsync();

            MySqlCommand command = new()
            {
                Connection = _connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "get_roles"
            };
            var roles = new List<Role>();
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var user = new Role
                {
                    Id = reader.GetUInt32("id"),
                    Name = reader.GetString("name"),
                };
                roles.Add(user);
            }
            await reader.CloseAsync();
            await _connection.CloseAsync();
            return roles;
        }

        public async Task<Role> GetRoleById(uint id)
        {
            await _connection.OpenAsync();
            MySqlCommand command = new()
            {
                Connection = _connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "get_role_by_id"
            };
            command.Parameters.AddWithValue("@id_role", id);
            var reader = await command.ExecuteReaderAsync();
            Role role = default;
            while (await reader.ReadAsync())
            {
                var r = new Role
                {
                    Id = reader.GetUInt32("id"),
                    Name = reader.GetString("name"),
                };
                role = r;
            }
            await reader.CloseAsync();
            await _connection.CloseAsync();
            return role;
        }


        //policy types
        public async Task<List<InsPolicyType>> GetPolicyTypes()
        {
            await _connection.OpenAsync();

            MySqlCommand command = new()
            {
                Connection = _connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "get_policy_types"
            };
            var types = new List<InsPolicyType>();
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var user = new InsPolicyType
                {
                    Id = reader.GetUInt32("id"),
                    Name = reader.GetString("name"),
                    InsuredSum = reader.GetUInt32("insured_sum")
                };
                types.Add(user);
            }
            await reader.CloseAsync();
            await _connection.CloseAsync();
            return types;
        }

        public async void InsertPolicyType(InsPolicyType policyType)
        {
            await _connection.OpenAsync();
            MySqlCommand command = new()
            {
                Connection = _connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "insert_policy_type"
            };
            command.Parameters.AddWithValue("@new_name", policyType.Name);
            command.Parameters.AddWithValue("@new_insured_sum", policyType.InsuredSum);
            await command.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
        }

        public async Task<InsPolicyType> GetPolicyTypeById(uint id)
        {
            await _connection.OpenAsync();
            MySqlCommand command = new()
            {
                Connection = _connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "get_policy_type_by_id"
            };
            command.Parameters.AddWithValue("@id_policy_type", id);
            var reader = await command.ExecuteReaderAsync();
            InsPolicyType policyType = default;
            while (await reader.ReadAsync())
            {
                var p = new InsPolicyType
                {
                    Id = reader.GetUInt32("id"),
                    Name = reader.GetString("name"),
                    InsuredSum = reader.GetUInt32("insured_sum"),
                };
                policyType = p;
            }
            await reader.CloseAsync();
            await _connection.CloseAsync();
            return policyType;
        }


        public async Task<Office> GetOfficeById(uint id)
        {
            await _connection.OpenAsync();
            MySqlCommand command = new()
            {
                Connection = _connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "get_office_by_id"
            };
            command.Parameters.AddWithValue("@id_office", id);
            var reader = await command.ExecuteReaderAsync();
            Office office = default;
            while (await reader.ReadAsync())
            {
                var o = new Office
                {
                    Id = reader.GetUInt32("id"),
                    PhoneNumber = reader.GetString("phone_number"),
                    Address = reader.GetString("address"),
                };
                office = o;
            }
            await reader.CloseAsync();
            await _connection.CloseAsync();
            return office;
        }

        public async Task<IList<Office>> GetOfficies()
        {
            await _connection.OpenAsync();

            MySqlCommand command = new()
            {
                Connection = _connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "get_offices"
            };
            var offices = new List<Office>();
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var o = new Office
                {
                    Id = reader.GetUInt32("id"),
                    PhoneNumber = reader.GetString("phone_number"),
                    Address = reader.GetString("address"),
                };
                offices.Add(o);
            }
            await reader.CloseAsync();
            await _connection.CloseAsync();
            return offices;
        }

        /*Ins*/
        public async Task<List<InsurancePolicy>> GetInsurancePolicies()
        {
            await _connection.OpenAsync();

            MySqlCommand command = new()
            {
                Connection = _connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "get_insurance_policies"
            };
            var policies = new List<InsurancePolicy>();
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var policy = new InsurancePolicy
                {
                    Id = reader.GetUInt32("id"),
                    CustomerPassportSeries = reader.GetString("customer_passport_series"),
                    CustomerPassportNumber = reader.GetString("customer_passport_number"),
                    WorkerId = reader.GetUInt32("worker_id"),
                    OfficeId = reader.GetUInt32("office_id"),
                    PolicyTypeId = reader.GetUInt32("policy_type_id"),
                    PeriodicFee = reader.GetUInt32("periodic_fee"),
                    WeeksPerPeriod = reader.GetUInt32("period_in_weeks")
                };
                policies.Add(policy);
            }
            await reader.CloseAsync();
            await _connection.CloseAsync();
            return policies;
        }

        public async Task<InsurancePolicy> GetInsurancePolicyById(uint id)
        {
            await _connection.OpenAsync();
            MySqlCommand command = new()
            {
                Connection = _connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "get_insurance_policy_by_id"
            };
            command.Parameters.AddWithValue("@id_insurance_policy", id);
            var reader = await command.ExecuteReaderAsync();
            InsurancePolicy policy = default;
            while (await reader.ReadAsync())
            {
                var p = new InsurancePolicy
                {
                    Id = reader.GetUInt32("id"),
                    CustomerPassportSeries = reader.GetString("customer_passport_series"),
                    CustomerPassportNumber = reader.GetString("customer_passport_number"),
                    WorkerId = reader.GetUInt32("worker_id"),
                    OfficeId = reader.GetUInt32("office_id"),
                    PolicyTypeId = reader.GetUInt32("policy_type_id"),
                    PeriodicFee = reader.GetUInt32("periodic_fee"),
                    WeeksPerPeriod = reader.GetUInt32("period_in_weeks")
                };
                policy = p;
            }
            await reader.CloseAsync();
            await _connection.CloseAsync();
            return policy;
        }
    }
}
