using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SajorIPT101Solution.SajorDomain.Commands;

namespace SajorIPT101Solution.SajorFramework.Commands
{
    public class DeleteEmployeeCommand : IDeleteEmployeeCommand
    {
        private readonly EmployeesDbContextFactory _contextFactory;

        public DeleteEmployeeCommand(EmployeesDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(Guid id)
        {
            using (EmployeesDbContext context = _contextFactory.Create())
            {
                var employeeDto = await context.Employees.FindAsync(id);
                if (employeeDto != null)
                {
                    context.Employees.Remove(employeeDto);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
