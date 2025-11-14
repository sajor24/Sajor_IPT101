using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SajorIPT101Solution.SajorDomain.Models
{
    public class Employee
    {
        public Guid Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public int Age { get; }
        public string Position { get; }

        public Employee(Guid id, string firstName, string lastName, int age, string position)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Position = position;
        }
    }
}
