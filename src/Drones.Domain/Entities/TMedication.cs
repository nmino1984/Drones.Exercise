namespace Drones.Domain.Entities;

public partial class TMedication : BaseEntity
{
    public string? Name { get; set; }

    public double Weight { get; set; }

    public string? Code { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<RDroneMedication> RDroneMedications { get; set; } = new List<RDroneMedication>();
}
