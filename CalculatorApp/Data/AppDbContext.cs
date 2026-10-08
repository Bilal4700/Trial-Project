using CalculatorApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CalculatorApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Users> Users => Set<Users>();

    public DbSet<LoginLog> LoginLogs => Set<LoginLog>();

    public DbSet<CalculationLog> CalculationLogs => Set<CalculationLog>();

    public DbSet<Review> Reviews => Set<Review>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(user => user.Id);

            entity.Property(user => user.Username)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(user => user.Password)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(user => user.CreatedAtUtc)
                .IsRequired();

            entity.HasIndex(user => user.Username)
                .IsUnique();
        });

        modelBuilder.Entity<LoginLog>(entity =>
        {
            entity.HasKey(log => log.Id);

            entity.Property(log => log.Username)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(log => log.FailureReason)
                .HasMaxLength(200);

            entity.Property(log => log.AttemptedAtUtc)
                .IsRequired();
        });

        modelBuilder.Entity<CalculationLog>(entity =>
        {
            entity.HasKey(log => log.Id);

            entity.Property(log => log.Operation)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(log => log.FirstNumber)
                .HasPrecision(18, 4);

            entity.Property(log => log.SecondNumber)
                .HasPrecision(18, 4);

            entity.Property(log => log.Result)
                .HasPrecision(18, 4);

            entity.Property(log => log.CreatedAtUtc)
                .IsRequired();
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(review => review.Id);

            entity.Property(review => review.Name)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(review => review.Comment)
                .IsRequired()
                .HasMaxLength(1000);

            entity.Property(review => review.CreatedAtUtc)
                .IsRequired();
        });
    }
}
