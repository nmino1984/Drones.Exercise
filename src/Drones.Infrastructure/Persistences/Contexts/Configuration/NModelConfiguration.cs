using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Drones.Domain.Entities;

public partial class NModelConfiguration : IEntityTypeConfiguration<NModel>
{
    public void Configure(EntityTypeBuilder<NModel> builder)
    {
        builder.ToTable("nModel");

        builder.Property(e => e.Name) //It refers to Model Name
            .HasMaxLength(50)
            .IsUnicode(false);
    }
}
