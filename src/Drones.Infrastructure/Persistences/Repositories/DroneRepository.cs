using Drones.Domain.Entities;
using Drones.Infrastructure.Persistences.Contexts;
using Drones.Infrastructure.Persistences.Interfaces;
using Drones.Utilities.Statics;
using Microsoft.EntityFrameworkCore;

namespace Drones.Infrastructure.Persistences.Repositories
{
    public class DroneRepository : GenericRepository<TDrone>, IDroneRepository
    {
        public DroneRepository(DronesContext context) : base(context) { }

        public async Task<bool> IsPossibleToAddADrone()
        {
            var drones = await GetAllAsync();

            if (drones.Count() < 10)
            {
                return true;
            }
            return false;
        }

        public async Task<TDrone> GetDroneBySerialNumber(string serialNumber)
        {
            var drone = await GetEntityQuery(x => x.SerialNumber == serialNumber).FirstOrDefaultAsync();

            return drone!;
        }

        public async Task<bool> GetIfDroneAvailable(int droneId)
        {
            var drone = await GetByIdAsync(droneId);

            if (drone.State == (int)StateTypes.IDLE)
            {
                return true;
            }
            return false;
        }

        public async Task<double> GetDroneBattery(int droneId)
        {
            var drone = await GetByIdAsync(droneId);

            return drone.BatteryCapacity;
        }

        public async Task<bool> ChangeStateToDrone(int droneId, StateTypes newState)
        {
            var drone = await GetByIdAsync(droneId);
            drone.State = (int)newState;
            
            return await EditAsync(drone);
        }

        public async Task<double> GetDroneWeightLimit(int droneId)
        {
            var drone = await GetByIdAsync(droneId);

            return drone.WeightLimit;
        }

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
