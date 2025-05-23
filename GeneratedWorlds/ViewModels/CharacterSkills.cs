using GeneratedWorlds.Domain.Types;

namespace GeneratedWorlds.ViewModels
{
    public class CharacterSkills
    {
        public Dictionary<SkillType, int> Skills { get; set; } = new();

        public CharacterSkills(Dictionary<SkillType, int> skills) 
        {
            Skills = skills;
        }
    }
}
