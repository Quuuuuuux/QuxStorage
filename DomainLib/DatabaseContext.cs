using DomainLib.Entities;
using Microsoft.EntityFrameworkCore;

namespace DomainLib;
/// <inheritdoc />
public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options){}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) {}

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void AddTimestamps()
    {
        var entities = ChangeTracker.Entries<BaseEntity>()
            .Where(x => x.State is EntityState.Added or EntityState.Modified);
    }
}