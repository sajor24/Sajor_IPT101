using Microsoft.EntityFrameworkCore;
using SajorWPF.Models;

namespace SajorWPF.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees => Set<Employee>();
    }
}
