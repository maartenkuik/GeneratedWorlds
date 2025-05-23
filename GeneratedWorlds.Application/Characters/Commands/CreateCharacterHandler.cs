using GeneratedWorlds.Application.Common.Interfaces;
using GeneratedWorlds.Domain.Entities;
using MediatR;

namespace GeneratedWorlds.Application.Characters.Commands
{
    public record CreateCharacterCommand(string Name, Guid Reference) : IRequest<Character>;

    public class CreateCharacterHandler : IRequestHandler<CreateCharacterCommand, Character>
    {
        private readonly ICharacterRepository _repository;

        public CreateCharacterHandler(ICharacterRepository repository)
        {
            _repository = repository;
        }

        public async Task<Character> Handle(CreateCharacterCommand request, CancellationToken cancellationToken)
        {
            var character = new Character(request.Name, request.Reference)
            {
                Inventory = new CharacterInventory(),
                Skills = new CharacterSkills()
            };

            return await _repository.CreateAsync(character);
        }
    }
}
