using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class IoTController : ControllerBase
{
    private readonly IIoTService _ioTService;

    public IoTController(IIoTService ioTService)
    {
        _ioTService = ioTService;
    }

    [HttpPut("updateTemperature/{aquariumId}")]
    public async Task<IActionResult> UpdateTemperature(long aquariumId, [FromBody] float temperature)
    {
        var success = await _ioTService.UpdateTemperatureAsync(aquariumId, temperature);
        if (success)
            return Ok("Temperature updated.");

        return NotFound("Aquarium not found.");
    }

    [HttpPut("controlLight/{aquariumId}")]
    public async Task<IActionResult> ControlLight(long aquariumId, [FromBody] bool isOn)
    {
        var success = await _ioTService.ControlLightAsync(aquariumId, isOn);
        if (success)
            return Ok("Light status updated.");

        return NotFound("Aquarium not found.");
    }

    [HttpPut("feedFish/{aquariumId}")]
    public async Task<IActionResult> FeedFish(long aquariumId)
    {
        var success = await _ioTService.FeedFishAsync(aquariumId);
        if (success)
            return Ok("Fish fed.");

        return NotFound("Aquarium not found.");
    }
}
