using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SajorIPT101Solution.SajorDomain.Models;

namespace SajorIPT101Solution.SajorDomain.Queries
{
    public interface IGetAllEmployeesQuery
    {
        Task<IEnumerable<Employee>> Execute();
    }
}
