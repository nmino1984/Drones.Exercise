namespace Drones.Domain.Entities;

public partial class NModel : BaseEntity
{
    public virtual ICollection<TDrone> TDrones { get; set; } = new List<TDrone>();
}
