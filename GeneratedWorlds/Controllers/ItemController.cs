using GeneratedWorlds.Application.Items.Commands;
using GeneratedWorlds.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GeneratedWorlds.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("potion")]
        public async Task<ActionResult<Potion>> CreatePotion([FromBody] CreatePotionCommand command)
        {
            var potion = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetPotion), new { reference = potion.Reference }, potion);
        }

        // Optional: Placeholder for future retrieval
        [HttpGet("potion/{reference:guid}")]
        public async Task<IActionResult> GetPotion(Guid reference)
        {
            return Ok(); // To be implemented later
        }
    }
}
