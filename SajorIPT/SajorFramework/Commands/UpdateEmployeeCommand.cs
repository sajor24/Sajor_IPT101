using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SajorIPT101Solution.SajorDomain.Commands;
using SajorIPT101Solution.SajorDomain.Models;
using SajorIPT101Solution.SajorFramework.DTOs;

namespace SajorIPT101Solution.SajorFramework.Commands
{
    public class UpdateEmployeeCommand : IUpdateEmployeeCommand
    {
        private readonly EmployeesDbContextFactory _contextFactory;

        public UpdateEmployeeCommand(EmployeesDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(Employee employee)
        {
            using (EmployeesDbContext context = _contextFactory.Create())
            {
                EmployeeDto employeeDto = new EmployeeDto()
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Age = employee.Age,
                    Position = employee.Position,
                };

                context.Employees.Update(employeeDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
