using GeneratedWorlds.Application.Common.Interfaces;
using GeneratedWorlds.Domain.Entities;
using GeneratedWorlds.Domain.Entities.Items;
using GeneratedWorlds.Domain.Types;
using GeneratedWorlds.Infrastructure.DataModels;
using GeneratedWorlds.Infrastructure.DataModels.Items;
using GeneratedWorlds.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace GeneratedWorlds.Infrastructure.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly AppDbContext _context;

        public CharacterRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Character?> GetByIdAsync(Guid reference)
        {
            var entity = await _context.Characters
                .Include(c => c.Skills)
                .Include(c => c.Inventory)
                .FirstOrDefaultAsync(c => c.Reference == reference);

            return entity is null ? null : await MapToDomainAsync(entity);
        }

        public async Task<Character?> GetByNameAsync(string name)
        {
            var entity = await _context.Characters
                .Include(c => c.Skills)
                .Include(c => c.Inventory)
                .FirstOrDefaultAsync(c => c.Name == name);

            return entity is null ? null : await MapToDomainAsync(entity);
        }

        public async Task<Character> CreateAsync(Character character)
        {
            var dataModel = new CharacterDataModel
            {
                Reference = character.Reference,
                Name = character.Name,
                Skills = character.Skills?.Skills.Select(s => new CharacterSkillDataModel
                {
                    Reference = Guid.NewGuid(),
                    CharacterReference = character.Reference,
                    SkillType = s.Key,
                    Experience = s.Value
                }).ToList()
            };

            _context.Characters.Add(dataModel);
            await _context.SaveChangesAsync();

            return await MapToDomainAsync(dataModel);
        }

        public async Task<Character?> UpdateSkillAsync(Guid reference, SkillType skill, int newExperience)
        {
            var entity = await _context.Characters
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Reference == reference);

            if (entity == null) return null;

            var skillEntry = entity.Skills.FirstOrDefault(s => s.SkillType == skill);
            if (skillEntry == null)
            {
                skillEntry = new CharacterSkillDataModel
                {
                    Reference = Guid.NewGuid(),
                    CharacterReference = reference,
                    SkillType = skill,
                    Experience = newExperience
                };
                entity.Skills.Add(skillEntry);
            }
            else
            {
                skillEntry.Experience = newExperience;
            }

            await _context.SaveChangesAsync();
            return await MapToDomainAsync(entity);
        }

        public async Task<bool> AddItemToInventoryAsync(Guid characterReference, Item item, int quantity)
        {
            var character = await _context.Characters
                .Include(c => c.Inventory)
                .FirstOrDefaultAsync(c => c.Reference == characterReference);

            if (character == null) return false;

            var itemExists = await _context.Items.AnyAsync(i => i.Reference == item.Reference);
            if (!itemExists) return false;

            var existing = character.Inventory.FirstOrDefault(i => i.ItemReference == item.Reference);
            if (existing != null)
            {
                existing.Quantity += quantity;
                _context.Entry(existing).State = EntityState.Modified;
            }
            else
            {
                var entry = new CharacterInventoryItemDataModel
                {
                    Reference = Guid.NewGuid(),
                    CharacterReference = characterReference,
                    ItemReference = item.Reference,
                    Name = item.Name,
                    ItemType = item.Type,
                    Quantity = quantity
                };

                _context.InventoryItems.Add(entry);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public Task SaveChangesAsync() => _context.SaveChangesAsync();

        private async Task<Character> MapToDomainAsync(CharacterDataModel data)
        {
            var character = new Character(data.Name, data.Reference)
            {
                Skills = new CharacterSkills
                {
                    Skills = data.Skills?.ToDictionary(
                        s => s.SkillType,
                        s => s.Experience
                    ) ?? new Dictionary<SkillType, int>()
                },
                Inventory = new CharacterInventory()
            };

            if (data.Inventory != null && data.Inventory.Any())
            {
                var itemRefs = data.Inventory.Select(i => i.ItemReference).ToList();

                // Load all known potion data in one query
                var potionData = await _context.Items
                    .OfType<PotionDataModel>()
                    .Where(p => itemRefs.Contains(p.Reference))
                    .ToListAsync();

                var potionsByRef = potionData.ToDictionary(p => p.Reference, p => p.ToDomain());

                foreach (var inv in data.Inventory)
                {
                    Item item = inv.ItemType switch
                    {
                        ItemType.Potion when potionsByRef.TryGetValue(inv.ItemReference, out var potion) => potion,
                        //_ => new Item(inv.ItemReference, inv.Name, inv.ItemType)
                    };

                    character.Inventory.Items[inv.ItemReference] = (item, inv.Quantity);
                }
            }

            return character;
        }

    }
}
