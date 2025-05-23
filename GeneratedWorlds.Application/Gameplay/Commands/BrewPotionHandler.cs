using GeneratedWorlds.Application.Common.Interfaces;
using GeneratedWorlds.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratedWorlds.Application.Gameplay.Commands
{
    public class BrewPotionResult
    {
        public Potion Potion { get; set; }
        public string Message { get; set; }
    }

    public record BrewPotionCommand(Guid CharacterReference) : IRequest<BrewPotionResult>;

    public class BrewPotionHandler : IRequestHandler<BrewPotionCommand, BrewPotionResult>
    {
        private readonly IItemRepository<Potion> _potionRepository;
        private readonly ICharacterRepository _characterRepository;
        private readonly Random _random = new();

        public BrewPotionHandler(
            IItemRepository<Potion> potionRepository,
            ICharacterRepository characterRepository)
        {
            _potionRepository = potionRepository;
            _characterRepository = characterRepository;
        }

        public async Task<BrewPotionResult> Handle(BrewPotionCommand request, CancellationToken cancellationToken)
        {
            var potions = (await _potionRepository.GetAllAsync()).ToList();

            if (!potions.Any())
                throw new InvalidOperationException("No potions available to brew.");

            var selectedPotion = potions[_random.Next(potions.Count)];

            var success = await _characterRepository.AddItemToInventoryAsync(request.CharacterReference, selectedPotion, 1);
            if (!success)
                throw new InvalidOperationException("Character not found or item could not be added.");

            return new BrewPotionResult
            {
                Potion = selectedPotion,
                Message = $"Brewed {selectedPotion.Name}!"
            };
        }
    }
}
