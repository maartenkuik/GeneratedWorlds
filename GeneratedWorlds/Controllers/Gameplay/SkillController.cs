using GeneratedWorlds.Application.Characters.Commands;
using GeneratedWorlds.Domain.Types;
using GeneratedWorlds.Mappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GeneratedWorlds.Controllers.Gameplay
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SkillController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("train")]
        public async Task<IActionResult> TrainSkill([FromQuery] Guid characterRef, [FromQuery] SkillType skill)
        {
            var result = await _mediator.Send(new TrainSkillCommand(characterRef, skill));
            if (result == null)
                return NotFound("Character not found.");

            return Ok(result.ToViewModel());
        }
    }
}