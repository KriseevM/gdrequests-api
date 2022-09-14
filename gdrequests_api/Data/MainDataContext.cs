using gdrequests_api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace gdrequests_api.Data;

public class MainDataContext : DbContext
{
    public DbSet<Request> Requests { get; set; } = null!;
    public DbSet<LevelMetadata> LevelMetadata { get; set; } = null!;
    private readonly IConfiguration _config;
    public MainDataContext(IConfiguration config, DbContextOptions<MainDataContext> options) : base(options)
    {
        _config = config;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(_config.GetConnectionString("levels"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Request>().Property(p => p.AddedAt)
            .HasDefaultValueSql("strftime('%s', 'now')");
        modelBuilder.Entity<Request>().HasKey(p => p.Id);
        modelBuilder.Entity<Request>().HasIndex(p => p.LevelId).IsUnique();
        modelBuilder.Entity<Request>().HasIndex(p => p.AddedAt);
        modelBuilder.Entity<Request>()
            .HasOne(l => l.Metadata)
            .WithOne(m => m.Request).
            HasForeignKey<LevelMetadata>(m => m.RequestId);
    }
}