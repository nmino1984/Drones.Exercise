using Drones.Domain.Entities;
using Drones.Infrastructure.Persistences.Contexts;
using Drones.Infrastructure.Persistences.Interfaces;
using Drones.Utilities.Statics;
using Microsoft.EntityFrameworkCore;

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

        public Task<bool> RegisterDrone(TDrone drone)
        {
            throw new NotImplementedException();
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
                };

                _entity.Add(droneMedicationRow);

                rowsAffected += await _context.SaveChangesAsync();
            }

            return rowsAffected == listIdMedication.Count;
        }

        public async Task<List<int>> CheckLoadedMedicationItemsByDroneGiven(int idDrone)
        {
            List<int> listMedicationsId = new List<int>();

            var listDroneMedications = await _entity.Where(x => x.IdDrone == idDrone).AsNoTracking().ToListAsync();

            foreach (var item in listDroneMedications)
            {
                listMedicationsId.Add(item.IdMedication);
            }

            return listMedicationsId;

        }
    }
}
