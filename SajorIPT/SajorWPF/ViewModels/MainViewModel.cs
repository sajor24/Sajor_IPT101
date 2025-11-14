using System.Collections.ObjectModel;
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
        private Person? _selectedPerson;

        public MainViewModel(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
            People = new ObservableCollection<Person>();
            AddPersonCommand = new RelayCommand(async _ => await AddPersonAsync(), _ => CanAddPerson());
            LoadPeopleCommand = new RelayCommand(async _ => await LoadPeopleAsync());
            
            // Load people on initialization
            _ = LoadPeopleAsync();
        }

        public ObservableCollection<Person> People { get; }

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

        public Person? SelectedPerson
        {
            get => _selectedPerson;
            set => SetProperty(ref _selectedPerson, value);
        }

        public ICommand AddPersonCommand { get; }
        public ICommand LoadPeopleCommand { get; }

        private bool CanAddPerson()
        {
            return !string.IsNullOrWhiteSpace(FirstName) && 
                   !string.IsNullOrWhiteSpace(LastName) && 
                   Age > 0;
        }

        private async Task AddPersonAsync()
        {
            var person = new Person
            {
                FirstName = FirstName,
                LastName = LastName,
                Age = Age,
                Position = Position
            };

            await _personRepository.AddAsync(person);
            await LoadPeopleAsync();

            // Clear form
            FirstName = string.Empty;
            LastName = string.Empty;
            Age = 0;
            Position = string.Empty;
        }

        private async Task LoadPeopleAsync()
        {
            var people = await _personRepository.GetAllAsync();
            People.Clear();
            foreach (var person in people)
            {
                People.Add(person);
            }
        }
    }

    // Simple RelayCommand implementation
    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Func<object?, bool>? _canExecute;

        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
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
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);
        }
    }
}
