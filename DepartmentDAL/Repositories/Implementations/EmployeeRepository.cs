using DepartmentCore.Models;
using DepartmentDAL.Contexts;
using DepartmentDAL.Repositories.Abstractions;
using DepartmentDAL.Repositories.Iplementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentDAL.Repositories.Implementations
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDBContext context) : base(context)
        {
        }
    }
}
