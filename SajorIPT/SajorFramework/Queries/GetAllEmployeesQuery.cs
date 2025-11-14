using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SajorIPT101Solution.SajorDomain.Models;
using SajorIPT101Solution.SajorDomain.Queries;
using SajorIPT101Solution.SajorFramework.DTOs;

namespace SajorIPT101Solution.SajorFramework.Queries
{
    public class GetAllEmployeesQuery : IGetAllEmployeesQuery
    {
        private readonly EmployeesDbContextFactory _contextFactory;

        public GetAllEmployeesQuery(EmployeesDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Employee>> Execute()
        {
            using (EmployeesDbContext context = _contextFactory.Create())
            {
                IEnumerable<EmployeeDto> employeeDtos = await context.Employees.ToListAsync();

                return employeeDtos.Select(e => new Employee(e.Id, e.FirstName, e.LastName, e.Age, e.Position));
            }
        }
    }
}
