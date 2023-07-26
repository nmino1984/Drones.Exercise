namespace Drones.Domain.Entities;

public partial class TDrone : BaseEntity
{
    public string? SerialNumber { get; set; }

    public int Model { get; set; }

    public double WeightLimit { get; set; }

    public double BatteryCapacity { get; set; }

    public double BatteryLevel { get; set; }

    public int State { get; set; }

    public virtual ICollection<RDroneMedication> RDroneMedications { get; set; } = new List<RDroneMedication>();

    //public virtual NModel ModelNavigation { get; set; } = null!;


    //public virtual NState? StateNavigation { get; set; }
}
