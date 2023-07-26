namespace Drones.Application.ViewModels.Drone.Response
{
    public class DroneResponseViewModel
    {
        public int Id { get; set; }
        public string? SerialNumber { get; set; }
        public string? Model { get; set; }
        public double WeightLimit { get; set; }
        public double BatteryCapacity { get; set; }
        public double BatteryLevel { get; set; }
        public string? State { get; set; }

    }
}
