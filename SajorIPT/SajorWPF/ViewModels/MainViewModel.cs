using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
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
        private int _age;
        private string _position = string.Empty;

        public MainViewModel(EmployeeContext context)
        {
            _context = context;
            Employees = new ObservableCollection<Employee>();
            
            AddCommand = new RelayCommand(_ => AddEmployee());
            UpdateCommand = new RelayCommand(_ => UpdateEmployee(), _ => SelectedEmployee != null);
            DeleteCommand = new RelayCommand(_ => DeleteEmployee(), _ => SelectedEmployee != null);
            ClearCommand = new RelayCommand(_ => ClearFields());

            LoadEmployees();
        }

        public ObservableCollection<Employee> Employees { get; }

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
                    Age = _selectedEmployee.Age;
                    Position = _selectedEmployee.Position;
                }
            }
        }

        public string FirstName
        {
            get => _firstName;
            set { _firstName = value; OnPropertyChanged(); }
        }

        public string LastName
        {
            get => _lastName;
            set { _lastName = value; OnPropertyChanged(); }
        }

        public int Age
        {
            get => _age;
            set { _age = value; OnPropertyChanged(); }
        }

        public string Position
        {
            get => _position;
            set { _position = value; OnPropertyChanged(); }
        }

        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ClearCommand { get; }

        private void LoadEmployees()
        {
            try
            {
                Employees.Clear();
                var employees = _context.Employees.ToList();
                foreach (var employee in employees)
                {
                    Employees.Add(employee);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading employees: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddEmployee()
        {
            try
            {
                var employee = new Employee
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Age = Age,
                    Position = Position
                };

                _context.Employees.Add(employee);
                _context.SaveChanges();
                Employees.Add(employee);
                ClearFields();
                MessageBox.Show("Employee added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding employee: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateEmployee()
        {
            if (SelectedEmployee == null) return;

            try
            {
                SelectedEmployee.FirstName = FirstName;
                SelectedEmployee.LastName = LastName;
                SelectedEmployee.Age = Age;
                SelectedEmployee.Position = Position;

                _context.SaveChanges();
                LoadEmployees();
                ClearFields();
                MessageBox.Show("Employee updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating employee: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteEmployee()
        {
            if (SelectedEmployee == null) return;

            try
            {
                var result = MessageBox.Show($"Are you sure you want to delete {SelectedEmployee.FirstName} {SelectedEmployee.LastName}?",
                    "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _context.Employees.Remove(SelectedEmployee);
                    _context.SaveChanges();
                    LoadEmployees();
                    ClearFields();
                    MessageBox.Show("Employee deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting employee: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearFields()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Age = 0;
            Position = string.Empty;
            SelectedEmployee = null;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
