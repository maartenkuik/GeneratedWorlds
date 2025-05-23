using GeneratedWorlds.Domain.Types;

namespace GeneratedWorlds.Domain.Entities
{
    public class CharacterSkills
    {
        public Dictionary<SkillType, int> Skills { get; set; } = new();

        public CharacterSkills() 
        {
            var initialSkills = new Dictionary<SkillType, int>
            {
                { SkillType.Cooking, 1 },
                { SkillType.Fletching, 1 },
                { SkillType.Brewery, 1 },
                { SkillType.Smithing, 1 },
            };

            Skills = initialSkills;
        }
    }
}
