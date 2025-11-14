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
    public class EditEmployeeCommand : AsyncCommandBase
    {
        private readonly EmployeeDetailsFormViewModel _employeeDetailsFormViewModel;
        private readonly EmployeesStore _employeesStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly Guid _employeeId;

        public EditEmployeeCommand(Guid employeeId,
            EmployeeDetailsFormViewModel employeeDetailsFormViewModel,
            EmployeesStore employeesStore,
            ModalNavigationStore modalNavigationStore)
        {
            _employeeId = employeeId;
            _employeeDetailsFormViewModel = employeeDetailsFormViewModel;
            _employeesStore = employeesStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            EmployeeDetailsViewModel employeeDetails = _employeeDetailsFormViewModel.EmployeeDetails;

            Employee employee = new Employee(
                _employeeId,
                employeeDetails.FirstName,
                employeeDetails.LastName,
                employeeDetails.Age,
                employeeDetails.Position);

            await _employeesStore.Update(employee);

            _modalNavigationStore.Close();
        }
    }
}
