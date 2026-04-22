using Drones.Domain.Entities;
using Drones.Infrastructure.Persistences.Contexts;
using Drones.Infrastructure.Persistences.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Drones.Infrastructure.Persistences.Repositories;

public class BatteryLogRepository : IBatteryLogRepository
{
    private readonly DronesContext _context;

    public BatteryLogRepository(DronesContext context)
    {
        _context = context;
    }

    public async Task<List<BatteryLog>> GetAllAsync() =>
        await _context.BatteryLogs.AsNoTracking().OrderByDescending(b => b.CheckedAt).ToListAsync();

    public async Task<List<BatteryLog>> GetByDroneIdAsync(int droneId) =>
        await _context.BatteryLogs.AsNoTracking()
            .Where(b => b.DroneId == droneId)
            .OrderByDescending(b => b.CheckedAt)
            .ToListAsync();
}
