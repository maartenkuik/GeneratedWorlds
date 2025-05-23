using GeneratedWorlds.Domain.Entities;
using View = GeneratedWorlds.ViewModels;

namespace GeneratedWorlds.Mappers
{
    public static class CharacterMapper
    {
        public static View.Character ToViewModel(this Character character)
        {
            var inventory = new View.CharacterInventory();

            foreach (var (itemRef, (item, quantity)) in character.Inventory.Items)
            {
                inventory.Items.Add(new View.InventoryItem
                {
                    Reference = item.Reference,
                    Name = item.Name,
                    Type = item.Type,
                    Quantity = quantity
                });
            }

            return new View.Character(character.Name, character.Reference, character.Skills.Skills)
            {
                Inventory = inventory
            };
        }
    }
}
