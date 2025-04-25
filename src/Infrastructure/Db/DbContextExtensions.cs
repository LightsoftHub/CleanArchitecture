using Light.Domain.Entities.Interfaces;
using Light.Domain.ValueObjects;

namespace CleanArchitechture.Db;

public static class DbContextExtensions
{
    public static void AuditEntries<TContext>(this TContext context, string? userId, DateTimeOffset auditTime, bool enableSoftDelete = false)
        where TContext : DbContext
    {
        var changeTracker = context.ChangeTracker;

        // fix null value when delete for Entities inherited ISoftDelete & ValueObjects
        changeTracker.Entries<ValueObject>()
            .Where(x => x.State is EntityState.Deleted)
            .ToList()
            .ForEach(e => e.State = EntityState.Unchanged);

        // auto set audit values for Auditable entities
        changeTracker.Entries<IAuditableEntity>()
            .ToList()
            .ForEach(e =>
            {
                switch (e.State)
                {
                    case EntityState.Added:
                        e.Entity.Created = auditTime;
                        e.Entity.CreatedBy = userId;
                        e.Entity.LastModified = auditTime;
                        e.Entity.LastModifiedBy = userId;
                        break;

                    case EntityState.Modified:
                        e.Entity.LastModified = auditTime;
                        e.Entity.LastModifiedBy = userId;
                        break;

                    case EntityState.Deleted:
                        if (e.Entity is ISoftDelete softDelete && enableSoftDelete)
                        {
                            softDelete.Deleted = auditTime;
                            softDelete.DeletedBy = userId;
                            e.State = EntityState.Modified;
                        }
                        break;
                }
            });
    }
}
