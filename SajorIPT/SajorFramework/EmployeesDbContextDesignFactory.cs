using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SajorIPT101Solution.SajorFramework
{
    public class EmployeesDbContextDesignFactory : IDesignTimeDbContextFactory<EmployeesDbContext>
    {
        public EmployeesDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EmployeesDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SajorWPFDb;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new EmployeesDbContext(optionsBuilder.Options);
        }
    }
}
