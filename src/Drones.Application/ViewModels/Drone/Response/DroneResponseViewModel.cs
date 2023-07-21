namespace Drones.Application.ViewModels.Drone.Response
{
    public class DroneResponseViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Model { get; set; }
        public int? WeightLimit { get; set; }
        public int? BatteryCapacity { get; set; }
        public string? State { get; set; }

    }
}
