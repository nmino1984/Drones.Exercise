using Drones.Domain.Entities;
using Drones.Infrastructure.Persistences.Contexts;
using Drones.Infrastructure.Persistences.Interfaces;
using Drones.Utilities.Statics;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Drones.Infrastructure.Persistences.Repositories
{
    public class DroneMedicationRepository : IDroneMedicationRepository
    {
        private readonly DronesContext _context;
        private readonly DbSet<RDroneMedication> _entity;

        public DroneMedicationRepository(DronesContext context)
        {
            _context = context;
            _entity = _context.Set<RDroneMedication>();
        }

        public async Task<List<RDroneMedication>> GetDroneMedications(int idDrone)
        {
            var listDroneMedications = await _entity.Where(w=>w.IdDrone == idDrone && w.Active == true) .ToListAsync();

            return listDroneMedications;
        }

        public async Task<bool> LoadDroneWithMedicationItems(int idDrone, List<int> listIdMedication)
        {
            var rowsAffected = 0;


            foreach (var item in listIdMedication)
            {
                var droneMedicationRow = new RDroneMedication
                {
                    IdDrone = idDrone,
                    IdMedication = item,
                    DateOpperation = DateTime.Now,
                    Active = true,
                };

                _entity.Add(droneMedicationRow);

                rowsAffected += await _context.SaveChangesAsync();
            }

            return rowsAffected == listIdMedication.Count;
        }

        public async Task<List<int>> CheckLoadedMedicationItemsByDroneGiven(int idDrone)
        {
            List<int> listMedicationsId = new List<int>();

            var listDroneMedications = await _entity.Where(x => x.IdDrone == idDrone && x.Active == true).AsNoTracking().ToListAsync();

            foreach (var item in listDroneMedications)
            {
                listMedicationsId.Add(item.IdMedication);
            }

            return listMedicationsId;

        }

        public async Task<bool> EditAsync(RDroneMedication entity)
        {

            _context.Update(entity);

            var rowsAffected = await _context.SaveChangesAsync();

            return rowsAffected > 0;
        }

    }
}
