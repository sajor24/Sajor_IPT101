using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SajorIPT101.SajorWPF.Stores;
using SajorIPT101.SajorWPF.ViewModels;
using SajorIPT101Solution.SajorDomain.Models;

namespace SajorIPT101.SajorWPF.Commands
{
    public class AddEmployeeCommand : AsyncCommandBase
    {
        private readonly EmployeeDetailsFormViewModel _employeeDetailsFormViewModel;
        private readonly EmployeesStore _employeesStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public AddEmployeeCommand(EmployeeDetailsFormViewModel employeeDetailsFormViewModel,
            EmployeesStore employeesStore,
            ModalNavigationStore modalNavigationStore)
        {
            _employeeDetailsFormViewModel = employeeDetailsFormViewModel;
            _employeesStore = employeesStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            EmployeeDetailsViewModel employeeDetails = _employeeDetailsFormViewModel.EmployeeDetails;

            Employee employee = new Employee(
                Guid.NewGuid(),
                employeeDetails.FirstName,
                employeeDetails.LastName,
                employeeDetails.Age,
                employeeDetails.Position);

            await _employeesStore.Add(employee);

            _modalNavigationStore.Close();
        }
    }
}
