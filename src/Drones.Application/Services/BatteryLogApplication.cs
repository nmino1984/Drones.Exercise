using Drones.Application.Commons.Bases;
using Drones.Application.Interfaces;
using Drones.Application.ViewModels.BatteryLog;
using Drones.Infrastructure.Persistences.Interfaces;
using Drones.Utilities.Statics;

namespace Drones.Application.Services;

public class BatteryLogApplication : IBatteryLogApplication
{
    private readonly IBatteryLogRepository _batteryLogRepository;

    public BatteryLogApplication(IBatteryLogRepository batteryLogRepository)
    {
        _batteryLogRepository = batteryLogRepository;
    }

    public async Task<BaseResponse<List<BatteryLogResponseViewModel>>> GetAllAsync()
    {
        var response = new BaseResponse<List<BatteryLogResponseViewModel>>();
        var logs = await _batteryLogRepository.GetAllAsync();

        response.IsSuccess = true;
        response.Data = logs.Select(Map).ToList();
        response.Message = response.Data.Count > 0 ? ReplyMessages.MESSAGE_QUERY : ReplyMessages.MESSAGE_QUERY_EMPTY;

        return response;
    }

    public async Task<BaseResponse<List<BatteryLogResponseViewModel>>> GetByDroneIdAsync(int droneId)
    {
        var response = new BaseResponse<List<BatteryLogResponseViewModel>>();
        var logs = await _batteryLogRepository.GetByDroneIdAsync(droneId);

        response.IsSuccess = true;
        response.Data = logs.Select(Map).ToList();
        response.Message = response.Data.Count > 0 ? ReplyMessages.MESSAGE_QUERY : ReplyMessages.MESSAGE_QUERY_EMPTY;

        return response;
    }

    private static BatteryLogResponseViewModel Map(Drones.Domain.Entities.BatteryLog b) => new()
    {
        Id = b.Id,
        DroneId = b.DroneId,
        SerialNumber = b.SerialNumber,
        BatteryLevel = b.BatteryLevel,
        CheckedAt = b.CheckedAt,
    };
}
