using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SajorIPT101.SajorWPF.Commands;
using SajorIPT101.SajorWPF.Stores;
using SajorIPT101Solution.SajorDomain.Models;

namespace SajorIPT101.SajorWPF.ViewModels
{
    public class EmployeesListingViewModel : ViewModelBase
    {
        private readonly EmployeesStore _employeesStore;
        private readonly SelectedEmployeeStore _selectedEmployeeStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        private readonly ObservableCollection<EmployeesListingItemViewModel> _employeesListingItemViewModels;
        public IEnumerable<EmployeesListingItemViewModel> EmployeesListingItemViewModels => _employeesListingItemViewModels;

        public EmployeesListingViewModel(EmployeesStore employeesStore, 
            SelectedEmployeeStore selectedEmployeeStore,
            ModalNavigationStore modalNavigationStore)
        {
            _employeesStore = employeesStore;
            _selectedEmployeeStore = selectedEmployeeStore;
            _modalNavigationStore = modalNavigationStore;

            _employeesListingItemViewModels = new ObservableCollection<EmployeesListingItemViewModel>();

            _employeesStore.EmployeesLoaded += EmployeesStore_EmployeesLoaded;
            _employeesStore.EmployeeAdded += EmployeesStore_EmployeeAdded;
            _employeesStore.EmployeeUpdated += EmployeesStore_EmployeeUpdated;
            _employeesStore.EmployeeDeleted += EmployeesStore_EmployeeDeleted;
        }

        protected override void Dispose()
        {
            _employeesStore.EmployeesLoaded -= EmployeesStore_EmployeesLoaded;
            _employeesStore.EmployeeAdded -= EmployeesStore_EmployeeAdded;
            _employeesStore.EmployeeUpdated -= EmployeesStore_EmployeeUpdated;
            _employeesStore.EmployeeDeleted -= EmployeesStore_EmployeeDeleted;

            base.Dispose();
        }

        private void EmployeesStore_EmployeesLoaded()
        {
            _employeesListingItemViewModels.Clear();

            foreach (Employee employee in _employeesStore.Employees)
            {
                AddEmployee(employee);
            }
        }

        private void EmployeesStore_EmployeeAdded(Employee employee)
        {
            AddEmployee(employee);
        }

        private void EmployeesStore_EmployeeUpdated(Employee employee)
        {
            EmployeesListingItemViewModel employeeViewModel =
                _employeesListingItemViewModels.FirstOrDefault(e => e.Employee.Id == employee.Id);

            if (employeeViewModel != null)
            {
                employeeViewModel.Update(employee);
            }
        }

        private void EmployeesStore_EmployeeDeleted(Guid id)
        {
            EmployeesListingItemViewModel itemViewModel = _employeesListingItemViewModels
                .FirstOrDefault(e => e.Employee.Id == id);

            if (itemViewModel != null)
            {
                _employeesListingItemViewModels.Remove(itemViewModel);
            }
        }

        private void AddEmployee(Employee employee)
        {
            ICommand editCommand = new OpenEditEmployeeCommand(_modalNavigationStore, _selectedEmployeeStore, _employeesStore);
            ICommand deleteCommand = new DeleteEmployeeCommand(_employeesStore);

            EmployeesListingItemViewModel itemViewModel = 
                new EmployeesListingItemViewModel(employee, editCommand, deleteCommand);
            _employeesListingItemViewModels.Add(itemViewModel);
        }
    }
}
