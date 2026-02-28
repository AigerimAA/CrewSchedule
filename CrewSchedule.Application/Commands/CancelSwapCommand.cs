using CrewSchedule.Application.Exceptions;
using CrewSchedule.Application.Interfaces;
using CrewSchedule.Application.Repositories;
using CrewSchedule.Domain.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Application.Commands
{
    public record class CancelSwapCommand(Guid SwapId, Guid RequesterId) : IRequest;

    public class CancelSwapCommandHandler : IRequestHandler<CancelSwapCommand>
    {
        private readonly ISwapRequestRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CancelSwapCommandHandler(ISwapRequestRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CancelSwapCommand request, CancellationToken cancellationToken)
        {
            var swap = await _repository.GetAsync(request.SwapId, cancellationToken);

            if (swap is null)
                throw new NotFoundException(nameof(SwapRequest), request.SwapId);

            swap.Cancel(request.RequesterId);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
    
}
