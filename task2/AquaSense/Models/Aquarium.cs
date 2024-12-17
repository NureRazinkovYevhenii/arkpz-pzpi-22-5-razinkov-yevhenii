namespace AquaSense.Models;

public class Aquarium
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public long UserId { get; set; }
    public float Temperature { get; set; }
    public bool IsLightOn { get; set; }
    public DateTime LastFeedTime { get; set; }
}
