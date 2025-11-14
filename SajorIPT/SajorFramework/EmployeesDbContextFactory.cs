using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SajorIPT101Solution.SajorFramework
{
    public class EmployeesDbContextFactory
    {
        private readonly DbContextOptions _options;

        public EmployeesDbContextFactory(DbContextOptions options)
        {
            _options = options;
        }

        public EmployeesDbContext Create()
        {
            return new EmployeesDbContext(_options);
        }
    }
}
