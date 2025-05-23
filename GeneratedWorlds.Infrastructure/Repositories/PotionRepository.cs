using GeneratedWorlds.Application.Common.Interfaces;
using GeneratedWorlds.Domain.Entities;
using GeneratedWorlds.Infrastructure.DataModels.Items;
using GeneratedWorlds.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratedWorlds.Infrastructure.Repositories
{
    public class PotionRepository : IItemRepository<Potion>
    {
        private readonly AppDbContext _context;

        public PotionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Potion?> GetByIdAsync(Guid reference)
        {
            var data = await _context.Items.OfType<PotionDataModel>().FirstOrDefaultAsync(i => i.Reference == reference);
            return data?.ToDomain();
        }

        public async Task<IEnumerable<Potion>> GetAllAsync()
        {
            return await _context.Items
                .OfType<PotionDataModel>()
                .Select(p => p.ToDomain())
                .ToListAsync();
        }

        public async Task<Potion> CreateAsync(Potion potion)
        {
            var data = potion.ToData();
            _context.Items.Add(data);
            await _context.SaveChangesAsync();
            return data.ToDomain();
        }
    }
}
