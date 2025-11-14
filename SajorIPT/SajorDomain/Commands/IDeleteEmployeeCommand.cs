using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SajorIPT101Solution.SajorDomain.Commands
{
    public interface IDeleteEmployeeCommand
    {
        Task Execute(Guid id);
    }
}
