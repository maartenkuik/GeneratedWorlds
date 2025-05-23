using GeneratedWorlds.Domain.Types;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneratedWorlds.Infrastructure.DataModels
{
    public class CharacterInventoryItemDataModel
    {
        [Key]
        public Guid Reference { get; set; }

        public Guid CharacterReference { get; set; }

        public Guid ItemReference { get; set; }

        public string Name { get; set; }

        public ItemType ItemType { get; set; }

        public int Quantity { get; set; }

        public virtual CharacterDataModel Character { get; set; }
    }

}
