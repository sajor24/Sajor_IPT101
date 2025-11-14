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
    public class EmployeesViewModel : ViewModelBase
    {
        public EmployeesListingViewModel EmployeesListingViewModel { get; }

        public ICommand AddEmployeeCommand { get; }
        public ICommand LoadEmployeesCommand { get; }

        public EmployeesViewModel(EmployeesStore employeesStore,
            SelectedEmployeeStore selectedEmployeeStore,
            ModalNavigationStore modalNavigationStore)
        {
            EmployeesListingViewModel = new EmployeesListingViewModel(employeesStore, selectedEmployeeStore, modalNavigationStore);

            AddEmployeeCommand = new OpenAddEmployeeCommand(modalNavigationStore, employeesStore);
            LoadEmployeesCommand = new LoadEmployeesCommand(employeesStore);
        }

        public static EmployeesViewModel LoadViewModel(EmployeesStore employeesStore,
            SelectedEmployeeStore selectedEmployeeStore,
            ModalNavigationStore modalNavigationStore)
        {
            EmployeesViewModel viewModel = new EmployeesViewModel(employeesStore, selectedEmployeeStore, modalNavigationStore);

            viewModel.LoadEmployeesCommand.Execute(null);

            return viewModel;
        }
    }
}
