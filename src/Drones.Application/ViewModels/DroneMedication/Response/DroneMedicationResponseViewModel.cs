namespace Drones.Application.ViewModels.Drone.Response
{
    public class DroneMedicationResponseViewModel
    {
        public int DroneId { get; set; }
        public int MedicationId { get; set; }
        public string? DroneSerialNumber { get; set; }
        public string? DroneModel { get; set; }
        public string? MedicationName { get; set; }
        public string? DateOpperation { get; set; }

    }
}
