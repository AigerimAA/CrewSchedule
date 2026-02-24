using CrewSchedule.Application.Interfaces;
using CrewSchedule.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace CrewSchedule.Application.Commands
{
    public record class ApproveSwapCommand(Guid SwapId, Guid ApproverId) : IRequest;

    public class ApproveSwapCommandHandler : IRequestHandler<ApproveSwapCommand>
    {
        private readonly ISwapRequestRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ApproveSwapCommandHandler(ISwapRequestRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ApproveSwapCommand request, CancellationToken cancellationToken)
        {
            var swap = await _repository.GetAsync(request.SwapId, cancellationToken);

            if (swap is null)
                throw new Exception("Swap not found");

            swap.Approve(request.ApproverId);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
