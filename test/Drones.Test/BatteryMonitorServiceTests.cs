using Drones.Domain.Entities;
using Drones.Infrastructure.Persistences.Contexts;
using Drones.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Drones.Test;

public class BatteryMonitorServiceTests
{
    private static DronesContext BuildInMemoryContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<DronesContext>()
            .UseInMemoryDatabase(dbName)
            .Options;
        return new DronesContext(options);
    }

    private static BatteryMonitorService BuildService(DronesContext context)
    {
        var scopeMock = new Mock<IServiceScope>();
        var providerMock = new Mock<IServiceProvider>();
        var scopeFactoryMock = new Mock<IServiceScopeFactory>();
        var loggerMock = new Mock<ILogger<BatteryMonitorService>>();

        providerMock
            .Setup(p => p.GetService(typeof(DronesContext)))
            .Returns(context);

        scopeMock.Setup(s => s.ServiceProvider).Returns(providerMock.Object);
        scopeFactoryMock.Setup(f => f.CreateScope()).Returns(scopeMock.Object);

        return new BatteryMonitorService(scopeFactoryMock.Object, loggerMock.Object);
    }

    [Fact]
    public async Task BatteryMonitorService_WhenDronesExist_InsertsOneLogPerDrone()
    {
        var dbName = Guid.NewGuid().ToString();
        await using var ctx = BuildInMemoryContext(dbName);
        ctx.TDrones.AddRange(
            new TDrone { SerialNumber = "DRN-001", BatteryLevel = 80, Model = 1, WeightLimit = 300, BatteryCapacity = 100, State = 0 },
            new TDrone { SerialNumber = "DRN-002", BatteryLevel = 40, Model = 2, WeightLimit = 200, BatteryCapacity = 100, State = 0 }
        );
        await ctx.SaveChangesAsync();

        var service = BuildService(ctx);
        await service.CheckBatteriesAsync(CancellationToken.None);

        var logs = await ctx.BatteryLogs.ToListAsync();
        Assert.Equal(2, logs.Count);
    }

    [Fact]
    public async Task BatteryMonitorService_LogsContainCorrectBatteryLevels()
    {
        var dbName = Guid.NewGuid().ToString();
        await using var ctx = BuildInMemoryContext(dbName);
        ctx.TDrones.Add(
            new TDrone { SerialNumber = "DRN-100", BatteryLevel = 65, Model = 1, WeightLimit = 300, BatteryCapacity = 100, State = 0 }
        );
        await ctx.SaveChangesAsync();

        var service = BuildService(ctx);
        await service.CheckBatteriesAsync(CancellationToken.None);

        var log = await ctx.BatteryLogs.FirstAsync();
        Assert.Equal("DRN-100", log.SerialNumber);
        Assert.Equal(65, log.BatteryLevel);
    }

    [Fact]
    public async Task BatteryMonitorService_WhenNoDrones_InsertsNoLogs()
    {
        var dbName = Guid.NewGuid().ToString();
        await using var ctx = BuildInMemoryContext(dbName);

        var service = BuildService(ctx);
        await service.CheckBatteriesAsync(CancellationToken.None);

        Assert.Empty(await ctx.BatteryLogs.ToListAsync());
    }
}
