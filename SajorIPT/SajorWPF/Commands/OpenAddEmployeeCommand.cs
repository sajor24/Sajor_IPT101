using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SajorIPT101.SajorWPF.Stores;
using SajorIPT101.SajorWPF.ViewModels;

namespace SajorIPT101.SajorWPF.Commands
{
    public class OpenAddEmployeeCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly EmployeesStore _employeesStore;

        public OpenAddEmployeeCommand(ModalNavigationStore modalNavigationStore, EmployeesStore employeesStore)
        {
            _modalNavigationStore = modalNavigationStore;
            _employeesStore = employeesStore;
        }

        public override void Execute(object parameter)
        {
            AddEmployeeViewModel addEmployeeViewModel = new AddEmployeeViewModel(_employeesStore, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = addEmployeeViewModel;
        }
    }
}
