using System.Text.RegularExpressions;

namespace ValidatePrimaryEmail.Infrastructure.Identity;

public partial class PrimaryEmailHelper
{
    public const string PrimaryEmailPropertyName = "PrimaryEmail";

    public static string ExtractPrimaryEmailAddress(string value)
    {
        if (PlusAddressingRegex().Match(value) is not { Success: true } match)
        {
            return value.Normalize().ToUpperInvariant();
        }

        return $"{match.Groups["username"].Value}@{match.Groups["domain"].Value}"
            .Normalize()
            .ToUpperInvariant();
    }

    [GeneratedRegex(@"^(?<username>.+)(?<subaddress>\+.*)@(?<domain>.+)$")]
    private static partial Regex PlusAddressingRegex();
}
