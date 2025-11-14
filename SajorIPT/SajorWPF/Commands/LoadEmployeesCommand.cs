using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SajorIPT101.SajorWPF.Stores;

namespace SajorIPT101.SajorWPF.Commands
{
    public class LoadEmployeesCommand : AsyncCommandBase
    {
        private readonly EmployeesStore _employeesStore;

        public LoadEmployeesCommand(EmployeesStore employeesStore)
        {
            _employeesStore = employeesStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            await _employeesStore.Load();
        }
    }
}
