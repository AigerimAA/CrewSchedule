using CrewSchedule.Application.Exceptions;
using CrewSchedule.Application.Interfaces;
using CrewSchedule.Application.Repositories;
using CrewSchedule.Domain.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace CrewSchedule.Application.Commands
{
    public record class ApproveSwapCommand(Guid SwapId) : IRequest;

    public class ApproveSwapCommandHandler : IRequestHandler<ApproveSwapCommand>
    {
        private readonly ISwapRequestRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public ApproveSwapCommandHandler(ISwapRequestRepository repository, IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task Handle(ApproveSwapCommand request, CancellationToken cancellationToken)
        {
            var approverId = _currentUserService.UserId
                ?? throw new UnauthorizedAccessException("User is not authenticated");

            var swap = await _repository.GetAsync(request.SwapId, cancellationToken);

            if (swap is null)
                throw new NotFoundException(nameof(SwapRequest), request.SwapId);

            swap.Approve(approverId);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
