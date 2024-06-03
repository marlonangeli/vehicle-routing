using Microsoft.EntityFrameworkCore;
using VehicleRouting.Domain.Common;
using VehicleRouting.Domain.Constants;
using VehicleRouting.Domain.Entities;
using VehicleRouting.Infrastructure.Extensions;

namespace VehicleRouting.Infrastructure.Database;

public sealed class VehicleRoutingDbContext : DbContext
{
    public VehicleRoutingDbContext(DbContextOptions<VehicleRoutingDbContext> options) : base(options)
    {
    }

    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Place> Places { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<WorkSchedule> WorkSchedules { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyUtcDateTimeConverter();

        // Relationships

        modelBuilder.Entity<Driver>()
            .HasMany(d => d.WorkSchedules)
            .WithOne(ws => ws.Driver)
            .HasForeignKey(ws => ws.DriverId);

        modelBuilder.Entity<WorkSchedule>()
            .HasOne(ws => ws.Driver)
            .WithMany(d => d.WorkSchedules)
            .HasForeignKey(ws => ws.DriverId);


        // Model configuration

        modelBuilder.Entity<Driver>()
            .ConfigureBaseEntity();

        modelBuilder.Entity<Driver>()
            .Property(d => d.Name)
            .HasMaxLength(EntityConstants.Driver.NameMaxLength)
            .IsRequired();

        modelBuilder.Entity<Place>()
            .ConfigureBaseEntity();

        modelBuilder.Entity<Place>()
            .Property(p => p.Name)
            .HasMaxLength(EntityConstants.Place.NameMaxLength)
            .IsRequired(false);

        modelBuilder.Entity<Place>()
            .Property(p => p.Description)
            .HasMaxLength(EntityConstants.Place.DescriptionMaxLength)
            .IsRequired(false);

        modelBuilder.Entity<Place>()
            .ComplexProperty(p => p.Address, a =>
            {
                a.Property(address => address.Latitude)
                    .IsRequired(false);
                a.Property(address => address.Longitude)
                    .IsRequired(false);
                a.Property(address => address.FullAddress)
                    .HasMaxLength(EntityConstants.Place.FullAddressMaxLength)
                    .IsRequired(false);
            });

        modelBuilder.Entity<Vehicle>()
            .ConfigureBaseEntity();

        modelBuilder.Entity<Vehicle>()
            .Property(v => v.Plate)
            .HasMaxLength(EntityConstants.Vehicle.PlateMaxLength)
            .IsRequired();

        modelBuilder.Entity<Vehicle>()
            .ComplexProperty(v => v.Model, m =>
            {
                m.IsRequired();

                m.Property(model => model.Brand)
                    .HasMaxLength(EntityConstants.Vehicle.ModelBrandMaxLength)
                    .IsRequired(false);
                m.Property(model => model.Model)
                    .HasMaxLength(EntityConstants.Vehicle.ModelNameMaxLength)
                    .IsRequired(false);
                m.Property(model => model.Year)
                    .IsRequired(false);
            });

        modelBuilder.Entity<WorkSchedule>()
            .ConfigureBaseEntity();

        modelBuilder.Entity<WorkSchedule>()
            .Property(w => w.Start)
            .IsRequired();

        modelBuilder.Entity<WorkSchedule>()
            .Property(w => w.End)
            .IsRequired();

        modelBuilder.Entity<WorkSchedule>()
            .Property(w => w.Day)
            .IsRequired();

        // Ignore base entity

        modelBuilder.Ignore<Entity>();

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditableEntities();

        return await base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditableEntities()
    {
        var utcNow = DateTime.UtcNow;

        foreach (var entityEntry in ChangeTracker.Entries<Entity>())
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(nameof(Entity.CreatedAt)).CurrentValue = utcNow;
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(nameof(Entity.ModifiedAt)).CurrentValue = utcNow;
            }
        }
    }
}