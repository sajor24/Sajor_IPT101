using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SajorIPT101Solution.SajorDomain.Models;
using SajorIPT101Solution.SajorDomain.Commands;
using SajorIPT101Solution.SajorDomain.Queries;

namespace SajorIPT101.SajorWPF.Stores
{
    public class EmployeesStore
    {
        private readonly IGetAllEmployeesQuery _getAllEmployeesQuery;
        private readonly ICreateEmployeeCommand _createEmployeeCommand;
        private readonly IUpdateEmployeeCommand _updateEmployeeCommand;
        private readonly IDeleteEmployeeCommand _deleteEmployeeCommand;

        private readonly Lazy<Task> _initializeLazy;

        private List<Employee> _employees;
        public IEnumerable<Employee> Employees => _employees;

        public event Action EmployeesLoaded;
        public event Action<Employee> EmployeeAdded;
        public event Action<Employee> EmployeeUpdated;
        public event Action<Guid> EmployeeDeleted;

        public EmployeesStore(IGetAllEmployeesQuery getAllEmployeesQuery,
            ICreateEmployeeCommand createEmployeeCommand,
            IUpdateEmployeeCommand updateEmployeeCommand,
            IDeleteEmployeeCommand deleteEmployeeCommand)
        {
            _getAllEmployeesQuery = getAllEmployeesQuery;
            _createEmployeeCommand = createEmployeeCommand;
            _updateEmployeeCommand = updateEmployeeCommand;
            _deleteEmployeeCommand = deleteEmployeeCommand;

            _initializeLazy = new Lazy<Task>(Initialize);
            _employees = new List<Employee>();
        }

        public async Task Load()
        {
            await _initializeLazy.Value;
        }

        public async Task Add(Employee employee)
        {
            await _createEmployeeCommand.Execute(employee);

            _employees.Add(employee);

            EmployeeAdded?.Invoke(employee);
        }

        public async Task Update(Employee employee)
        {
            await _updateEmployeeCommand.Execute(employee);

            int currentIndex = _employees.FindIndex(e => e.Id == employee.Id);

            if (currentIndex != -1)
            {
                _employees[currentIndex] = employee;
            }
            else
            {
                _employees.Add(employee);
            }

            EmployeeUpdated?.Invoke(employee);
        }

        public async Task Delete(Guid id)
        {
            await _deleteEmployeeCommand.Execute(id);

            _employees.RemoveAll(e => e.Id == id);

            EmployeeDeleted?.Invoke(id);
        }

        private async Task Initialize()
        {
            IEnumerable<Employee> employees = await _getAllEmployeesQuery.Execute();

            _employees.Clear();
            _employees.AddRange(employees);

            EmployeesLoaded?.Invoke();
        }
    }
}
