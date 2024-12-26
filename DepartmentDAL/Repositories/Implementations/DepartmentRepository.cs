using DepartmentCore.Models;
using DepartmentDAL.Contexts;
using DepartmentDAL.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentDAL.Repositories.Iplementations
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(AppDBContext context) : base(context)
        {
        }
    }
}
