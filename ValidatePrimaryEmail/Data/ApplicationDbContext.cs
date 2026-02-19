using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ValidatePrimaryEmail.Infrastructure.Identity;

namespace ValidatePrimaryEmail.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<IdentityUser>(eb =>
        {
            eb.Property<string>(PrimaryEmailHelper.PrimaryEmailPropertyName).HasMaxLength(256);

            eb.HasIndex(PrimaryEmailHelper.PrimaryEmailPropertyName)
                .HasDatabaseName("PrimaryEmailName")
                .IsUnique();
        });

        base.OnModelCreating(builder);
    }
}
