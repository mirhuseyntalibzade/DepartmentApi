using DepartmentCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentDAL.Repositories.Abstractions
{
    public interface IGenericRepository<T> where T : BaseEntity, new()
    {
        Task Create(T entity);
        Task<ICollection<T>> GetAllAsync();
        Task<T> FindByIdAsync(int Id);
        Task<ICollection<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> FindOneAsync(Expression<Func<T, bool>> predicate);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync();
    }
}
