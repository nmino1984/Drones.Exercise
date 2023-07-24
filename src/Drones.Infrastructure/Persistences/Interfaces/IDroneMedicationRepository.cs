using Drones.Domain.Entities;
using Drones.Infrastructure.Persistences.Interfaces;

namespace Drones.Infrastructure.Persistences.Interfaces
{
    public interface IDroneMedicationRepository
    {
        Task<bool> LoadDroneWithMedicationItems(int idDrone, List<int> idMedication);
        Task<List<int>> CheckLoadedMedicationItemsByDroneGiven(int idDrone);
    }
}
