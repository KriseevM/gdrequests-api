using gdrequests_api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace gdrequests_api.Data;

public class MainDataContext : DbContext
{
    public DbSet<Level> Levels { get; set; } = null!;
    private IConfiguration _config;
    public MainDataContext(IConfiguration config, DbContextOptions<MainDataContext> options) : base(options)
    {
        _config = config;
        Database.Migrate();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(_config.GetConnectionString("levels"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Level>().Property(p => p.AddedAt)
            .HasConversion<int>(
                v => (int)((DateTime.UtcNow - DateTime.UnixEpoch).TotalSeconds),
                v => DateTime.UnixEpoch.AddSeconds(v))
            .HasDefaultValueSql("UNIXEPOCH()");
        modelBuilder.Entity<Level>().HasKey(p => p.Id);
        modelBuilder.Entity<Level>().HasIndex(p => p.ServerId).IsUnique();
        modelBuilder.Entity<Level>().HasIndex(p => p.AddedAt);
    }
}