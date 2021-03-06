using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExcerciseApp.API.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<string> includes = null);
        
        Task<T> Get(Expression<Func<T, bool>> expression, IList<string> includes = null);
        Task InsertAsync(T entity);
        Task InsertRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        Task DeleteAsync(int id);
        void DeleteRange(IEnumerable<T> entities);
    }
}
