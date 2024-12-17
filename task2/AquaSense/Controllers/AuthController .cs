using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthenticationService _authenticationService;

    public AuthController(AuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        try
        {
            var token = await _authenticationService.RegisterAsync(model.Username, model.Password);
            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        try
        {
            var token = await _authenticationService.LoginAsync(model.Username, model.Password);
            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}

public class RegisterModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}
