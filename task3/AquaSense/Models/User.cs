using System;
using System.Text.Json.Serialization;

namespace AquaSense.Models;

public class User
{
    public long Id { get; set; }
    public string Username { get; set; }

    [JsonIgnore]
    public string PasswordHash { get; set; }
    public string Role { get; set; }
}
