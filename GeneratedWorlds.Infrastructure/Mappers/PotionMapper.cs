using GeneratedWorlds.Domain.Entities;
using GeneratedWorlds.Domain.Types;
using GeneratedWorlds.Infrastructure.DataModels.Items;

namespace GeneratedWorlds.Infrastructure.Mappers
{
    public static class PotionMapper
    {
        public static Potion ToDomain(this PotionDataModel data)
        {
            return new Potion(
                data.Reference,
                data.Name,
                data.RelatedSkill,
                data.Effect
            );
        }

        public static PotionDataModel ToData(this Potion item)
        {
            return new PotionDataModel
            {
                Reference = item.Reference,
                Name = item.Name,
                Type = ItemType.Potion, // required for EF Core TPH
                RelatedSkill = item.RelatedSkill,
                Effect = item.Effect
            };
        }
    }
}
