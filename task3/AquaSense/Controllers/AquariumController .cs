using AquaSense.Models;
using AquaSense.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AquariumController : ControllerBase
{
    private readonly IAquariumService _aquariumService;

    public AquariumController(IAquariumService aquariumService)
    {
        _aquariumService = aquariumService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAquarium([FromBody] AquariumRequest aquariumRequest)
    {
        var userId = GetUserIdFromJwt();
        if (userId == null)
            return Unauthorized("User ID not found in token.");

        var aquarium = new Aquarium
        {
            Name = aquariumRequest.Name,
            Description = aquariumRequest.Description,
            UserId = userId.Value
        };

        var createdAquarium = await _aquariumService.CreateAquariumAsync(aquarium);
        if (createdAquarium != null)
            return Ok(createdAquarium);

        return BadRequest("Failed to create aquarium.");
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAquarium(long id)
    {
        var userId = GetUserIdFromJwt();
        if (userId == null)
            return Unauthorized("User ID not found in token.");

        var success = await _aquariumService.DeleteAquariumAsync(id);
        if (success)
            return Ok("Aquarium deleted.");

        return NotFound("Aquarium not found.");
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateAquarium(long id, [FromBody] AquariumRequest updatedAquariumRequest)
    {
        var userId = GetUserIdFromJwt();
        if (userId == null)
            return Unauthorized("User ID not found in token.");

        var updatedAquarium = new Aquarium
        {
            Id = id,
            Name = updatedAquariumRequest.Name,
            Description = updatedAquariumRequest.Description,
            UserId = userId.Value
        };

        var updated = await _aquariumService.UpdateAquariumAsync(id, updatedAquarium);
        if (updated != null)
            return Ok(updated);

        return BadRequest("Failed to update aquarium.");
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetAquarium(long id)
    {
        var userId = GetUserIdFromJwt();
        if (userId == null)
            return Unauthorized("User ID not found in token.");

        var aquarium = await _aquariumService.GetAquariumAsync(id);
        if (aquarium != null)
            return Ok(aquarium);

        return NotFound("Aquarium not found.");
    }

    private long? GetUserIdFromJwt()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
            return null;

        if (long.TryParse(userIdClaim, out var userId))
            return userId;

        return null;
    }
}
