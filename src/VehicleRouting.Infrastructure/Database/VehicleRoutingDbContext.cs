using Microsoft.EntityFrameworkCore;
using VehicleRouting.Domain.Entities;

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
            .HasIndex(d => d.Id);

        modelBuilder.Entity<Driver>()
            .Property(d => d.Name)
            .HasMaxLength(64)
            .IsRequired();

        modelBuilder.Entity<Place>()
            .HasIndex(p => p.Id);

        modelBuilder.Entity<Place>()
            .Property(p => p.Name)
            .HasMaxLength(100)
            .IsRequired(false);

        modelBuilder.Entity<Place>()
            .Property(p => p.Description)
            .HasMaxLength(255)
            .IsRequired(false);

        modelBuilder.Entity<Vehicle>()
            .HasIndex(v => v.Id);

        modelBuilder.Entity<Vehicle>()
            .Property(v => v.Plate)
            .HasMaxLength(8)
            .IsRequired();

        modelBuilder.Entity<WorkSchedule>()
            .HasIndex(w => w.Id);

        modelBuilder.Entity<WorkSchedule>()
            .Property(w => w.Start)
            .IsRequired();

        modelBuilder.Entity<WorkSchedule>()
            .Property(w => w.End)
            .IsRequired();

        modelBuilder.Entity<WorkSchedule>()
            .Property(w => w.Day)
            .IsRequired();
    }
}