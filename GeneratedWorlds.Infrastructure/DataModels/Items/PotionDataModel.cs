using GeneratedWorlds.Domain.Types;

namespace GeneratedWorlds.Infrastructure.DataModels.Items
{
    public class PotionDataModel : ItemDataModel
    {
        public SkillType RelatedSkill { get; set; }
        public string Effect { get; set; }
    }
}
