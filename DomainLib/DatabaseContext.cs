using DomainLib.Entities;
using Microsoft.EntityFrameworkCore;

namespace DomainLib;
/// <inheritdoc />
public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options){}

    #region DbSets

    public DbSet<CredentialsPair> CredentialsPairs => Set<CredentialsPair>();

    #endregion
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) {}

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        AddTimestamps();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new())
    {
        AddTimestamps();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void AddTimestamps()
    {
        var entities = ChangeTracker.Entries()
            .Where(x => x.Entity is BaseEntity && x.State is EntityState.Added or EntityState.Modified);

        foreach (var entity in entities)
        {
            var now = DateTime.UtcNow;
            if (entity.State == EntityState.Added)
                ((BaseEntity) entity.Entity).CreatedAt = now;
            ((BaseEntity) entity.Entity).UpdatedAt = now;
        }
    }
}