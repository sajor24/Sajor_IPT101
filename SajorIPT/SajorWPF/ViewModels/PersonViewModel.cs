using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SajorWPF.Models;
using SajorWPF.Repositories;

namespace SajorWPF.ViewModels
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        private readonly IPersonRepository _personRepository;
        private ObservableCollection<Person> _people;
        private Person? _selectedPerson;

        public PersonViewModel(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
            _people = new ObservableCollection<Person>();
        }

        public ObservableCollection<Person> People
        {
            get => _people;
            set
            {
                _people = value;
                OnPropertyChanged();
            }
        }

        public Person? SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadPeopleAsync()
        {
            var people = await _personRepository.GetAllAsync();
            People.Clear();
            foreach (var person in people)
            {
                People.Add(person);
            }
        }

        public async Task AddPersonAsync(Person person)
        {
            person.CreatedAt = DateTime.Now;
            await _personRepository.AddAsync(person);
            await LoadPeopleAsync();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
