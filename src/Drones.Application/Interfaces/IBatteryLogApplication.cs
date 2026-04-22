using Drones.Application.Commons.Bases;
using Drones.Application.ViewModels.BatteryLog;

namespace Drones.Application.Interfaces;

public interface IBatteryLogApplication
{
    Task<BaseResponse<List<BatteryLogResponseViewModel>>> GetAllAsync();
    Task<BaseResponse<List<BatteryLogResponseViewModel>>> GetByDroneIdAsync(int droneId);
}
