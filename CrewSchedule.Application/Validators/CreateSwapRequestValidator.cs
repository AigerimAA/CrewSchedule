using CrewSchedule.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Application.Validators
{
    public class CreateSwapRequestValidator : AbstractValidator<CreateSwapRequestCommand>
    {
        public CreateSwapRequestValidator()
        {
            RuleFor(x => x.FromCrewMemberId).NotEmpty();
            RuleFor(x => x.ToCrewMemberId).NotEmpty();
            RuleFor(x => x.FlightId).NotEmpty();
        }
    }
}
