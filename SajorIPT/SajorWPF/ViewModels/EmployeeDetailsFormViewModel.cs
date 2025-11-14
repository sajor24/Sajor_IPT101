using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SajorIPT101.SajorWPF.ViewModels
{
    public class EmployeeDetailsFormViewModel : ViewModelBase
    {
        public EmployeeDetailsViewModel EmployeeDetails { get; }

        public ICommand SubmitCommand { get; set; }
        public ICommand CancelCommand { get; }

        public EmployeeDetailsFormViewModel(ICommand submitCommand, ICommand cancelCommand)
        {
            EmployeeDetails = new EmployeeDetailsViewModel();
            SubmitCommand = submitCommand;
            CancelCommand = cancelCommand;
        }
    }
}
