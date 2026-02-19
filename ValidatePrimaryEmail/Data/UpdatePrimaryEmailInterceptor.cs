using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ValidatePrimaryEmail.Infrastructure.Identity;

namespace ValidatePrimaryEmail.Data;

public class UpdatePrimaryEmailInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result
    )
    {
        if (eventData.Context is not { } context)
        {
            return result;
        }

        UpdatePrimaryEmailAddresses(context.ChangeTracker.Entries());

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken()
    )
    {
        if (eventData.Context is not { } context)
        {
            return ValueTask.FromResult(result);
        }

        UpdatePrimaryEmailAddresses(context.ChangeTracker.Entries());

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdatePrimaryEmailAddresses(IEnumerable<EntityEntry> entries)
    {
        foreach (var entry in entries)
        {
            if (entry.State is EntityState.Added or EntityState.Modified)
            {
                var email = entry.Property(nameof(IdentityUser.Email)).CurrentValue?.ToString();
                if (email is null)
                {
                    entry.Property(PrimaryEmailHelper.PrimaryEmailPropertyName).CurrentValue = null;
                    continue;
                }

                entry.Property(PrimaryEmailHelper.PrimaryEmailPropertyName).CurrentValue =
                    PrimaryEmailHelper.ExtractPrimaryEmailAddress(email);
            }
        }
    }
}
