using GeneratedWorlds.Domain.Entities.Items;
using System.Collections.Generic;

namespace GeneratedWorlds.Domain.Entities
{
    public class CharacterInventory
    {
        public Dictionary<Guid, (Item Item, int Quantity)> Items { get; set; } = new();

        public void AddItem(Item item, int quantity = 1)
        {
            if (Items.ContainsKey(item.Reference))
            {
                Items[item.Reference] = (item, Items[item.Reference].Quantity + quantity);
            }
            else
            {
                Items[item.Reference] = (item, quantity);
            }
        }

        public void RemoveItem(Guid reference, int quantity = 1)
        {
            if (!Items.ContainsKey(reference)) return;

            var (item, currentQuantity) = Items[reference];
            var newQuantity = currentQuantity - quantity;

            if (newQuantity <= 0)
                Items.Remove(reference);
            else
                Items[reference] = (item, newQuantity);
        }

        public IEnumerable<T> GetItemsOfType<T>() where T : Item
        {
            return Items.Values
                        .Where(x => x.Item is T)
                        .Select(x => x.Item)
                        .Cast<T>();
        }

        public (Item Item, int Quantity)? GetItem(Guid reference)
        {
            return Items.TryGetValue(reference, out var value) ? value : null;
        }
    }
}
