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
    public class OpenEditEmployeeCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly SelectedEmployeeStore _selectedEmployeeStore;
        private readonly EmployeesStore _employeesStore;

        public OpenEditEmployeeCommand(ModalNavigationStore modalNavigationStore, 
            SelectedEmployeeStore selectedEmployeeStore,
            EmployeesStore employeesStore)
        {
            _modalNavigationStore = modalNavigationStore;
            _selectedEmployeeStore = selectedEmployeeStore;
            _employeesStore = employeesStore;
        }

        public override void Execute(object parameter)
        {
            Employee employee = parameter as Employee;
            if (employee != null)
            {
                _selectedEmployeeStore.SelectedEmployee = employee;
                EditEmployeeViewModel editEmployeeViewModel = new EditEmployeeViewModel(employee, _employeesStore, _modalNavigationStore);
                _modalNavigationStore.CurrentViewModel = editEmployeeViewModel;
            }
        }
    }
}
