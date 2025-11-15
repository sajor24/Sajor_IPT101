using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using SajorWPF.Data;
using SajorWPF.Helpers;
using SajorWPF.Models;

namespace SajorWPF.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly EmployeeContext _context;
        private Employee? _selectedEmployee;
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private string _age = string.Empty;
        private string _position = string.Empty;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Employee> Employees { get; set; }

        public Employee? SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged();
                if (_selectedEmployee != null)
                {
                    FirstName = _selectedEmployee.FirstName;
                    LastName = _selectedEmployee.LastName;
                    Age = _selectedEmployee.Age.ToString();
                    Position = _selectedEmployee.Position;
                }
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public string Age
        {
            get => _age;
            set
            {
                _age = value;
                OnPropertyChanged();
            }
        }

        public string Position
        {
            get => _position;
            set
            {
                _position = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ClearCommand { get; }

        public MainViewModel(EmployeeContext context)
        {
            _context = context;
            Employees = new ObservableCollection<Employee>();

            AddCommand = new RelayCommand(Add);
            UpdateCommand = new RelayCommand(Update, CanUpdate);
            DeleteCommand = new RelayCommand(Delete, CanDelete);
            ClearCommand = new RelayCommand(Clear);

            LoadEmployees();
        }

        private void LoadEmployees()
        {
            _context.Database.EnsureCreated();
            var employees = _context.Employees.ToList();
            Employees.Clear();
            foreach (var employee in employees)
            {
                Employees.Add(employee);
            }
        }

        private void Add(object? parameter)
        {
            if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
                return;

            if (!int.TryParse(Age, out int ageValue))
                return;

            var employee = new Employee
            {
                FirstName = FirstName,
                LastName = LastName,
                Age = ageValue,
                Position = Position
            };

            _context.Employees.Add(employee);
            _context.SaveChanges();

            Employees.Add(employee);
            Clear(null);
        }

        private void Update(object? parameter)
        {
            if (SelectedEmployee == null)
                return;

            if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
                return;

            if (!int.TryParse(Age, out int ageValue))
                return;

            SelectedEmployee.FirstName = FirstName;
            SelectedEmployee.LastName = LastName;
            SelectedEmployee.Age = ageValue;
            SelectedEmployee.Position = Position;

            _context.SaveChanges();

            // Refresh the collection
            var index = Employees.IndexOf(SelectedEmployee);
            if (index >= 0)
            {
                Employees[index] = SelectedEmployee;
            }

            Clear(null);
        }

        private bool CanUpdate(object? parameter)
        {
            return SelectedEmployee != null;
        }

        private void Delete(object? parameter)
        {
            if (SelectedEmployee == null)
                return;

            _context.Employees.Remove(SelectedEmployee);
            _context.SaveChanges();

            Employees.Remove(SelectedEmployee);
            Clear(null);
        }

        private bool CanDelete(object? parameter)
        {
            return SelectedEmployee != null;
        }

        private void Clear(object? parameter)
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Age = string.Empty;
            Position = string.Empty;
            SelectedEmployee = null;
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
