using AquaSense.Models;
using AquaSense.Repository;

public interface IAquariumService
{
    Task<Aquarium> CreateAquariumAsync(Aquarium aquarium);
    Task<bool> DeleteAquariumAsync(long id);
    Task<Aquarium> UpdateAquariumAsync(long id, Aquarium updatedAquarium);
    Task<Aquarium> GetAquariumAsync(long id);
}

public class AquariumService : IAquariumService
{
    private readonly IRepository<Aquarium> _aquariumRepository;

    public AquariumService(IRepository<Aquarium> aquariumRepository)
    {
        _aquariumRepository = aquariumRepository;
    }

    public async Task<Aquarium> CreateAquariumAsync(Aquarium aquarium)
    {
        var result = await _aquariumRepository.AddAsync(aquarium);
        return result ? aquarium : null;
    }

    public async Task<bool> DeleteAquariumAsync(long id)
    {
        var aquarium = await _aquariumRepository.GetByIdAsync(id);
        if (aquarium == null)
            return false;

        return await _aquariumRepository.DeleteAsync(id);
    }

    public async Task<Aquarium> UpdateAquariumAsync(long id, Aquarium updatedAquarium)
    {
        var aquarium = await _aquariumRepository.GetByIdAsync(id);
        if (aquarium == null)
            return null;

        aquarium.Name = updatedAquarium.Name;
        aquarium.Description = updatedAquarium.Description;

        var result = await _aquariumRepository.UpdateAsync(aquarium);
        return result ? aquarium : null;
    }

    public async Task<Aquarium> GetAquariumAsync(long id)
    {
        return await _aquariumRepository.GetByIdAsync(id);
    }
}
