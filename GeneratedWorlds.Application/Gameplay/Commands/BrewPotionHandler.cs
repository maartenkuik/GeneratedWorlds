using GeneratedWorlds.Application.Common.Interfaces;
using GeneratedWorlds.Application.Items.Commands;
using GeneratedWorlds.Domain.Entities;
using GeneratedWorlds.Domain.Entities.Items;
using GeneratedWorlds.Domain.Types;
using MediatR;

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
        private readonly ICharacterRepository _characterRepository;
        private readonly IPotionGenerator _potionGenerator;
        private readonly IMediator _mediator;

        public BrewPotionHandler(
            ICharacterRepository characterRepository,
            IPotionGenerator potionGenerator,
            IMediator mediator)
        {
            _characterRepository = characterRepository;
            _potionGenerator = potionGenerator;
            _mediator = mediator;
        }

        public async Task<BrewPotionResult> Handle(BrewPotionCommand request, CancellationToken cancellationToken)
        {
            var character = await _characterRepository.GetByIdAsync(request.CharacterReference);
            if (character == null)
                throw new InvalidOperationException("Character not found.");

            var skillLevel = character.Skills.Skills.TryGetValue(SkillType.Brewery, out var level) ? level : 1;

            // Generate name + effect from AI
            var (name, effect) = await _potionGenerator.GeneratePotionAsync(skillLevel, SkillType.Brewery);

            // Save potion to DB via MediatR
            var potion = await _mediator.Send(new CreatePotionCommand(name, SkillType.Brewery, effect), cancellationToken);

            // Add to character's inventory
            var added = await _characterRepository.AddItemToInventoryAsync(character.Reference, potion, 1);
            if (!added)
                throw new InvalidOperationException("Failed to add potion to character inventory.");

            return new BrewPotionResult
            {
                Potion = potion,
                Message = $"You brewed a {potion.Name}!"
            };
        }
    }
}
