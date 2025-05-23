using GeneratedWorlds.Application.Common.Interfaces;
using GeneratedWorlds.Domain.Entities;
using GeneratedWorlds.Domain.Types;
using MediatR;

namespace GeneratedWorlds.Application.Characters.Commands
{
    public record TrainSkillCommand(Guid CharacterReference, SkillType Skill) : IRequest<Character>;

    public class TrainSkillHandler : IRequestHandler<TrainSkillCommand, Character>
    {
        private readonly ICharacterRepository _repository;
        private static readonly Random _random = new();

        public TrainSkillHandler(ICharacterRepository repository)
        {
            _repository = repository;
        }

        public async Task<Character> Handle(TrainSkillCommand request, CancellationToken cancellationToken)
        {
            var character = await _repository.GetByIdAsync(request.CharacterReference);
            if (character == null)
                return null;

            var currentXp = character.Skills.Skills.ContainsKey(request.Skill)
                ? character.Skills.Skills[request.Skill]
                : 0;

            var xpGain = _random.Next(1, 6);
            var newExperience = currentXp + xpGain;

            return await _repository.UpdateSkillAsync(request.CharacterReference, request.Skill, newExperience);
        }
    }
}
