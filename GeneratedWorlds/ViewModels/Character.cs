using GeneratedWorlds.Domain.Types;

namespace GeneratedWorlds.ViewModels
{
    public class Character
    {
        public Guid Reference { get; set; }

        public string Name { get; set; }

        public CharacterInventory Inventory { get; set; }

        public CharacterSkills Skills { get; set; }

        public Character(string name)
        {
            Name = name;
            Reference = Guid.NewGuid();
        }

        public Character(string name, Guid reference)
        {
            Name = name;
            Reference = reference;
        }

        public Character(string name, Guid reference, Dictionary<SkillType, int> skills)
        {
            Name = name;
            Reference = reference;
            Skills = new CharacterSkills(skills);
        }
    }
}
