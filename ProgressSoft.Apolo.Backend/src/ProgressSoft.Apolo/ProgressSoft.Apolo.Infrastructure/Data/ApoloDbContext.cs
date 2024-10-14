using Microsoft.EntityFrameworkCore;
using ProgressSoft.Apolo.Domain;
using ProgressSoft.Apolo.Infrastructure.Data.EntitiesConfigurations;

namespace ProgressSoft.Apolo.Infrastructure;

public class ApoloDbContext : DbContext
{
    public ApoloDbContext() {}
    public ApoloDbContext(DbContextOptions<ApoloDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

    {

        optionsBuilder.UseSqlServer("Server=ABDALHAMEED-PC\\SQLEXPRESS;Database=ProgressSoft.Apolo;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;");

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new BusinessCardEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ImageEntityTypeConfiguration());
    }

    public DbSet<BusinessCard> BusinessCards { get; set; }
    public DbSet<Image> Images { get; set; }
}
