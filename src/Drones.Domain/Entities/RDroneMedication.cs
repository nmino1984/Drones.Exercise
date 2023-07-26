namespace Drones.Domain.Entities;

public partial class RDroneMedication : BaseEntity
{
    public int IdDrone { get; set; }

    public int IdMedication { get; set; }

    public DateTime DateOpperation { get; set; }

    public virtual TDrone IdDroneNavigation { get; set; } = null!;

    public virtual TMedication IdMedicationNavigation { get; set; } = null!;
}
