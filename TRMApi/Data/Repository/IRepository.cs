using System.Collections.Generic;
using System.Threading.Tasks;

namespace TRMApi.Data.Repository
{
    public interface IRepository<T, K> where T : class
    {
        Task<T> GetByIdAsync (K id);
        List<T> GetAll();
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}