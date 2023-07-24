namespace Drones.Application.ViewModels.Drone.Request
{
    public class DroneRequestViewModel
    {
        public string? SerialNumber { get; set; }
        public int Model { get; set; }
        public int? WeightLimit { get; set; }
        public int? BatteryCapacity { get; set; }
        public int State { get; set; }
    }
}
