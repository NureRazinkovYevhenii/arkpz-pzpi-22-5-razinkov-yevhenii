using AquaSense.Models;
using AquaSense.Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthenticationService
{
    private readonly IRepository<User> _userRepository;
    private readonly IConfiguration _configuration;

    public AuthenticationService(IRepository<User> userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<string> RegisterAsync(string username, string password)
    {
        var existingUser = (await _userRepository.GetAllAsync()).FirstOrDefault(u => u.Username == username);
        if (existingUser != null)
            throw new Exception("User already exists.");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

        // Створюємо нового користувача
        var user = new User
        {
            Username = username,
            PasswordHash = passwordHash
        };

        await _userRepository.AddAsync(user);
        return GenerateJwtToken(user);
    }

    public async Task<string> LoginAsync(string username, string password)
    {
        // Знаходимо користувача
        var user = (await _userRepository.GetAllAsync()).FirstOrDefault(u => u.Username == username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            throw new Exception("Invalid username or password.");

        return GenerateJwtToken(user);
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Username)
    };

        // Получаем ключ из конфигурации
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpireMinutes"])),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}