using System;
using System.Collections.Generic;
using System.Reflection;
using Drones.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Drones.Infrastructure.Persistences.Contexts;

public partial class DronesContext : DbContext
{
    public DronesContext()
    {
    }

    public DronesContext(DbContextOptions<DronesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TDrone> TDrones { get; set; }
     
    public virtual DbSet<TMedication> TMedications { get; set; }

    public virtual DbSet<RDroneMedication> RDroneMedications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        //modelBuilder.ApplyConfiguration<TDrone>(new TDroneConfiguration());
        //modelBuilder.ApplyConfiguration<TMedication>(new TMedicationConfiguration());
        //modelBuilder.ApplyConfiguration<RDroneMedication>(new RDroneMedicationConfiguration());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
