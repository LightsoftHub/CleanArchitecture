using CleanArchitechture.Db;
using Light.Identity.EntityFrameworkCore;

namespace CleanArchitechture.Identity;

public class AppIdentityDbContext(
    ICurrentUser currentUser,
    TimeProvider timeProvider,
    DbContextOptions<AppIdentityDbContext> options) :
    IdentityContext(options)
{
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
}