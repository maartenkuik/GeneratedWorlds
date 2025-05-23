using GeneratedWorlds.Application.Common.Interfaces;
using GeneratedWorlds.Domain.Entities;
using MediatR;

namespace GeneratedWorlds.Application.Characters.Queries
{
    public record GetCharacterByNameQuery(string name) : IRequest<Character>;

    public class GetCharacterByNameHandler : IRequestHandler<GetCharacterByNameQuery, Character>
    {
        private readonly ICharacterRepository _repository;

        public GetCharacterByNameHandler(ICharacterRepository repository)
        {
            _repository = repository;
        }

        public async Task<Character> Handle(GetCharacterByNameQuery request, CancellationToken cancellationToken)
        {
            var character = await _repository.GetByNameAsync(request.name);
            return character;
        }
    }
}