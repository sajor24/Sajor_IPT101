using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SajorIPT101Solution.SajorDomain.Models;

namespace SajorIPT101.SajorWPF.ViewModels
{
    public class EmployeesListingItemViewModel : ViewModelBase
    {
        public Employee Employee { get; private set; }

        public string FirstName => Employee.FirstName;
        public string LastName => Employee.LastName;
        public int Age => Employee.Age;
        public string Position => Employee.Position;

        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public EmployeesListingItemViewModel(Employee employee, ICommand editCommand, ICommand deleteCommand)
        {
            Employee = employee;
            EditCommand = editCommand;
            DeleteCommand = deleteCommand;
        }

        public void Update(Employee employee)
        {
            Employee = employee;

            OnPropertyChanged(nameof(FirstName));
            OnPropertyChanged(nameof(LastName));
            OnPropertyChanged(nameof(Age));
            OnPropertyChanged(nameof(Position));
        }
    }
}
