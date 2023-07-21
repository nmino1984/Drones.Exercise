namespace Drones.Infrastructure.Persistences.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IMedicationRepository Medication { get; }
        IDroneRepository Drone { get; }
        IDroneMedicationRepository DroneMedication { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
