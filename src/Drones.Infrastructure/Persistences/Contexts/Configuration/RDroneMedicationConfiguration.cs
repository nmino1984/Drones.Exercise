using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Drones.Domain.Entities;

public partial class RDroneMedicationConfiguration : IEntityTypeConfiguration<RDroneMedication>
{
    public void Configure(EntityTypeBuilder<RDroneMedication> builder)
    {
        builder.HasKey(e => e.Id);

        builder.ToTable("rDroneMedication");

        builder.Property(e => e.Id);

        builder.HasOne(d => d.IdDroneNavigation).WithMany(p => p.RDroneMedications)
            .HasForeignKey(d => d.IdDrone)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_rDroneMedication_tDrone");

        builder.HasOne(d => d.IdMedicationNavigation).WithMany(p => p.RDroneMedications)
            .HasForeignKey(d => d.IdMedication)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_rDroneMedication_tMedication");
    }
}
