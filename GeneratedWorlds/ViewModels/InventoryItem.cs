using GeneratedWorlds.Domain.Types;

namespace GeneratedWorlds.ViewModels
{
    public class InventoryItem
    {
        public Guid Reference { get; set; }
        public string Name { get; set; }
        public ItemType Type { get; set; }
        public int Quantity { get; set; }
    }
}
