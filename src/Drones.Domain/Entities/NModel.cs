namespace Drones.Domain.Entities;

public partial class NModel : BaseEntity
{
    public string? Name { get; set; }

    public virtual ICollection<TDrone> TDrones { get; set; } = new List<TDrone>();
}
