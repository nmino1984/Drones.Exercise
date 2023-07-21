namespace Drones.Domain.Entities;

public partial class TDrone : BaseEntity
{
    public int Model { get; set; }

    public int WeightLimit { get; set; }

    public int BatteryCapacity { get; set; }

    public int State { get; set; }

    public virtual NModel ModelNavigation { get; set; } = null!;

    public virtual ICollection<RDroneMedication> RDroneMedications { get; set; } = new List<RDroneMedication>();

    public virtual NState? StateNavigation { get; set; }
}
