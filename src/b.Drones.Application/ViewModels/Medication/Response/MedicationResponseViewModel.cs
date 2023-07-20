namespace Drones.Application.ViewModels.Response
{
    public class MedicationResponseViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Weight { get; set; }
        public string? Code { get; set; }
        public byte[]? Image { get; set; }

    }
}
