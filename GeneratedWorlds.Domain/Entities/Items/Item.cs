using GeneratedWorlds.Domain.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratedWorlds.Domain.Entities.Items
{
    public abstract class Item
    {
        public Guid Reference { get; set; }
        public string Name { get; set; }
        public ItemType Type { get; set; }

        protected Item(string name, ItemType type)
        {
            Reference = Guid.NewGuid();
            Name = name;
            Type = type;
        }

        protected Item(Guid reference, string name, ItemType type)
        {
            Reference = reference;
            Name = name;
            Type = type;
        }

        protected Item() { } // For serialization
    }
}
