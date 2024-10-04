using Microsoft.EntityFrameworkCore;
using ProgressSoft.Apolo.Domain;

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
    }

    public DbSet<BusinessCard> BusinessCards { get; set; }
}
