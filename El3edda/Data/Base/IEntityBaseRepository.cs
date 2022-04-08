using System.Linq.Expressions;

namespace El3edda.Data.Base
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        Task<IEnumerable<T>> GetAllAsync();
        
        //Overload to implement Load() for navigational properties
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
        
      
        Task<T> GetByIdAsync(int id);


        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includeProperties);


        Task AddAsync(T entity);
        Task UpdateAsync(int id, T newEntity);
        Task DeleteAsync(int id);
    }
}
