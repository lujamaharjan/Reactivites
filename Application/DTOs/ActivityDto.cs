using Application.Profiles;

namespace Application.DTOs;

public class ActivityDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;

    public DateTime Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Venue { get; set; } = string.Empty;
    public string HostUsername { get; set; } = string.Empty;
    public bool IsCancelled { get; set; }
    private ICollection<Profile> Attendees { get; set; } = new List<Profile>();
}