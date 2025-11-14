using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SajorIPT101.SajorWPF.Stores;
using SajorIPT101Solution.SajorDomain.Models;

namespace SajorIPT101.SajorWPF.Commands
{
    public class DeleteEmployeeCommand : AsyncCommandBase
    {
        private readonly EmployeesStore _employeesStore;

        public DeleteEmployeeCommand(EmployeesStore employeesStore)
        {
            _employeesStore = employeesStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter is Employee employee)
            {
                await _employeesStore.Delete(employee.Id);
            }
        }
    }
}
