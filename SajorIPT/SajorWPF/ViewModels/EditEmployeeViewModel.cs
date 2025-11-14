using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SajorIPT101.SajorWPF.Commands;
using SajorIPT101.SajorWPF.Stores;
using SajorIPT101Solution.SajorDomain.Models;

namespace SajorIPT101.SajorWPF.ViewModels
{
    public class EditEmployeeViewModel : ViewModelBase
    {
        public EmployeeDetailsFormViewModel EmployeeDetailsFormViewModel { get; }

        public EditEmployeeViewModel(Employee employee, EmployeesStore employeesStore, ModalNavigationStore modalNavigationStore)
        {
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);
            EmployeeDetailsFormViewModel = new EmployeeDetailsFormViewModel(null, cancelCommand);
            
            ICommand submitCommand = new EditEmployeeCommand(employee.Id, EmployeeDetailsFormViewModel, employeesStore, modalNavigationStore);
            EmployeeDetailsFormViewModel.SubmitCommand = submitCommand;

            EmployeeDetailsFormViewModel.EmployeeDetails.FirstName = employee.FirstName;
            EmployeeDetailsFormViewModel.EmployeeDetails.LastName = employee.LastName;
            EmployeeDetailsFormViewModel.EmployeeDetails.Age = employee.Age;
            EmployeeDetailsFormViewModel.EmployeeDetails.Position = employee.Position;
        }
    }
}
