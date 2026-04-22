using Drones.Domain.Entities;
using Drones.Infrastructure.Persistences.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Drones.Infrastructure.Services;

/// <summary>
/// Background service that checks battery levels of all drones every 60 seconds
/// and writes an audit entry to the BatteryLog table.
/// </summary>
public class BatteryMonitorService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<BatteryMonitorService> _logger;
    private static readonly TimeSpan _interval = TimeSpan.FromSeconds(60);

    public BatteryMonitorService(IServiceScopeFactory scopeFactory, ILogger<BatteryMonitorService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("BatteryMonitorService started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            await CheckBatteriesAsync(stoppingToken);
            await Task.Delay(_interval, stoppingToken);
        }

        _logger.LogInformation("BatteryMonitorService stopped.");
    }

    private async Task CheckBatteriesAsync(CancellationToken cancellationToken)
    {
        try
        {
            // DronesContext is Transient — resolve a fresh scope per tick
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DronesContext>();

            var drones = await context.TDrones.AsNoTracking().ToListAsync(cancellationToken);

            var logs = drones.Select(d => new BatteryLog
            {
                DroneId     = d.Id,
                SerialNumber = d.SerialNumber ?? string.Empty,
                BatteryLevel = d.BatteryLevel,
                CheckedAt   = DateTime.UtcNow
            }).ToList();

            await context.BatteryLogs.AddRangeAsync(logs, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Battery check completed: {Count} drone(s) logged at {Time}.",
                logs.Count, DateTime.UtcNow);
        }
        catch (OperationCanceledException)
        {
            // Graceful shutdown — not an error
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Battery check failed.");
        }
    }
}
