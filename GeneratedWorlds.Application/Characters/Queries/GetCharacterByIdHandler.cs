using GeneratedWorlds.Application.Common.Interfaces;
using GeneratedWorlds.Domain.Entities;
using MediatR;

namespace GeneratedWorlds.Application.Characters.Queries
{
    public record GetCharacterByIdQuery(Guid Reference) : IRequest<Character>;

    public class GetCharacterByIdHandler : IRequestHandler<GetCharacterByIdQuery, Character>
    {
        private readonly ICharacterRepository _repository;

        public GetCharacterByIdHandler(ICharacterRepository repository)
        {
            _repository = repository;
        }

        public async Task<Character> Handle(GetCharacterByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Reference);
        }
    }
}
