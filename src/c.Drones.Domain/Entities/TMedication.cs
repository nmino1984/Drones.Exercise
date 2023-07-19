using Drones.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Drones.Domain.Entities;

public partial class TMedication : BaseEntity
{

    public int? Weight { get; set; }

    public string? Code { get; set; }

    public byte[]? Image { get; set; }

    public virtual ICollection<RDroneMedication> RDroneMedications { get; set; } = new List<RDroneMedication>();
}
