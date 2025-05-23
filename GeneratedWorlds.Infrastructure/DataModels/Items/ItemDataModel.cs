using GeneratedWorlds.Domain.Types;
using System;
using System.ComponentModel.DataAnnotations;

namespace GeneratedWorlds.Infrastructure.DataModels.Items
{
    public class ItemDataModel
    {
        [Key]
        public Guid Reference { get; set; }

        public string Name { get; set; }

        public ItemType Type { get; set; }
    }
}
