using GeneratedWorlds.Domain.Entities;
using GeneratedWorlds.Domain.Entities.Items;

namespace GeneratedWorlds.Application.Common.Interfaces
{
    public interface IItemRepository<T> where T : Item
    {
        Task<T?> GetByIdAsync(Guid reference);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> CreateAsync(T item);
    }
}
