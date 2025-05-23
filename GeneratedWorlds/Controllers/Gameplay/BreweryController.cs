using GeneratedWorlds.Application.Gameplay.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GeneratedWorlds.Controllers.Gameplay
{
    [ApiController]
    [Route("api/[controller]")]
    public class BreweryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BreweryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("brew/{characterReference:guid}")]
        public async Task<IActionResult> BrewPotion(Guid characterReference)
        {
            var result = await _mediator.Send(new BrewPotionCommand(characterReference));
            return Ok(result);
        }
    }
}
