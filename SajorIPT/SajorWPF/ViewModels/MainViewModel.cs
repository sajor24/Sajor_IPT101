using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using SajorWPF.Models;
using SajorWPF.Repositories;

namespace SajorWPF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IPersonRepository _personRepository;

        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private int _age;
        private string _position = string.Empty;

        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        public int Age
        {
            get => _age;
            set => SetProperty(ref _age, value);
        }

        public string Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
        }

        public ObservableCollection<Person> Persons { get; set; }

        public ICommand AddPersonCommand { get; }
        public ICommand LoadPersonsCommand { get; }

        public MainViewModel(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
            Persons = new ObservableCollection<Person>();

            AddPersonCommand = new RelayCommand(async () => await AddPerson());
            LoadPersonsCommand = new RelayCommand(async () => await LoadPersons());

            // Load persons on startup
            _ = LoadPersons();
        }

        private async Task AddPerson()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
                {
                    MessageBox.Show("First Name and Last Name are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var person = new Person
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Age = Age,
                    Position = Position
                };

                await _personRepository.AddAsync(person);
                await LoadPersons();

                // Clear form
                FirstName = string.Empty;
                LastName = string.Empty;
                Age = 0;
                Position = string.Empty;

                MessageBox.Show("Person added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding person: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task LoadPersons()
        {
            try
            {
                var persons = await _personRepository.GetAllAsync();
                Persons.Clear();
                foreach (var person in persons)
                {
                    Persons.Add(person);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading persons: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    // Simple RelayCommand implementation for MVVM
    public class RelayCommand : ICommand
    {
        private readonly Func<Task> _execute;
        private readonly Func<bool>? _canExecute;

        public RelayCommand(Func<Task> execute, Func<bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public async void Execute(object? parameter)
        {
            await _execute();
        }
    }
}
