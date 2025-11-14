using System.Collections.ObjectModel;
using SajorWPF.Models;
using SajorWPF.Repositories;

namespace SajorWPF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IPersonRepository _personRepository;
        private ObservableCollection<Person> _people;

        public MainViewModel(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
            _people = new ObservableCollection<Person>();
            _ = LoadPeopleAsync();
        }

        public ObservableCollection<Person> People
        {
            get => _people;
            set => SetProperty(ref _people, value);
        }

        private async Task LoadPeopleAsync()
        {
            try
            {
                var people = await _personRepository.GetAllAsync();
                People.Clear();
                foreach (var person in people)
                {
                    People.Add(person);
                }
            }
            catch (Exception ex)
            {
                // In a real application, handle the exception appropriately
                System.Diagnostics.Debug.WriteLine($"Error loading people: {ex.Message}");
            }
        }
    }
}
