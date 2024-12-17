using AquaSense.Models;
using AquaSense.Repository;

public interface IIoTService
{
    Task<bool> UpdateTemperatureAsync(long aquariumId, float temperature);
    Task<bool> ControlLightAsync(long aquariumId, bool isOn);
    Task<bool> FeedFishAsync(long aquariumId);
}

public class IoTService : IIoTService
{
    private readonly IRepository<Aquarium> _aquariumRepository;

    public IoTService(IRepository<Aquarium> aquariumRepository)
    {
        _aquariumRepository = aquariumRepository;
    }

    public async Task<bool> UpdateTemperatureAsync(long aquariumId, float temperature)
    {
        var aquarium = await _aquariumRepository.GetByIdAsync(aquariumId);
        if (aquarium == null)
            return false;

        aquarium.Temperature = temperature;
        var result = await _aquariumRepository.UpdateAsync(aquarium);
        return result;
    }

    public async Task<bool> ControlLightAsync(long aquariumId, bool isOn)
    {
        var aquarium = await _aquariumRepository.GetByIdAsync(aquariumId);
        if (aquarium == null)
            return false;

        aquarium.IsLightOn = isOn;
        var result = await _aquariumRepository.UpdateAsync(aquarium);
        return result;
    }

    public async Task<bool> FeedFishAsync(long aquariumId)
    {
        var aquarium = await _aquariumRepository.GetByIdAsync(aquariumId);
        if (aquarium == null)
            return false;

        aquarium.LastFeedTime = DateTime.UtcNow;
        var result = await _aquariumRepository.UpdateAsync(aquarium);
        return result;
    }
}