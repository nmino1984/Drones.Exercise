using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Drones.Domain.Entities;

public partial class TDroneConfiguration : IEntityTypeConfiguration<TDrone>
{
    public void Configure(EntityTypeBuilder<TDrone> builder)
    {
        builder.HasKey(e => e.Id);

        builder.ToTable("tDrone");

        builder.Property(e => e.Id);
        builder.Property(e => e.WeightLimit);
        builder.Property(e => e.BatteryCapacity);
        builder.Property(e => e.Batterylevel);
        builder.Property(e => e.State);
        builder.Property(e => e.SerialNumber) //It refers to Serial Number
            .HasMaxLength(100)
            .IsUnicode(false);

        //builder.HasOne(d => d.ModelNavigation).WithMany(p => p.TDrones)
        //    .HasForeignKey(d => d.Model)
        //    .OnDelete(DeleteBehavior.ClientSetNull)
        //    .HasConstraintName("FK_tDrone_nModel");

        //builder.HasOne(d => d.StateNavigation).WithMany(p => p.TDrones)
        //    .HasForeignKey(d => d.State)
        //    .HasConstraintName("FK_tDrone_nState");
    }
}
