using Drones.Domain.Entities;
using Drones.Infrastructure.Persistences.Interfaces;

namespace Drones.Infrastructure.Persistences.Interfaces
{
    public interface IDroneRepository : IGenericRepository<TDrone>
    {

    }
}
