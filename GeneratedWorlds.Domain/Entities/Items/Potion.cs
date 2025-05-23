using GeneratedWorlds.Domain.Entities.Items;
using GeneratedWorlds.Domain.Types;

namespace GeneratedWorlds.Domain.Entities
{
    public class Potion : Item
    {
        public SkillType RelatedSkill { get; set; }
        public string Effect { get; set; }

        public Potion(string name, SkillType relatedSkill, string effect)
            : base(name, ItemType.Potion)
        {
            RelatedSkill = relatedSkill;
            Effect = effect;
        }

        public Potion(Guid reference, string name, SkillType relatedSkill, string effect)
            : base(reference, name, ItemType.Potion)
        {
            RelatedSkill = relatedSkill;
            Effect = effect;
        }

        public Potion() : base() { }
    }
}