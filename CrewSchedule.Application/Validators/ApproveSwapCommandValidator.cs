using CrewSchedule.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Application.Validators
{
    public class ApproveSwapCommandValidator : AbstractValidator<ApproveSwapCommand>
    {
        public ApproveSwapCommandValidator()
        {
            RuleFor(x => x.SwapId)
                .NotEmpty().WithMessage("SwapId is required");
        }
    }
}
