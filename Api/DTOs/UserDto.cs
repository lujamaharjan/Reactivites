namespace Api.DTOs;

public record UserDto
{
    public string DisplayName { get; init; } = string.Empty;
    public string Token { get; init; }= string.Empty;
    public string Username { get; init; }= string.Empty;
    public string Image { get; init; }= string.Empty;
}