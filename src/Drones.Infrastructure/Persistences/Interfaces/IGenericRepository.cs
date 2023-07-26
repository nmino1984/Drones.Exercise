using Drones.Domain.Entities;
using Drones.Infrastructure.Commons.Bases.Response;
using System.Linq.Expressions;

namespace Drones.Infrastructure.Persistences.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<BaseEntityResponse<T>> GetAllAsyncAsResponse();
        Task<T> GetByIdAsync(int id);
        Task<bool> RegisteAsync(T entity);
        Task<bool> EditAsync(T entity);
        Task<bool> DeleteAsync(int id);
        IQueryable<T> GetEntityQuery(Expression<Func<T, bool>>? filter = null);
    }
}
