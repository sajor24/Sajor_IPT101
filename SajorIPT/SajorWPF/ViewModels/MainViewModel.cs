using System.Collections.ObjectModel;
using System.Threading.Tasks;
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
        }

        public ObservableCollection<Person> People
        {
            get => _people;
            set => SetProperty(ref _people, value);
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
    }
}
