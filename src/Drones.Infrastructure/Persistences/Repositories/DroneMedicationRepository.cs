using Drones.Domain.Entities;
using Drones.Infrastructure.Persistences.Contexts;
using Drones.Infrastructure.Persistences.Interfaces;

namespace Drones.Infrastructure.Persistences.Repositories
{
    public class DroneMedicationRepository : GenericRepository<RDroneMedication>, IDroneMedicationRepository
    {
        public DroneMedicationRepository(DronesContext context) : base(context) { }

        public Task<bool> RegisterDrone(TDrone drone)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LoadDroneWithMedicationItems(int idDrone, List<int> idMedication)
        {
            throw new NotImplementedException();
        }

        public Task<List<TMedication>> CheckMedicationByDroneGiven(int idDrone)
        {
            throw new NotImplementedException();
        }

        public Task<List<TDrone>> CheckListAvailableDrones()
        {
            throw new NotImplementedException();
        }

        public Task<int> CheckDroneBatteryGivenDrone()
        {
            throw new NotImplementedException();
        }
    }
}
