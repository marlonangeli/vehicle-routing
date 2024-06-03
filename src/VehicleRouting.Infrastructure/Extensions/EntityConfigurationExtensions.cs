using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleRouting.Domain.Common;

namespace VehicleRouting.Infrastructure.Extensions;

public static class EntityConfigurationExtensions
{
    public static void ConfigureBaseEntity<T> (this EntityTypeBuilder<T> builder) where T : Entity
    {
        builder.ToTable(typeof(T).Name)
            .HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .ValueGeneratedNever();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.ModifiedAt)
            .IsRequired(false);
    }
}