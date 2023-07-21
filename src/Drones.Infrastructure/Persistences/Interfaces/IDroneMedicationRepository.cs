using Drones.Domain.Entities;
using Drones.Infrastructure.Persistences.Interfaces;

namespace Drones.Infrastructure.Persistences.Interfaces
{
    public interface IDroneMedicationRepository : IGenericRepository<RDroneMedication>
    {
        Task<bool> LoadDroneWithMedicationItems(int idDrone, List<int> idMedication);
        Task<List<TMedication>> CheckMedicationByDroneGiven(int idDrone);
        Task<List<TDrone>> CheckListAvailableDrones();
        Task<int> CheckDroneBatteryGivenDrone();
    }
}
