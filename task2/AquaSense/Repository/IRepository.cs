namespace AquaSense.Repository;

public interface IRepository<TEntity> where TEntity : class
{
    Task<bool> AddAsync(TEntity entity, CancellationToken ct = default);
    Task<bool> UpdateAsync(TEntity entity, CancellationToken ct = default);
    Task<bool> DeleteAsync(long id, CancellationToken ct = default);
    Task<TEntity> GetByIdAsync(long id, CancellationToken ct = default);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken ct = default);
}
