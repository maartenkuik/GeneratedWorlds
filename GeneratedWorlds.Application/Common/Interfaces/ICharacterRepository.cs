using GeneratedWorlds.Domain.Entities;
using GeneratedWorlds.Domain.Entities.Items;
using GeneratedWorlds.Domain.Types;

namespace GeneratedWorlds.Application.Common.Interfaces
{
    public interface ICharacterRepository
    {
        Task<Character> GetByIdAsync(Guid reference);
        Task<Character> GetByNameAsync(string name);
        Task<Character> CreateAsync(Character character);
        Task<Character> UpdateSkillAsync(Guid reference, SkillType skill, int experience);
        Task<bool> AddItemToInventoryAsync(Guid characterReference, Item item, int quantity);
        Task SaveChangesAsync();
    }
}
