using GeneratedWorlds.Domain.Types;
using System.ComponentModel.DataAnnotations;

namespace GeneratedWorlds.Infrastructure.DataModels
{
    public class CharacterSkillDataModel
    {
        [Key]
        public Guid Reference { get; set; }

        public Guid CharacterReference { get; set; }

        public SkillType SkillType { get; set; }

        public int Experience { get; set; }

        public virtual CharacterDataModel Character { get; set; }

    }
}
