﻿using Drones.Domain.Entities;
using Drones.Infrastructure.Persistences.Contexts;
using Drones.Infrastructure.Persistences.Interfaces;

namespace Drones.Infrastructure.Persistences.Repositories
{
    public class MedicationRepository : GenericRepository<TMedication>, IMedicationRepository
    {
        public MedicationRepository(DronesContext context) : base(context) { }

        #region Estos métodos ya se programan de tipo genéricos, por lo que ya no tienen que estar aqui en la Clase
        /*////////////////////////////////////////////////////////////////////////////////////////////////////
        
        public async Task<IEnumerable<Category>> ListSelectCategories()
        {
            var categories = await _context.Categories
                .Where(w => w.State.Equals((int)StateTypes.Active) && w.AuditDeleteUser == null && w.AuditDeleteDate == null).AsNoTracking().ToListAsync();

            return categories;
        }

        public async Task<Category> GetBategoryById(int id)
        {
            var category = await _context.Categories
                .Where(w => w.CategoryId == id).AsNoTracking().FirstOrDefaultAsync();

            return category!;
        }

        public async Task<bool> RegisterCategory(Category category)
        {
            //Momentaneamente se va a utilizar el User con id = 1...
            //posteriormente se pondrá el que está autenticado
            category.AuditCreateUser = 1;
            //Igual con la fecha del registro
            category.AuditCreateDate = DateTime.Now;

            await _context.AddAsync(category);

            //Devuelve la Cantidad de Filas afectadas
            var rowsAffected = await _context.SaveChangesAsync();

            //En caso que se añada una fila va a devolver 1, que es > 0
            return rowsAffected > 0;
        }

        public async Task<bool> EditCategory(Category category)
        {
            //Momentaneamente se va a utilizar el User con id = 1...
            //posteriormente se pondrá el que está autenticado
            category.AuditUpdateUser = 1;
            //Igual con la fecha del registro
            category.AuditUpdateDate = DateTime.Now;

            _context.Update(category);
            //Las Propiedades de AuditCreateUser y AuditCreateDate van a ser excluidas para no Modificarlas
            //en caso de que por error se modifiquen y mantener la legalidad de los datos
            _context.Entry(category).Property(p => p.AuditCreateUser).IsModified = false;
            _context.Entry(category).Property(p => p.AuditCreateDate).IsModified = false;

            //Devuelve la Cantidad de Filas afectadas
            var rowsAffected = await _context.SaveChangesAsync();

            //En caso que se añada una fila va a devolver 1, que es > 0
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var category = await _context.Categories
                .Where(w => w.CategoryId == id).AsNoTracking().FirstOrDefaultAsync();

            category!.AuditDeleteUser = 1;
            category!.AuditDeleteDate = DateTime.Now;

            _context.Update(category);

            //Devuelve la Cantidad de Filas afectadas
            var rowsAffected = await _context.SaveChangesAsync();

            //En caso que se añada una fila va a devolver 1, que es > 0
            return rowsAffected > 0;

        }*/
        #endregion
    }
}
