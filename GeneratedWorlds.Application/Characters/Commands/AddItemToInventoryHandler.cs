using GeneratedWorlds.Application.Common.Interfaces;
using GeneratedWorlds.Domain.Entities;
using MediatR;

namespace GeneratedWorlds.Application.Characters.Commands
{
    public record AddItemToInventoryCommand(Guid CharacterReference, Guid ItemReference, int Quantity) : IRequest<bool>;

    public class AddItemToInventoryHandler : IRequestHandler<AddItemToInventoryCommand, bool>
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IItemRepository<Potion> _potionRepository;

        public AddItemToInventoryHandler(
            ICharacterRepository characterRepository,
            IItemRepository<Potion> potionRepository)
        {
            _characterRepository = characterRepository;
            _potionRepository = potionRepository;
        }

        public async Task<bool> Handle(AddItemToInventoryCommand request, CancellationToken cancellationToken)
        {
            var item = await _potionRepository.GetByIdAsync(request.ItemReference);
            if (item == null) return false;

            return await _characterRepository.AddItemToInventoryAsync(
                request.CharacterReference,
                item,
                request.Quantity
            );
        }
    }
}
