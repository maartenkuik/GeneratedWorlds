using GeneratedWorlds.Domain.Types;

namespace GeneratedWorlds.Application.Common.Interfaces
{
    public interface IPotionGenerator
    {
        Task<(string name, string effect)> GeneratePotionAsync(int skillLevel, SkillType skillType);
    }
}
