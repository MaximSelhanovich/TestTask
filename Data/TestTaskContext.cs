using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestTask.Models;

namespace TestTask.Data
{
    public class TestTaskContext : DbContext
    {
        public TestTaskContext (DbContextOptions<TestTaskContext> options)
            : base(options)
        {
        }

        public DbSet<TestTask.Models.User> User { get; set; } = default!;

        public DbSet<TestTask.Models.InsPolicyType> PolicyType { get; set; } = default!;

        public DbSet<TestTask.Models.Office> Office { get; set; } = default!;

        public DbSet<TestTask.Models.InsurancePolicy> InsurancePolicy { get; set; } = default!;

        public DbSet<TestTask.Models.Role> Role { get; set; } = default!;
    }
}
