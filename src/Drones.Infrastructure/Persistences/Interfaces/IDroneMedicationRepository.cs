using Drones.Domain.Entities;
using Drones.Infrastructure.Persistences.Interfaces;
using Drones.Infrastructure.Persistences.Repositories;

namespace Drones.Infrastructure.Persistences.Interfaces
{
    public interface IDroneMedicationRepository 
    {
        Task<List<RDroneMedication>> GetDroneMedications(int idDrone);
        Task<bool> LoadDroneWithMedicationItems(int idDrone, List<int> idMedication);
        Task<List<int>> CheckLoadedMedicationItemsByDroneGiven(int idDrone);

        Task<bool> EditAsync(RDroneMedication entity);
    }
}
