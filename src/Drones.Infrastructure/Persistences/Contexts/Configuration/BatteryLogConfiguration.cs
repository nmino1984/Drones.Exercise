using Drones.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Drones.Infrastructure.Persistences.Contexts.Configuration;

public class BatteryLogConfiguration : IEntityTypeConfiguration<BatteryLog>
{
    public void Configure(EntityTypeBuilder<BatteryLog> builder)
    {
        builder.HasKey(e => e.Id);

        builder.ToTable("BatteryLog");

        builder.Property(e => e.DroneId)
            .IsRequired();

        builder.Property(e => e.SerialNumber)
            .HasMaxLength(100)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(e => e.BatteryLevel)
            .IsRequired();

        builder.Property(e => e.CheckedAt)
            .HasColumnType("datetime")
            .IsRequired();
    }
}
