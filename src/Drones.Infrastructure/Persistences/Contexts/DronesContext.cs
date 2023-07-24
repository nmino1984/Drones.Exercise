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

    public virtual DbSet<TMedicationConfiguration> TMedications { get; set; }

    //public virtual DbSet<NModelConfiguration> NModels { get; set; }

    //public virtual DbSet<NStateConfiguration> NStates { get; set; }

    public virtual DbSet<RDroneMedicationConfiguration> RDroneMedications { get; set; }

    public virtual DbSet<TDroneConfiguration> TDrones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
