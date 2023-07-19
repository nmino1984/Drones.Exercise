namespace Drones.Domain.Entities;

public partial class NState : BaseEntity
{
    public virtual ICollection<TDrone> TDrones { get; set; } = new List<TDrone>();
}
