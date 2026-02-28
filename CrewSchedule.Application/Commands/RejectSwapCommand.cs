using System;
using System.Collections.Generic;
using System.Text;
using CrewSchedule.Application.Exceptions;
using CrewSchedule.Application.Interfaces;
using CrewSchedule.Application.Repositories;
using CrewSchedule.Domain.Aggregates;
using MediatR;

namespace CrewSchedule.Application.Commands
{
    public record class RejectSwapCommand(Guid SwapId, Guid RejecterId) : IRequest;

    public class RejectSwapCommandHandler : IRequestHandler<RejectSwapCommand>
    {
        private readonly ISwapRequestRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RejectSwapCommandHandler(ISwapRequestRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(RejectSwapCommand request, CancellationToken cancellationToken)
        {
            var swap = await _repository.GetAsync(request.SwapId, cancellationToken);

            if (swap is null)
                throw new NotFoundException(nameof(SwapRequest), request.SwapId);

            swap.Reject(request.RejecterId);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
