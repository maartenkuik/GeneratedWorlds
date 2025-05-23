namespace GeneratedWorlds.Domain.Entities
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
    }
}
