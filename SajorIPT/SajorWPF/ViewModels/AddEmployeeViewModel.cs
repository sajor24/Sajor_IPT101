using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SajorIPT101.SajorWPF.Commands;
using SajorIPT101.SajorWPF.Stores;

namespace SajorIPT101.SajorWPF.ViewModels
{
    public class AddEmployeeViewModel : ViewModelBase
    {
        public EmployeeDetailsFormViewModel EmployeeDetailsFormViewModel { get; }

        public AddEmployeeViewModel(EmployeesStore employeesStore, ModalNavigationStore modalNavigationStore)
        {
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);
            EmployeeDetailsFormViewModel = new EmployeeDetailsFormViewModel(null, cancelCommand);
            
            ICommand submitCommand = new AddEmployeeCommand(EmployeeDetailsFormViewModel, employeesStore, modalNavigationStore);
            EmployeeDetailsFormViewModel.SubmitCommand = submitCommand;
        }

        private AddEmployeeViewModel(EmployeeDetailsFormViewModel employeeDetailsFormViewModel)
        {
            EmployeeDetailsFormViewModel = employeeDetailsFormViewModel;
        }
    }
}
