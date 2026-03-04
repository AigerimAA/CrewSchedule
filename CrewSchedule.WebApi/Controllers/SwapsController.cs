using CrewSchedule.Application.Commands;
using CrewSchedule.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CrewSchedule.WebApi.Controllers
{
    [ApiController]
    [Route("api/swaps")]
    public class SwapsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SwapsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Получить swap-запрос по ID
        /// </summary>
        /// 
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetSwapRequestQuery(id));
            return Ok(result);
        }

        /// <summary>
        /// Создать запрос на замену между двумя членами экипажа
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSwapRequestCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        /// <summary>
        /// Подтвердить swap (только целевой член экипажа)
        /// </summary>
        [HttpPost("{id:guid}/approve")]
        public async Task<IActionResult> Approve(Guid id, [FromBody] Guid approverId)
        {
            await _mediator.Send(new ApproveSwapCommand(id));
            return NoContent();
        }

        /// <summary>
        /// Отклонить swap (только целевой член экипажа)
        /// </summary>
        [HttpPost("{id:guid}/reject")]
        public async Task<IActionResult> Reject (Guid id, [FromBody] Guid rejecterId)
        {
            await _mediator.Send(new RejectSwapCommand(id, rejecterId));
            return NoContent();
        }

        /// <summary>
        /// Отменить swap (только инициатор)
        /// </summary>
        [HttpPost("{id:guid}/cancel")]
        public async Task<IActionResult> Cancel(Guid id, [FromBody] Guid requesterId)
        {
            await _mediator.Send(new CancelSwapCommand(id, requesterId));
            return NoContent();
        }
    }
}
