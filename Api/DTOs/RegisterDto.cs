using System.ComponentModel.DataAnnotations;

namespace Api.DTOs;

public record RegisterDto()
{
    [Required]
    public string DisplayName { get; init; } = string.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; init; } = string.Empty;
    [Required]
    [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$", ErrorMessage = "Password must have 1 uppercase, 1 lowercase, 1 number, 1 non alphanumeric and at least 8 characters")]
    public string Password { get; init; } = string.Empty;
    [Required]
    public string Username { get; init; } = string.Empty;
}