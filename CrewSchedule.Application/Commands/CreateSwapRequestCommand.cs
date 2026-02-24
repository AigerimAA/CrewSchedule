using CrewSchedule.Application.Interfaces;
using CrewSchedule.Application.Repositories;
using CrewSchedule.Domain.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Application.Commands
{
    public record CreateSwapRequestCommand(Guid FromCrewMemberId, Guid ToCrewMemberId, Guid FlightId) : IRequest<Guid>;


    public class CreateSwapRequestCommandHandler : IRequestHandler<CreateSwapRequestCommand, Guid>
    {
        private readonly ISwapRequestRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSwapRequestCommandHandler(ISwapRequestRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateSwapRequestCommand request, CancellationToken cancellationToken)
        {
            var swap = new SwapRequest(request.FromCrewMemberId, request.ToCrewMemberId, request.FlightId);

            await _repository.AddAsync(swap, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return swap.Id;
        }
    }
}
