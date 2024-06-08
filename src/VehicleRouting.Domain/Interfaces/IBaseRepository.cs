using VehicleRouting.Domain.Common;

namespace VehicleRouting.Domain.Interfaces;

public interface IBaseRepository<T> where T : Entity
{
    Task<T?> GetByIdAsync(Guid id);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task SaveChangesAsync();
}