using Drones.Domain.Entities;
using Drones.Infrastructure.Commons.Bases.Response;
using Drones.Infrastructure.Persistences.Contexts;
using Drones.Infrastructure.Persistences.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Drones.Infrastructure.Persistences.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly DronesContext _context;
        private readonly DbSet<T> _entity;

        public GenericRepository(DronesContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var getAll = await _entity.AsNoTracking().ToListAsync();

            return getAll;
        }

        public async Task<BaseEntityResponse<T>> GetAllAsyncAsResponse()
        {
            var response = new BaseEntityResponse<T>();

            var getAll = await _entity.AsNoTracking().ToListAsync();

            response.TotalRecords = getAll.Count;
            response.Items = getAll;

            return response;

        }

        public async Task<T> GetByIdAsync(int id)
        {
            var getById = await _entity.AsNoTracking().Where(w => w.Id == id).FirstOrDefaultAsync();

            return getById!;
        }

        public async Task<bool> RegisteAsync(T entity)
        {
            await _context.AddAsync(entity);

            var rowsAffected = await _context.SaveChangesAsync();

            return rowsAffected > 0;
        }

        public async Task<bool> EditAsync(T entity)
        {
            _context.Update(entity);

            var rowsAffected = await _context.SaveChangesAsync();

            return rowsAffected > 0;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            T entity = await GetByIdAsync(id);

            _context.Remove(entity);

            var rowsAffected = await _context.SaveChangesAsync();

            return rowsAffected > 0;
        }

        public IQueryable<T> GetEntityQuery(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _entity;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }
    }
}
