using Drones.Infrastructure.Persistences.Contexts;
using Drones.Infrastructure.Persistences.Interfaces;

namespace Drones.Infrastructure.Persistences.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DronesContext _context;
        public IMedicationRepository Medication { get; private set; }

        public IDroneRepository Drone { get; private set; }

        public IDroneMedicationRepository DroneMedication { get; private set; }

        public UnitOfWork(DronesContext context)
        {
            _context = context;
            Medication = new MedicationRepository(_context);
            Drone = new DroneRepository(_context);
            DroneMedication = new DroneMedicationRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
