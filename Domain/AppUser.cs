using Microsoft.AspNetCore.Identity;

namespace Domain;

public class AppUser :IdentityUser
{
    public AppUser(string displayName, string? email, string? userName)
    {
        DisplayName = displayName;
        Email = email;
        UserName = userName;
    }

    public string DisplayName { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
}