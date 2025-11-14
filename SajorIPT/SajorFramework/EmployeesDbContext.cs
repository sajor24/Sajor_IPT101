using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SajorIPT101Solution.SajorFramework.DTOs;

namespace SajorIPT101Solution.SajorFramework
{
    public class EmployeesDbContext : DbContext
    {
        public EmployeesDbContext(DbContextOptions options) : base(options) { }

        public DbSet<EmployeeDto> Employees { get; set; }
    }
}
