using System.ComponentModel.DataAnnotations;

namespace GeneratedWorlds.Infrastructure.DataModels
{
    public class CharacterDataModel
    {
        [Key]
        public Guid Reference { get; set; }

        public string Name { get; set; }

        public virtual ICollection<CharacterSkillDataModel> Skills { get; set; }

        public virtual ICollection<CharacterInventoryItemDataModel> Inventory { get; set; }
    }
}
