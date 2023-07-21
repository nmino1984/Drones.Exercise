namespace Drones.Application.ViewModels.Drone.Request
{
    public class DroneMedicationRequestViewModel
    {
        public int IdDrone { get; set; }
        public int IdMedication { get; set; }
        public string? Name { get; set; }
        public int Model { get; set; }
        public int? WeightLimit { get; set; }
        public int? BatteryCapacity { get; set; }
        public int State { get; set; }
    }
}
