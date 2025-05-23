using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratedWorlds.Application.Common.Interfaces
{
    public interface IPotionGenerator
    {
        Task<(string name, string effect)> GeneratePotionAsync(int skillLevel);
    }
}
