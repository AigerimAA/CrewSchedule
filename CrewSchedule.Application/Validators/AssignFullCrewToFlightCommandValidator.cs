using CrewSchedule.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Application.Validators
{
    public class AssignFullCrewToFlightCommandValidator : AbstractValidator<AssignFullCrewToFlightCommand>
    {
        public AssignFullCrewToFlightCommandValidator()
        {
            RuleFor(x => x.FlightId).NotEmpty();
            RuleFor(x => x.Crew).NotEmpty();
            RuleForEach(x => x.Crew)
                .ChildRules(c =>
                {
                    c.RuleFor(x => x.CrewMemberId).NotEmpty();
                    c.RuleFor(x => x.Role).IsInEnum();
                });
            RuleFor(x => x.FlightEndUtc)
                .GreaterThan(x => x.FlightStartUtc);
        }
    }
}
