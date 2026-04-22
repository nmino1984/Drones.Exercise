namespace Drones.Application.ViewModels.BatteryLog;

public class BatteryLogResponseViewModel
{
    public int Id { get; set; }
    public int DroneId { get; set; }
    public string SerialNumber { get; set; } = string.Empty;
    public double BatteryLevel { get; set; }
    public DateTime CheckedAt { get; set; }
}
