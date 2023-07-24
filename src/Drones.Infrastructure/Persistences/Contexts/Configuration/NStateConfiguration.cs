using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Drones.Domain.Entities;

public partial class NStateConfiguration : IEntityTypeConfiguration<NState>
{
    public void Configure(EntityTypeBuilder<NState> builder)
    {
        builder.HasKey(e => e.Id);

        builder.ToTable("nState");

        builder.Property(e => e.Id);

        builder.Property(e => e.Name) //It refers to State
            .HasMaxLength(50)
            .IsUnicode(false);
    }
}
