using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentCore.Models
{
    public class Department : BaseAuditableEntity
    {
        public string Name { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}
