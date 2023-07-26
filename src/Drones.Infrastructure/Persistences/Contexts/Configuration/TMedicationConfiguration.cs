using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Drones.Domain.Entities;

public partial class TMedicationConfiguration : IEntityTypeConfiguration<TMedication>
{
    public void Configure(EntityTypeBuilder<TMedication> builder)
    {
        builder.HasKey(e => e.Id);

        builder.ToTable("tMedication");

        builder.Property(e => e.Id);

        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .IsUnicode(false);
        builder.Property(e => e.Code)
            .HasMaxLength(255)
            .IsUnicode(false);
        builder.Property(e => e.Image)
            .HasMaxLength(255)
            .IsUnicode(false);
    }
}
