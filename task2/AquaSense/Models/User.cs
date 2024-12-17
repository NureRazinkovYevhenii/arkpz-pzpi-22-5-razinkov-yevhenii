using System;

namespace AquaSense.Models;

public class User
{
    public long Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public virtual ICollection<Aquarium> Aquariums { get; set; } = new List<Aquarium>();
}
