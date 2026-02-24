using CrewSchedule.Application.Commands;
using CrewSchedule.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace CrewSchedule.WebApi.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/flights")]
    public class FlightsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FlightsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllFlightsQuery());
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetFlightByIdQuery(id));

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("{id:guid}/crew")]
        public async Task<IActionResult> GetCrew(Guid id)
        {
            var result = await _mediator.Send(new GetCrewForFlightQuery(id));

            return Ok(result);
        }

        [HttpPost("{flightId:guid}/checkin")]
        public async Task<IActionResult> CheckIn(Guid flightId, [FromBody] Guid crewMemberId)
        {
            await _mediator.Send(new CheckInCommand(flightId, crewMemberId));

            return NoContent();
        }
    }
}
