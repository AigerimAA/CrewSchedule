using CrewSchedule.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Application.Validators
{
    public class CheckInCommandValidator : AbstractValidator<CheckInCommand>
    {
        public CheckInCommandValidator()
        {
            RuleFor(x => x.FlightId)
                .NotEmpty();

            RuleFor(x => x.CrewMemberId)
                .NotEmpty();
        }
    }
}
