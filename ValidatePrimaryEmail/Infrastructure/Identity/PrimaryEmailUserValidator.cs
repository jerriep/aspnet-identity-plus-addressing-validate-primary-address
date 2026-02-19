using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ValidatePrimaryEmail.Infrastructure.Identity;

public class PrimaryEmailUserValidator : IUserValidator<IdentityUser>
{
    public async Task<IdentityResult> ValidateAsync(
        UserManager<IdentityUser> manager,
        IdentityUser user
    )
    {
        if (string.IsNullOrEmpty(user.Email))
        {
            return IdentityResult.Success;
        }

        var email = PrimaryEmailHelper.ExtractPrimaryEmailAddress(user.Email);
        var matchedUser = await manager.Users.FirstOrDefaultAsync(u =>
            EF.Property<string>(u, PrimaryEmailHelper.PrimaryEmailPropertyName) == email
        );

        if (matchedUser != null && !string.Equals(matchedUser.Id, user.Id))
        {
            return IdentityResult.Failed(
                new IdentityError
                {
                    Code = "PRIMARY_EMAIL_REUSE_DISALLOWED",
                    Description =
                        $"An account with the primary email {email.ToLowerInvariant()} already exists. You cannot register multiple accounts with the same primary email.",
                }
            );
        }

        return IdentityResult.Success;
    }
}
