using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SajorIPT101Solution.SajorDomain.Models;

namespace SajorIPT101Solution.SajorDomain.Commands
{
    public interface IUpdateEmployeeCommand
    {
        Task Execute(Employee employee);
    }
}
