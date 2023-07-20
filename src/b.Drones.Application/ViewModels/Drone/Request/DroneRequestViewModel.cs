namespace Drones.Application.ViewModels.Drone.Request
{
    public class DroneRequestViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Model { get; set; }
        public int? WeightLimit { get; set; }
        public int? BatteryCapacity { get; set; }
        public int State { get; set; }
    }
}
