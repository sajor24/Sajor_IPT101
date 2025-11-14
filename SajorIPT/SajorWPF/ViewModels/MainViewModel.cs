using System.Collections.ObjectModel;
using SajorWPF.Models;
using SajorWPF.Repositories;

namespace SajorWPF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IPersonRepository _personRepository;
        private ObservableCollection<Person> _people;

        public ObservableCollection<Person> People
        {
            get => _people;
            set => SetProperty(ref _people, value);
        }

        public MainViewModel(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
            _people = new ObservableCollection<Person>();
            LoadPeopleAsync();
        }

        private async void LoadPeopleAsync()
        {
            try
            {
                var people = await _personRepository.GetAllAsync();
                People = new ObservableCollection<Person>(people);
            }
            catch
            {
                // In a real application, you would handle this error properly
                // For now, we'll just ensure the collection is initialized
                People = new ObservableCollection<Person>();
            }
        }
    }
}
