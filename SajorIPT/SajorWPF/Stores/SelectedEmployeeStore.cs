using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SajorIPT101Solution.SajorDomain.Models;

namespace SajorIPT101.SajorWPF.Stores
{
    public class SelectedEmployeeStore
    {
        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get
            {
                return _selectedEmployee;
            }
            set
            {
                _selectedEmployee = value;
                SelectedEmployeeChanged?.Invoke();
            }
        }

        public event Action SelectedEmployeeChanged;
    }
}
