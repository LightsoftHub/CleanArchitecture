using CleanArchitecture.Db;
using CleanArchitecture.SignalR.Models;
using Light.Identity.EntityFrameworkCore;

namespace CleanArchitecture.Identity;

public class AppIdentityDbContext(
    ICurrentUser currentUser,
    TimeProvider timeProvider,
    DbContextOptions<AppIdentityDbContext> options) :
    IdentityContext(options)
{
    public virtual DbSet<Notification> Notifications => Set<Notification>();

    public override int SaveChanges()
    {
        var now = timeProvider.GetUtcNow();
        this.AuditEntries(currentUser.UserId, now, false);
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var now = timeProvider.GetUtcNow();
        this.AuditEntries(currentUser.UserId, now, false);
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Notification>().ToTable(name: "Notifications", Schemas.System);
    }
}