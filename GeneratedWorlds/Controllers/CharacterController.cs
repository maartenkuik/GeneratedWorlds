using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using MediatR;
using GeneratedWorlds.Application.Characters.Commands;
using GeneratedWorlds.Application.Characters.Queries;
using GeneratedWorlds.ViewModels;
using GeneratedWorlds.Mappers;

namespace GeneratedWorlds.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CharacterController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("by-name/{name}")]
        public async Task<ActionResult<ViewModels.Character>> GetByName(string name)
        {
            var character = await _mediator.Send(new GetCharacterByNameQuery(name));
            return character is null ? NotFound() : Ok(character.ToViewModel());
        }

        [HttpGet("by-reference/{reference:guid}")]
        public async Task<ActionResult<ViewModels.Character>> GetByReference(Guid reference)
        {
            var character = await _mediator.Send(new GetCharacterByIdQuery(reference));
            return character is null ? NotFound() : Ok(character.ToViewModel());
        }

        [HttpPost("{characterReference:guid}/add-item/{itemReference:guid}")]
        public async Task<IActionResult> AddItemToInventory(
            Guid characterReference,
            Guid itemReference,
            [FromQuery] int quantity = 1)
        {
            var command = new AddItemToInventoryCommand(characterReference, itemReference, quantity);
            var result = await _mediator.Send(command);

            return result ? Ok("Item added to inventory.") : NotFound("Character or item not found.");
        }


        // POST: api/Character
        [HttpPost]
        public async Task<ActionResult<Character>> Create([FromBody] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Name is required.");

            var reference = Guid.NewGuid();
            var character = await _mediator.Send(new CreateCharacterCommand(name, reference));

            return CreatedAtAction(nameof(GetByReference), new { reference = character.Reference }, character);
        }
    }
}
