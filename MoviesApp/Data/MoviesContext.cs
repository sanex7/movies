using Microsoft.EntityFrameworkCore;
using MoviesApp.Models;

namespace MoviesApp.Data;

public class MoviesContext : DbContext
{
    public DbSet<Title> Titles { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite("Data Source=movies.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasMany(u => u.Titles)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Title>()
            .Property(t => t.Name)
            .HasMaxLength(50)
            .IsRequired();

        modelBuilder.Entity<Title>()
            .Property(t => t.Year)
            .IsRequired();

        modelBuilder.Entity<Title>()
            .Property(t => t.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}
