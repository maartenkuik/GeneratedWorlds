using GeneratedWorlds.Application.Common.Interfaces;
using GeneratedWorlds.Domain.Entities;
using GeneratedWorlds.Domain.Types;
using MediatR;

namespace GeneratedWorlds.Application.Items.Commands
{
    public record CreatePotionCommand(string Name, SkillType RelatedSkill, string Effect) : IRequest<Potion>;

    public class CreatePotionHandler : IRequestHandler<CreatePotionCommand, Potion>
    {
        private readonly IItemRepository<Potion> _repository;

        public CreatePotionHandler(IItemRepository<Potion> repository)
        {
            _repository = repository;
        }

        public async Task<Potion> Handle(CreatePotionCommand request, CancellationToken cancellationToken)
        {
            var potion = new Potion(request.Name, request.RelatedSkill, request.Effect);
            return await _repository.CreateAsync(potion);
        }
    }
}
